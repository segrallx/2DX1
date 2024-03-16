using UnityEngine;

public class BarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        mCurEnemy = enemy;
        mCurEnemy.mCurSpeed = mCurEnemy.mChaseSpeed;

        mCurEnemy.mAnim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        if ( !mCurEnemy.mPhysic.mIsGround
             || (mCurEnemy.mPhysic.mTouchLeftWall&&mCurEnemy.mFaceDir.x<0)
             || (mCurEnemy.mPhysic.mTouchRightWall && mCurEnemy.mFaceDir.x>0) )
        {
            mCurEnemy.transform.localScale = new Vector3(mCurEnemy.mFaceDir.x,1,1);
            mCurEnemy.mFaceDir = new Vector3(-mCurEnemy.transform.localScale.x, 0, 0);
        }


        if(!mCurEnemy.FoundPlayer() && mCurEnemy.mLostTimeCounter>0 ) {
            mCurEnemy.mLostTimeCounter -= Time.deltaTime;
        }else {
            //mCurEnemy.mLostTimeCounter = mCurEnemy.mLostTime;
        }

        if(mCurEnemy.mLostTimeCounter <=0 ) {
            mCurEnemy.SwitchState(NPCState.Patrol);
        }

    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        mCurEnemy.mLostTimeCounter = mCurEnemy.mLostTime;
        mCurEnemy.mAnim.SetBool("run", false);
    }
}
