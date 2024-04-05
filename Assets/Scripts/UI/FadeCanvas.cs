using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeCanvas : MonoBehaviour
{

    public FadeEventSO mFadeEvent;


    public Image mFadeImage;



    private void OnEnable()
    {
        mFadeEvent.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        mFadeEvent.OnEventRaised -= OnFadeEvent;
    }


    private void OnFadeEvent(Color color , float duration, bool fadeIn)
    {
        mFadeImage.DOBlendableColor(color, duration);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
