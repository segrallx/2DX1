using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    public int mMaxHealth = 50;
    public int mCurrentHealth;

    [Header("受伤无敌")]
    public float mInvulnerableDuration;
    private float mInvulnerableCounter;
    public bool mInvulnurable;
    public UnityEvent<Transform> mOnTakeDamage;
    public UnityEvent mOnDead;

    public UnityEvent<Charactor> mOnHealthChange;


    // Start is called before the first frame update
    void Start()
    {
        mOnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if(!mInvulnurable)
        {
            mInvulnurable = true;
            mInvulnerableCounter = mInvulnerableDuration;
        }
    }

    private void Update()
    {
        if(mInvulnurable)
        {
            mInvulnerableCounter -= Time.deltaTime;
            if (mInvulnerableCounter <= 0)
            {
                mInvulnurable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (mInvulnurable)
        {
            return;
        }

        if(mCurrentHealth - attacker.mDamage >0 )  {
            mCurrentHealth -= attacker.mDamage;
            TriggerInvulnerable();
            // 执行受伤
            mOnTakeDamage?.Invoke(attacker.transform);

        }else {
            mCurrentHealth = 0;
            //dead
            //Debug.Log("charactor dead");
            mOnDead?.Invoke();
        }

        mOnHealthChange?.Invoke(this);

    }
}
