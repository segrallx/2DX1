using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ResManager: ManageBase<ResManager>
{
    private Dictionary<Type, bool> wantCacheDic;

    public override void Init()
    {
        base.Init();
        //wantCacheDic = new Dictionary<Type, bool>();
        wantCacheDic = GameRoot.Instance.GameSetting.wantCacheDic;
    }


    private bool CheckCacheDic(Type type)
    {
        return wantCacheDic.ContainsKey(type);
    }

    // 加载unity资源如autoclip sprite
    public T LoadAsset<T>(string path) where T:UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    public T Load<T>() where T : class, new()
    {
        if(CheckCacheDic(typeof(T))) {
            return PoolManager.Instance.GetObject<T>();
        }else {
            return new T();
        }
    }

    // 异步加载游戏物体.
    public void LoadGameObjectAsync<T>(string path, Action<T> callBack = null, 
        Transform parent = null) where T: UnityEngine.Object
    {
        // 考虑对象池.
        if (CheckCacheDic(typeof(T))) 
        {
            GameObject go = PoolManager.Instance.CheckCacheAndLoadGameObject(path, parent);
            if(go !=null)
            {
                callBack?.Invoke(go.GetComponent<T>());
            }
            else
            {
                StartCoroutine(DoLoadGameObjectAsync<T>(path, callBack, parent));
            }
        }
        else
        {
            StartCoroutine(DoLoadGameObjectAsync<T>(path, callBack, parent));
        }
    }

    IEnumerator DoLoadGameObjectAsync<T>(string path, Action<T> callBack = null,
    Transform parent = null) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(path);
        yield return request;
        GameObject go = InstantiateForPrefeb(request.asset as GameObject, parent);
        //go.name = prefebName;
        callBack?.Invoke(go.GetComponent<T>());
    }


    // 异步加载unity资源.
    public void LoadAssetAsync<T>(string path, Action<T> callBack) where T:UnityEngine.Object
    {
        StartCoroutine(DoLoadAssetAsync(path, callBack));
    }

    private IEnumerator DoLoadAssetAsync<T>(string path, Action<T> callBack) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        yield return request;
        callBack?.Invoke(request.asset as T);
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
