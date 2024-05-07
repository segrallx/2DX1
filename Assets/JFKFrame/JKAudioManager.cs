using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JKAudioManager : ManageBase<JKAudioManager>
{
    [SerializeField]
    private AudioSource bgAudioSource;

    [SerializeField]
    private AudioSource vfxAudioSource;

    [SerializeField]
    [Range(0,1)]
    [OnValueChanged("UpdateGlobalAudioPlay")]
    private float globalVolume;
    public float GlobalVolume { get => globalVolume; set  { globalVolume = value; UpdateGlobalAudioPlay(); } }

    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateBGAudioPlay")]
    private float bgVolume;
    public float BgVolume { get => bgVolume; set { bgVolume = value; UpdateGlobalAudioPlay(); } }

    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateVfxAudioPlay")]
    private float vfxVolume;
    public float VfxVolume { get => vfxVolume; set { vfxVolume = value;  UpdateGlobalAudioPlay(); } }


    [SerializeField]
    [OnValueChanged("UpdateMute")]
    private bool isMute = false;
    public bool IsMute { 
        get => isMute;
        set
        {
            isMute = value;
            UpdateMute();
        }
    }

    private void UpdateMute()
    {
        bgAudioSource.mute = isMute;
        UpdateVfxAudioPlay();
    }

    [SerializeField]
    [OnValueChanged("UpdateLoop")]
    private bool isLoop = true;
    public bool IsLoop
    {
        get => isLoop;
        set
        {
            isLoop = value;
//            bgAudioSource.loop = isLoop;
        }
    }

    private void UpdateLoop()
    {
        bgAudioSource.loop = isLoop;
    }


    private void UpdateBGAudioPlay()
    {
        bgAudioSource.volume = bgVolume * globalVolume;
    }

    private void UpdateVfxAudioPlay()
    {
        //vfxAudioSource.volume = vfxVolume * globalVolume;
        Debug.Log("UpdateVfxAudioPlay");
    }

    private void UpdateGlobalAudioPlay()
    {
        Debug.Log("UpdateGlobalAudioPlay");
        UpdateBGAudioPlay();
        UpdateVfxAudioPlay();
    }

    public void PlayBgAudio(AudioClip clip, bool isLoop= true, float volume = -1)
    {
        bgAudioSource.clip = clip;
        this.isLoop = isLoop;
        if(volume!=-1)
        {
            BgVolume = volume;
        }

        bgAudioSource.Play();
    }

    public void PlayBgAudio(string clipPath, bool isLoop = true, float volume = -1)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        PlayBgAudio(clip, isLoop, volume);
    }


}
