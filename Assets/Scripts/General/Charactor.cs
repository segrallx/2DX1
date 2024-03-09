using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public int mMaxHealth = 50;
    public int mCurrentHealth;

    [Header(" ‹…ÀŒﬁµ–")]
    public float mInvulnerableDuration;
    private float mInvulnerableCounter;
    public bool mInvulnurable;

    // Start is called before the first frame update
    void Start()
    {
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
        }else {
            mCurrentHealth = 0;
            //dead
            Debug.Log("charactor dead");
        }

    }
}
