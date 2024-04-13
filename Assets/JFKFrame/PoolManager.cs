using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : ManageBase<PoolManager>
{
    public override void Init()
    {
        base.Init();
        Debug.Log("pool manager init ok");
    }
}
