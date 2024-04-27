using System;
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



    public T GetGameObject<T>(GameObject prefeb, Transform parent = null)
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

    // 检查缓存以及加载游戏物体
    public GameObject CheckCacheAndLoadGameObject(string path, Transform parent)
    {
        // 通过路径获得最终预制体的名称 "Prefeb/X1.prefeb"
        string[] pathSplit = path.Split("/");
        string prefebName = pathSplit[pathSplit.Length - 1];

        //对象池有数据.
        if (gameObjectPoolDic.ContainsKey(prefebName) && gameObjectPoolDic[prefebName].poolQueue.Count>0)
        {
            return gameObjectPoolDic[prefebName].GetObj(parent);
        }
        else
        {
            return null;
        }
    }

    public GameObject GetGameObject(GameObject prefeb, Transform parent = null)
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


    public void PushObject<T>(object obj)
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


    #region 删除

    // common
    public void Clear(bool clearGameObject = true,bool clearObject=true)
    {
        if(clearGameObject)
        {
            gameObjectPoolDic.Clear();
            for(int i = 0; i < poolRootObj.transform.childCount; i++)
            {
                Destroy(poolRootObj.transform.GetChild(0).gameObject);
            }
        }

        if(clearObject) {
            objectPoolDic.Clear();
        }
    }

    public void ClearGameObject()
    {
        Clear(true, false);
    }

    public void ClearGameObject(string prefebName)
    {
        GameObject go = poolRootObj.transform.Find(prefebName).gameObject;
        if (go != null)
        {
            Destroy(go.gameObject);
            gameObjectPoolDic.Remove(prefebName);
        }
    }

    public void ClearGameObject(GameObject prefeb)
    {
        ClearGameObject(prefeb.name);
    }

    public void ClearObject()
    {
        Clear(false, true);
    }

    public void ClearObject<T>()
    {
        objectPoolDic.Remove(typeof(T).FullName);
    }

    public void ClearObject(Type t)
    {
        objectPoolDic.Remove(t.FullName);
    }


    #endregion
}
