using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectPoolData
{
    //
    public GameObject fatherObj;
    //list
    public Queue<GameObject> poolQueue;


    public GameObjectPoolData(GameObject obj, GameObject poolRootObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.SetParent(poolRootObj.transform);
        this.poolQueue = new Queue<GameObject>();
        PushObj(obj);
    }


    public void PushObj(GameObject obj)
    {
        poolQueue.Enqueue(obj);
        obj.transform.SetParent(fatherObj.transform);
        obj.SetActive(false);
    }

    public GameObject GetObj()
    {
        GameObject obj = poolQueue.Dequeue();
        obj.SetActive(true);
        obj.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
        return obj;
    }
}
