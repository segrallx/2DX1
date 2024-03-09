using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator mAnim;
    private Rigidbody2D mRb;
    private PhysicCheck mPhysicCheck;
    private PlayerCtroller mPlayerCtrler;

    // Start is called before the first frame update
    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        mRb = GetComponent<Rigidbody2D>();
        mPhysicCheck = GetComponent<PhysicCheck>();
        mPlayerCtrler = GetComponent<PlayerCtroller>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void  SetAnimation()
    {
        mAnim.SetFloat("velocity_x", Mathf.Abs(mRb.velocity.x));
        mAnim.SetFloat("velocity_y", (mRb.velocity.y));
        mAnim.SetBool("isground", mPhysicCheck.mIsGround);
        mAnim.SetBool("isCrouch", mPlayerCtrler.mIsCrouch);
        mAnim.SetBool("dead", mPlayerCtrler.mIsDead);
        mAnim.SetBool("isAttack", mPlayerCtrler.mIsAttack);
       // mAnim.SetInteger("combo", mPlayerCtrler.combo);

    }

    public void PlayHurt()
    {
        Debug.Log("play hurt");
        mAnim.SetTrigger("hurt");
    }

    public void PlayAttack()
    {
        mAnim.SetTrigger("attack");
    }
}
