using UnityEngine;

public class BarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        mCurEnemy = enemy;
        mCurEnemy.mCurSpeed = mCurEnemy.mNormalSpeed;
    }

    public override void LogicUpdate()
    {
        if(mCurEnemy.FoundPlayer()){
            mCurEnemy.SwitchState(NPCState.Chase);
        }


        if (mCurEnemy.mWait)
        {
            mCurEnemy.mWaitTimeCounter -= Time.deltaTime;
            if (mCurEnemy.mWaitTimeCounter > 0)
            {
                return;
            }
            mCurEnemy.transform.localScale = new Vector3(mCurEnemy.mFaceDir.x, 1, 1);
            mCurEnemy.mFaceDir = new Vector3(-mCurEnemy.transform.localScale.x, 0, 0);

            mCurEnemy.mWait = false;
            return;
        }


        if ( !mCurEnemy.mPhysic.mIsGround
             || (mCurEnemy.mPhysic.mTouchLeftWall&&mCurEnemy.mFaceDir.x<0)
             || (mCurEnemy.mPhysic.mTouchRightWall && mCurEnemy.mFaceDir.x>0))
        {
            mCurEnemy.mWait = true;
            mCurEnemy.mWaitTimeCounter = mCurEnemy.mWaitTime;
            mCurEnemy.mAnim.SetBool("walk", false);
            mCurEnemy.mRb.velocity = Vector2.zero;
        }

    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        mCurEnemy.mAnim.SetBool("walk", false);
    }
}
