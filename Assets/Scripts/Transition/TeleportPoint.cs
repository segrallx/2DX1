using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IInteractable
{

    public Vector3 mPositionToGo;

    // Start is called before the first frame update
    void Start()
    {
    }

    void IInteractable.TriggerAction()
    {
        Debug.Log("teleport");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
