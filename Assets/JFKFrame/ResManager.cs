using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResManager: ManagerBase<ResManager>
{
    private Dictionary<Type, bool> wantCacheDic;

    public override void Init()
    {
        base.Init();
        wantCacheDic = new Dictionary<Type, bool>();
    }


    private bool CheckCacheDic(Type type)
    {
        return wantCacheDic.ContainsKey(type);
    }

    public T Load<T>() where T : class, new()
    {
        if(CheckCacheDic(typeof(T))) {
            return PoolManager.Instance.GetObject<T>();
        }else {
            return new T();
        }
    }

    public T Load<T>(string path, Transform parent =null) where T: Component
    {
        if(CheckCacheDic(typeof(T))) {
            return PoolManager.Instance.GetGameObject<T>(GetPrefeb(path), parent);
        }else {
            return InstantiateForPrefeb(path,parent).GetComponent<T>();
        }
    }


    public GameObject GetPrefeb(string path)
    {
        GameObject prefeb = Resources.Load<GameObject>(path);
        if(prefeb==null) {
            throw new Exception($"JK prefeb path error {path}");
        }

        return prefeb;
    }

    public GameObject InstantiateForPrefeb(string path, Transform parent =null)
    {
        return InstantiateForPrefeb(GetPrefeb(path), parent);
    }

    public GameObject InstantiateForPrefeb(GameObject prefeb, Transform parent =null)
    {
        GameObject go = GameObject.Instantiate<GameObject>(prefeb,parent);
        go.name = prefeb.name;
        return go;
    }
}
