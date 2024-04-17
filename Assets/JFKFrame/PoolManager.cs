using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : ManageBase<PoolManager>
{

    [SerializeField]
    private GameObject poolRootObj;

    public Dictionary<string, GameObjectPoolData> gameObjectPoolDic =
        new Dictionary<string, GameObjectPoolData>();

    public Dictionary<string,  ObjectPoolData> objectPoolDic =
        new Dictionary<string, ObjectPoolData>();

    public override void Init()
    {
        base.Init();
    }

    // game Object



    public T GetGameObject<T>(GameObject prefeb, Transfrom parent = null)
        where T : UnityEngine.Object
    {
        GameObject obj = GetGameObject(prefeb,parent);
        if (obj != null)
        {
            return obj.GetComponent<T>();
        }

        return null;
    }


    private bool CheckGameObjectCache(GameObject prefeb)
    {
        string name = prefeb.name;
        return gameObjectPoolDic.ContainsKey(name) && gameObjectPoolDic[name].poolQueue.Count>0;
    }

    public GameObject GetGameObject(GameObject prefeb, Transfrom parent = null)
    {
        GameObject obj = null;
        string name = prefeb.name;
        if(CheckGameObjectCache(prefeb))
        {
            obj = gameObjectPoolDic[name].GetObj(parent);
        }
        else
        {
            obj = GameObject.Instantiate(prefeb);
            obj.name = name;
        }
        return obj;
    }

    public void PushGameObject(GameObject obj)
    {
        string name = obj.name;
        if (gameObjectPoolDic.ContainsKey(name))
        {
            gameObjectPoolDic[name].PushObj(obj);
        }
        else
        {
            gameObjectPoolDic.Add(name, new GameObjectPoolData(obj, poolRootObj));
        }
    }

    // normal Object

    private bool CheckObjectCache<T>()
    {
        string name = typeof(T).FullName;
        return objectPoolDic.ContainsKey(name) && objectPoolDic[name].poolQueue.Count>0;
    }


    public T GetObject<T>() where T : class, new()
    {
        T obj;
        if(CheckObjectCache<T>()) {
            string name = typeof(T).FullName;
            obj = (T) objectPoolDic[name].GetObj();
            return obj;
        } else {
            return new T();
        }
    }


    public void PushObject(object obj)
    {
        string name = typeof(T).FullName;
        if (objectPoolDic.ContainsKey(name))
        {
            objectPoolDic[name].PushObj(obj);
        }
        else
        {
            var newpool = new ObjectPoolData();
            objectPoolDic.Add(name, newpool);
            newpool.PushObj(obj);
        }
    }


    // common
    public void Clear(bool wantClearCObject=true)
    {
        gameObjectPoolDic.Clear();
        if(wantClearCObject) {
            objectPoolDic.Clear();
        }
    }
}
