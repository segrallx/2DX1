using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtroller : MonoBehaviour
{
    public PlayerInputController mInputController;
    public Vector2 mInputDirection;
    private Rigidbody2D mRigidbody;
    private CapsuleCollider2D mColl;
    private PlayerAnimation mAnim;

    //速度
    [Header("基本逻辑")]
    public float mSpeed = 260;
    public float mVelocityX;
    public float mJumpForce = 10;
    private PhysicCheck mPhysicCheck;

    private float mRunSpeed;
    private float mWalkSpeed;// => mSpeed/2.5f;

    // 是否下蹲
    public bool mIsCrouch;
    private Vector2 mCollOriginOffset;
    private Vector2 mCollOriginSize;

    public bool mIsHurt;
    public float mHurtForce;

    public bool mIsDead;
    public bool mIsAttack;

    public PhysicsMaterial2D mMaterialWall;
    public PhysicsMaterial2D mMaterialNormal;


    private void Awake()
    {
        mInputController = new PlayerInputController();
        mRigidbody = GetComponent<Rigidbody2D>();
        mPhysicCheck = GetComponent<PhysicCheck>();
        mColl = GetComponent<CapsuleCollider2D>();
        mAnim = GetComponent<PlayerAnimation>();

        mCollOriginOffset = mColl.offset;
        mCollOriginSize = mColl.size;

        mInputController.Gameplay.Jump.started += Jump;


        #region 强制走路
        mRunSpeed = mSpeed;
        mWalkSpeed = mSpeed / 2.5f;

        // 按钮按住
        mInputController.Gameplay.WalkButton.performed += ctx =>
        {
            if (mPhysicCheck.mIsGround)
            {
                mSpeed = mWalkSpeed;
            }
        };

        // 按钮抬起
        mInputController.Gameplay.WalkButton.canceled += ctx =>
        {
            if (mPhysicCheck.mIsGround)
            {
                mSpeed = mRunSpeed;
            }
        };

        #endregion


        #region 攻击
        mInputController.Gameplay.Attack.started += PlayerAttack;
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
        CheckState();
    }

    private void CheckState()
    {
        mColl.sharedMaterial = mPhysicCheck.mIsGround ?  mMaterialNormal: mMaterialWall;
    }

    private void FixedUpdate()
    {
        if (!mIsHurt && !mIsAttack)
        {
            Move();
        }
    }

    public void Move()
    {
        // move left & right
        if (!mIsCrouch)
        {
            mRigidbody.velocity = new Vector2(
                mSpeed * mInputDirection.x * Time.deltaTime, mRigidbody.velocity.y);
        }

        int faceDir = (int)transform.localScale.x;
        if (mInputDirection.x > 0)
        {
            faceDir = 1;
        }

        if (mInputDirection.x < 0)
        {
            faceDir = -1;
        }
        // flip
        transform.localScale = new Vector3(faceDir, 1, 1);
        mVelocityX = mRigidbody.velocity.x;

        // 下端
        mIsCrouch = mInputDirection.y < -0.5f && mPhysicCheck.mIsGround;
        if (mIsCrouch)
        {
            // 修改碰撞体
            mColl.offset = new Vector2(-0.05f, 0.85f);
            mColl.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            // 还原碰撞体
            mColl.offset = mCollOriginOffset;
            mColl.size = mCollOriginSize;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        Debug.Log("JUMP");
        if (mPhysicCheck.mIsGround)
        {
            mRigidbody.AddForce(transform.up * mJumpForce, ForceMode2D.Impulse);
        }
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     Debug.Log(collision.name);
    // }

    #region UnityEvet
    public void GetHurt(Transform attacker)
    {
        mIsHurt = true;
        mRigidbody.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        mRigidbody.AddForce(dir * mHurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        mIsDead = true;
        mInputController.Gameplay.Disable(); // 人物操作disable
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        //Debug.Log("player attack");
        mAnim.PlayAttack();
        mIsAttack = true;
    }

    #endregion


}
