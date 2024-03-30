using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayerAudioEventSO mPlayerAudioEvent;
    public AudioClip mAudioClip;
    public bool mPlayerOnEnable;

    private void OnEnable()
    {
        if (mPlayerOnEnable)
        {
            PlayAudioClip();
        }
    }

    public void PlayAudioClip()
    {
        if(mAudioClip == null || mPlayerAudioEvent ==null)
        {
            Debug.LogFormat("should not be here {0}", gameObject.name);
            return;
        }
        mPlayerAudioEvent.OnEventRaised(mAudioClip);
    }

}
