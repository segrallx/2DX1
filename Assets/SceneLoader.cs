using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO mFirstLoadScene;
    public SceneLoadEventSO mSceneLoadEventSO;


    private Vector3 mPositionToGo;
    private bool mFadedScreen;
    private GameSceneSO mSceneToLoad;
    private GameSceneSO mSceneCurrent;

    public float mFadeDuration;

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
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        mSceneToLoad.mSceneRefer.LoadSceneAsync(LoadSceneMode.Additive, true) ;
    }
}
