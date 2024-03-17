using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("事件监听")]
    public CharactorEventSO mHealthEvent;
    public PlayerStateBar mPlayerStateBar;

    private void OnEnable()
    {
        mHealthEvent.mOnEventRaised+=OnHealthEvent;
    }

    private void OnDisable()
    {
        mHealthEvent.mOnEventRaised-=OnHealthEvent;
    }


    private void OnHealthEvent(Charactor charactor)
    {
        var percent = (float)charactor.mCurrentHealth / (float)charactor.mMaxHealth;
        mPlayerStateBar.OnHealthChange(percent);
    }


}
