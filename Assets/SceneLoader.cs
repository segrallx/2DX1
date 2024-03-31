using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO mFirstLoadScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(mFirstLoadScene.mSceneRefer, LoadSceneMode.Additive);
    }
}
