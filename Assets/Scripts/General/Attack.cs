using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int mDamage;
    public float mAttackRange;
    public float mAttackRate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        collision.GetComponent<Charactor>()?.TakeDamage(this);
    }
}
