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

    [Header("计时器")]
    public float mWaitTime;
    public float mWaitTimeCounter;
    public bool mWait;

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
        Move();
    }

    public virtual void Move()
    {
        mRb.velocity = new Vector2(mCurSpeed * mFaceDir.x * Time.deltaTime, mRb.velocity.y);
    }

}
