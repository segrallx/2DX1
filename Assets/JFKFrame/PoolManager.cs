using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : ManageBase<PoolManager>
{

    [SerializeField]
    private GameObject poolRootObj;

    public Dictionary<string, GameObjectPoolData> gameObjectPoolDic = 
        new Dictionary<string, GameObjectPoolData>();

    public override void Init()
    {
        base.Init();
    }

    public T GetGameObject<T>(GameObject prefeb) where T : UnityEngine.Object
    {
        GameObject obj = GetGameObject(prefeb);
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

    public GameObject GetGameObject(GameObject prefeb)
    {
        GameObject obj = null;
        string name = prefeb.name;
        if(CheckGameObjectCache(prefeb))
        {
            obj = gameObjectPoolDic[name].GetObj();
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

    public void Clear()
    {
        gameObjectPoolDic.Clear();
    }
}
