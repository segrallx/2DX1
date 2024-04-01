using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> mLoadRequestEvent;

    public void RiaseLoadRequestEvent(GameSceneSO locationToLoad, Vector3 posToGo, bool fadedScreen)
    {
        mLoadRequestEvent?.Invoke(locationToLoad, posToGo, fadedScreen);
    }
}

