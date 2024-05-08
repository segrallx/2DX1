using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class JKAudioManager : ManageBase<JKAudioManager>
{
    [SerializeField]
    private AudioSource bgAudioSource;

    [SerializeField]
    private GameObject prefeb_AudioPlay;
    private List<AudioSource> audioPlayerList = new List<AudioSource>();




//    [SerializeField]
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

    private bool isPause;

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
        //Debug.Log("UpdateVfxAudioPlay");
        for(int i= audioPlayerList.Count; i>=0 ; i--)
        {
            if (audioPlayerList[i]!=null)
            {
                SetEffectAudioPlay(audioPlayerList[i]);
            }else
            {
                audioPlayerList.RemoveAt(i);
            }
        }
    }

    private void SetEffectAudioPlay(AudioSource audioSource, float spatial = -1)
    {
        audioSource.mute = isMute;
        audioSource.volume = vfxVolume * globalVolume;
        if(spatial!=-1)
        {
            audioSource.spatialBlend = spatial;
        }

        if(isPause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

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


    private AudioSource GetAudioPlay(bool is3D = true)
    {
        AudioSource audioSource = PoolManager.Instance.GetGameObject<AudioSource>(prefeb_AudioPlay);
        SetEffectAudioPlay(audioSource, is3D ? 1f : 0f);
        audioPlayerList.Add(audioSource);
        return audioSource;
    }

    // 回收播放器
    private void RecycleAudioPlay(AudioSource audioSource, AudioClip clip, UnityAction callback, float time)
    {
        StartCoroutine(DoRecycleAudioPlay(audioSource, clip, callback, time));
    }

    private IEnumerator DoRecycleAudioPlay(AudioSource audioSource, AudioClip clip, UnityAction callback, float time)
    {
        yield return new WaitForSeconds(clip.length);
        if(audioSource!=null)
        {
            audioSource.JKGameObjectPushPool();
        }

        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    // 播放一次特效音乐
    public void PlayOnShot(AudioClip clip,Vector3 pos, float volumeSale = 1, bool is3D = true, 
        UnityAction callback = null, float callBackTime = 0)
    {
        AudioSource audioSource = GetAudioPlay(is3D);
        //audioSource.transform.SetParent(component.transform);
        audioSource.transform.localPosition = pos;// Vector3.zero;
        audioSource.PlayOneShot(clip, volumeSale);

        RecycleAudioPlay(audioSource,clip,callback, callBackTime);
    }

    public void PlayOnShot(AudioClip clip, Component com, float volumeSale = 1, bool is3D = true,
    UnityAction callback = null, float callBackTime = 0)
    {
        AudioSource audioSource = GetAudioPlay(is3D);
        audioSource.transform.SetParent(com.transform);
        audioSource.transform.localPosition = Vector3.zero;
        audioSource.PlayOneShot(clip, volumeSale);

        RecycleAudioPlay(audioSource, clip, callback, callBackTime);
    }

    public void PlayOnShot(string clipPath, Vector3 pos, float volumeSale = 1, bool is3D = true,
    UnityAction callback = null, float callBackTime = 0)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        if(clip!=null)
        {
            PlayOnShot(clip, pos, volumeSale, is3D, callback, callBackTime);
        }
    }

    public void PlayOnShot(string clipPath, Component com, float volumeSale = 1, bool is3D = true,
UnityAction callback = null, float callBackTime = 0)
    {
        AudioClip clip = ResManager.Instance.LoadAsset<AudioClip>(clipPath);
        if (clip != null)
        {
            PlayOnShot(clip, com, volumeSale, is3D, callback, callBackTime);
        }
    }



}
