using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    // public override void Move()
    // {
    //     base.Move();
    //     mAnim.SetBool("walk", true);
    // }

    protected override void Awake()
    {
        base.Awake();
        mPatrolState = new BarPatrolState();
    }

}
