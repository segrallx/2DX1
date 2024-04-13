using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManageBase: MonoBehaviour
{
    public virtual void Init() { }
}


public abstract class ManageBase<T> : ManageBase where T:ManageBase<T>
{
    public static T Instance;

    /// <summary>
    /// ��������ʼ��
    /// </summary>
    public override void Init()
    {
        Instance = this as T;
    }
}
