using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCheck : MonoBehaviour
{

    private CapsuleCollider2D mColl;

    [Header("检测参数")]
    public bool mManual;
    public float mCheckRaduis = 0.2f;
    public LayerMask mGroundLayer;
    public Vector2 mBottomOffset;
    public Vector2 mLeftOffset;
    public Vector2 mRightOffset;

    [Header("状态")]
    public bool mIsGround;
    public bool mTouchLeftWall;
    public bool mTouchRightWall;


    void Awake()
    {
        mColl = GetComponent<CapsuleCollider2D>();
        if (!mManual)
        {
            mRightOffset = new Vector2((mColl.bounds.size.x + mColl.offset.x) / 2, mColl.bounds.size.y / 2);
            mLeftOffset = new Vector2(-mRightOffset.x,mRightOffset.y);
        }
    }

    //
    void Update()
    {
        Check();
    }

    void Check()
    {
        // 检测地面
        mIsGround = Physics2D.OverlapCircle((Vector2)transform.position+mBottomOffset,
                                            mCheckRaduis, mGroundLayer);

        //left wall
        mTouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + mLeftOffset,
                                            mCheckRaduis, mGroundLayer);
        mTouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + mRightOffset,
                                    mCheckRaduis, mGroundLayer);


    }

    // render in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position+mBottomOffset, mCheckRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + mLeftOffset, mCheckRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + mRightOffset, mCheckRaduis);

    }

}
