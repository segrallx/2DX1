using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class JFKExtends 
{

    public static void JKGameObjectPushPool(this GameObject go)
    {
        PoolManager.Instance.PushGameObject(go);
    }

    public static void JKGameObjectPushPool(this Component go)
    {
        JKGameObjectPushPool(go.gameObject);
    }

    // 数值相等对比
    public static bool ArrayEquals(this object[] objs1, object[] objs2)
    {
        if(objs2==null || objs2.GetType()!= objs1.GetType())
        {
            return false;
        }

        if(objs1.Length!=objs2.Length)
        {
            return false;
        }

        for(int i=0;i<objs1.Length;i++)
        {
            if (!objs1[i].Equals(objs2[i]))
            {
                return false;
            }
        }

        return true;
    }


    public static void JKObjectPushPool(this object obj)
    {
        PoolManager.Instance.PushObject(obj);
    }

}
