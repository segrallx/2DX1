using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{
    public Vector3 mPositionToGo;
    public GameSceneSO mSceneToGo;
    public SceneLoadEventSO mLoadEventSO;

    // Start is called before the first frame update
    void Start()
    {
    }

    void IInteractable.TriggerAction()
    {
        Debug.Log("teleport");
        mLoadEventSO.RiaseLoadRequestEvent(mSceneToGo, mPositionToGo, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
