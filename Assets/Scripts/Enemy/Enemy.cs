using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D mRb;
    protected Animator mAnim;
    protected PhysicCheck mPhysic;

    [Header("基本参数")]
    public float mNormalSpeed;
    public float mChaseSpeed;
    public float mCurSpeed;
    public Vector3 mFaceDir;
    public float mHurtForce;

    public Transform mAttacker;
    [Header("计时器")]
    public float mWaitTime;
    public float mWaitTimeCounter;
    public bool mWait;

    [Header("状态")]
    public bool mIsHurt;
    public bool mIsDead;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mPhysic = GetComponent<PhysicCheck>();
        mCurSpeed = mNormalSpeed;
        mFaceDir = new Vector3(-transform.localScale.x, 0, 0);
    }

    private void Update()
    {
        if (mWait)
        {
            mWaitTimeCounter -= Time.deltaTime;
            if (mWaitTimeCounter > 0)
            {
                return;
            }
            transform.localScale = new Vector3(mFaceDir.x, 1, 1);
            mWait = false;
            //mWaitTimeCounter = mWaitTime;
            mFaceDir = new Vector3(-transform.localScale.x, 0, 0);
            return;
        }

        if ( (mPhysic.mTouchLeftWall&&mFaceDir.x<0) ||  (mPhysic.mTouchRightWall && mFaceDir.x>0))
        {
            mWait = true;
            mWaitTimeCounter = mWaitTime;
            mAnim.SetBool("walk", false);
        }


    }

    private void FixedUpdate()
    {
        if (mWait)
        {
            return;
        }

        if(mIsHurt || mIsDead )
        {
            return;
        }
        Move();
    }

    public virtual void Move()
    {
        if(!mIsHurt)
        {
            mRb.velocity = new Vector2(mCurSpeed * mFaceDir.x * Time.deltaTime, mRb.velocity.y);
        }
    }


    // 被攻击后转身
    public void OnTakeDamage(Transform attackTrans)
    {
        Debug.Log("enemy tabke damage");
        mAttacker = attackTrans;
        //转身
        if(attackTrans.position.x - transform.position.x >0 ) {
            transform.localScale= new Vector3(-1,1,1);
        }

        if(attackTrans.position.x - transform.position.x <0 ) {
            transform.localScale= new Vector3(1,1,1);
        }

        // 受伤被击退
        mIsHurt = true;
        mAnim.SetTrigger("hurt");

        Vector2 dir = new Vector2(transform.position.x -attackTrans.position.x ,0 ).normalized;
        StartCoroutine(OnHurt(dir));
    }

    IEnumerator OnHurt(Vector2 dir)
    {
        mRb.AddForce(dir*mHurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        mIsHurt = false;
    }

    public void OnDie()
    {
        mAnim.SetBool("dead", true);
        mIsDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

}
