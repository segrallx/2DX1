using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{

    private CapsuleCollider2D mColl;

    [Header("������")]
    public bool mManual;
    public float mCheckRaduis = 0.2f;
    public LayerMask mGroundLayer;
    public Vector2 mBottomOffset;
    public Vector2 mLeftOffset;
    public Vector2 mRightOffset;

    [Header("״̬")]
    public bool mIsGround;
    public bool mTouchLeftWall;
    public bool mTouchRightWall;


    void Awake()
    {
        mColl = GetComponent<CapsuleCollider2D>();
        if (!mManual)
        {
            mRightOffset = new Vector2((mColl.bounds.size.x + mColl.offset.x) / 2,
                                       mColl.bounds.size.y / 2);
            mLeftOffset = new Vector2(-mRightOffset.x, mRightOffset.y);
        }
    }

    //
    void Update()
    {
        Check();
    }

    void Check()
    {
        // ������
        if (transform.localScale.x > 0)
        {
            mIsGround = Physics2D.OverlapCircle((Vector2)transform.position + mBottomOffset,
                                                mCheckRaduis, mGroundLayer);
            //left wall
            mTouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + mLeftOffset,
                                                     mCheckRaduis, mGroundLayer);

            mTouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + mRightOffset,
                                                      mCheckRaduis, mGroundLayer);
        }
        else
        {
            mIsGround = Physics2D.OverlapCircle((Vector2)transform.position +
                                                new Vector2(-mBottomOffset.x, mBottomOffset.y),
                                                mCheckRaduis, mGroundLayer);
            //left wall
            mTouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position +
                                                     new Vector2(-mLeftOffset.x, mLeftOffset.y),
                                                     mCheckRaduis, mGroundLayer);

            mTouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position +
                                                      new Vector2(-mRightOffset.x, mRightOffset.y),
                                                      mCheckRaduis, mGroundLayer);
        }
    }

    // render in editor
    private void OnDrawGizmosSelected()
    {
        if (transform.localScale.x > 0)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + mBottomOffset, mCheckRaduis);
            Gizmos.DrawWireSphere((Vector2)transform.position + mLeftOffset, mCheckRaduis);
            Gizmos.DrawWireSphere((Vector2)transform.position + mRightOffset, mCheckRaduis);
        }
        else
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(- mBottomOffset.x, mBottomOffset.y) , mCheckRaduis);
            Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(- mLeftOffset.x, mLeftOffset.y) , mCheckRaduis);
            Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(- mRightOffset.x, mRightOffset.y), mCheckRaduis);
        }
    }

}
