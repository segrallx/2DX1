using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D mRb;
    [HideInInspector]
    public Animator mAnim;
    [HideInInspector]
    public PhysicCheck mPhysic;

    [Header("��������")]
    public float mNormalSpeed;
    public float mChaseSpeed;
    public float mCurSpeed;
    public Vector3 mFaceDir;
    public float mHurtForce;

    public Transform mAttacker;
    [Header("��ʱ��")]
    public float mWaitTime;
    public float mWaitTimeCounter;
    public bool mWait;

    public float mLostTime;
    public float mLostTimeCounter;


    [Header("״̬")]
    public bool mIsHurt;
    public bool mIsDead;

    [Header("���")]
    public Vector2 mCenterOffset;
    public Vector2 mCheckSize;
    public float mCheckDistance;
    public LayerMask mAttackLayer;

    // state machine.
    protected BaseState mPatrolState;
    protected BaseState mCurState;
    protected BaseState mChaseState;
    protected BaseState mSkillState;

    protected virtual void Awake()
    {
        Debug.Log("enemy awake");
        mRb = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mPhysic = GetComponent<PhysicCheck>();
        mCurSpeed = mNormalSpeed;
        mFaceDir = new Vector3(-transform.localScale.x, 0, 0);
        mPatrolState = new BarPatrolState();
    }

    private void OnEnable()
    {
        Debug.Log("enemy on enable");
        mCurState = mPatrolState;
        mCurState.OnEnter(this);
    }


    private void OnDisable()
    {
        Debug.Log("enemy on disable");
        mCurState.OnExit();
    }



    private void Update()
    {
        mCurState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        if(mWait || mIsHurt || mIsDead){
        }
        else {
            Move();
        }

        mCurState.PhysicsUpdate();
    }

    public virtual void Move()
    {
        mAnim.SetBool("walk", true);
        mRb.velocity = new Vector2(mCurSpeed * mFaceDir.x *
                                   Time.deltaTime, mRb.velocity.y);
    }


    public bool FoundPlayer()
    {
        var ret =  Physics2D.BoxCast(transform.position + (Vector3)mCenterOffset,
                                     mCheckSize , 0, mFaceDir, mCheckDistance, mAttackLayer);
        return ret;
    }


    public void SwitchState(NPCState state)
    {
        var newState = state switch {
            NPCState.Patrol => mPatrolState,
            NPCState.Chase => mChaseState,
            _ => null
            //NPCState.Skill => mSkillState,
        };

        mCurState.OnExit();
        mCurState = newState;
        mCurState.OnEnter(this);
    }

    #region  �¼�ִ�з���

    // ��������ת��
    public void OnTakeDamage(Transform attackTrans)
    {
        Debug.Log("enemy tabke damage");
        mAttacker = attackTrans;
        //ת��
        if(attackTrans.position.x - transform.position.x >0 ) {
            transform.localScale= new Vector3(-1,1,1);
        }

        if(attackTrans.position.x - transform.position.x <0 ) {
            transform.localScale= new Vector3(1,1,1);
        }

        // ���˱�����
        mIsHurt = true;
        mAnim.SetTrigger("hurt");

        Vector2 dir = new Vector2(transform.position.x -attackTrans.position.x ,0 ).normalized;
        mRb.velocity = new Vector2(0,mRb.velocity.y);
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
        gameObject.layer = 2 ;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)mCenterOffset + 
            new Vector3(mCheckDistance*- transform.localScale.x,0,0), 0.2f);
    }

}
