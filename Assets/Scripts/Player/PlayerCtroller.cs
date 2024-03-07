using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtroller : MonoBehaviour
{
    public PlayerInputController mInputController;
    public Vector2 mInputDirection;
    private Rigidbody2D mRigidbody;

    //速度
    [Header("基本逻辑")]
    public float mSpeed = 260;
    public float mVelocityX;
    public float mJumpForce = 10;
    private PhysicCheck mPhysicCheck;

    private float mRunSpeed;
    private float mWalkSpeed ;// => mSpeed/2.5f;


    private void Awake()
    {
        mInputController = new PlayerInputController();
        mRigidbody = GetComponent<Rigidbody2D>();
        mPhysicCheck = GetComponent<PhysicCheck>();

        mInputController.Gameplay.Jump.started += Jump;


        #region 强制走路
        mRunSpeed = mSpeed;
        mWalkSpeed = mSpeed/2.5f;

        // 按钮按住
        mInputController.Gameplay.WalkButton.performed +=  ctx => {
            if(mPhysicCheck.mIsGround) {
                mSpeed = mWalkSpeed;
            }
        };

        // 按钮抬起
        mInputController.Gameplay.WalkButton.canceled +=  ctx => {
            if(mPhysicCheck.mIsGround) {
                mSpeed = mRunSpeed;
            }
        };

        #endregion

    }

    private void OnEnable()
    {
        mInputController.Enable();
    }

    private void OnDisable()
    {
        mInputController.Disable();
    }

    public void Update()
    {
        mInputDirection = mInputController.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // move left & right
        mRigidbody.velocity = new Vector2(
            mSpeed * mInputDirection.x * Time.deltaTime, mRigidbody.velocity.y);

        int faceDir = (int)transform.localScale.x;
        if(mInputDirection.x >0 ) {
            faceDir = 1;
        }

        if(mInputDirection.x <0 ) {
            faceDir = -1;
        }
        // flip
        transform.localScale =  new Vector3(faceDir,1,1);

        mVelocityX = mRigidbody.velocity.x;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("JUMP");
        if(mPhysicCheck.mIsGround)
        {
            mRigidbody.AddForce(transform.up * mJumpForce, ForceMode2D.Impulse);
        }

    }


}
