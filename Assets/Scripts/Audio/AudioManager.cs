using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("�¼�����")]
    public PlayerAudioEventSO mFXEvent;
    public PlayerAudioEventSO mBGMEvent;

    [Header("���")]

    public AudioSource mBgmSource;
    public AudioSource mFxSource;


    private void OnEnable()
    {
        mFXEvent.OnEventRaised += OnFXEvent;
        mBGMEvent.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        mFXEvent.OnEventRaised -= OnFXEvent;
        mBGMEvent.OnEventRaised -= OnBGMEvent;

    }

    private void OnBGMEvent(AudioClip clip)
    {
        mBgmSource.clip = clip;
        mBgmSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        mFxSource.clip = clip;
        mFxSource.Play();
    }


}
