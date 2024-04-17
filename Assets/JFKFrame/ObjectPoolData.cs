using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolData
{
    public Queue<object> poolQueue;
    public void PushObj(object obj)
    {
        poolQueue.Enqueue(obj);
    }

    public object GetObj()
    {
        object obj = poolQueue.Dequeue();
        return obj;
    }
}
