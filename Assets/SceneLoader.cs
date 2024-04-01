using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO mFirstLoadScene;
    public SceneLoadEventSO mSceneLoadEventSO;
    public Transform mPlayerTrans;

    public VoidEventSO mAfterSceneLoadEvent;


    private Vector3 mPositionToGo;
    private bool mFadedScreen;
    private GameSceneSO mSceneToLoad;
    private GameSceneSO mSceneCurrent;

    public float mFadeDuration;
    private bool mIsLoading;

    private void Awake()
    {
        //Addressables.LoadSceneAsync(mFirstLoadScene.mSceneRefer, LoadSceneMode.Additive);
        mSceneCurrent = mFirstLoadScene;
        mSceneCurrent.mSceneRefer.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        mSceneLoadEventSO.mLoadRequestEvent += OnLoadRequestEventSO;
    }

    private void OnDisable()
    {
        mSceneLoadEventSO.mLoadRequestEvent -= OnLoadRequestEventSO;
    }

    private void OnLoadRequestEventSO(GameSceneSO locationToLoad, Vector3 postToGo, bool fadedScreen)
    {
        if (mIsLoading)
            return;

        mIsLoading = true;
        mSceneToLoad = locationToLoad;
        mPositionToGo = postToGo;
        mFadedScreen = fadedScreen;


        Debug.Log(mSceneToLoad.mSceneRefer.SubObjectName);
        StartCoroutine(UnLoadPreviousScene());
    }

    private IEnumerator UnLoadPreviousScene()
    {
        if(mFadedScreen)
        {
        }

        yield return new WaitForSeconds(mFadeDuration);

        if(mSceneCurrent!=null)
        {
           yield return mSceneCurrent.mSceneRefer.UnLoadScene();
        }

        mPlayerTrans.gameObject.SetActive(false);
        LoadNewScene();

    }

    private void LoadNewScene()
    {
       var loadingOption =  mSceneToLoad.mSceneRefer.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadComplete;
    }

    /// <summary>
    /// 加载结束之后
    /// </summary>
    /// <param name="handle"></param>
    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        mSceneCurrent = mSceneToLoad;
        mPlayerTrans.position = mPositionToGo;
        mPlayerTrans.gameObject.SetActive(true);

        if(mFadedScreen)
        {
            //TODO:
        }

        mIsLoading = false;
        mAfterSceneLoadEvent.RaiseEvent();
    }
}
