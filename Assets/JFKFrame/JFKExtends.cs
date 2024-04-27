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

    /*
    public static void JKObjectPushPool(this object obj)
    {
        PoolManager.Instance.PushObject(obj);

    }
    */

}
