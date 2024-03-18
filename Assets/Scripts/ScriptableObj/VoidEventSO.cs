using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction mOnEventRaised;

    public void RaiseEvent()
    {
        mOnEventRaised?.Invoke();
    }
}
