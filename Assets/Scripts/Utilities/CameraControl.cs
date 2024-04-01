using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D mConfiner2D;
    public CinemachineImpulseSource mImpluseSource;
    public VoidEventSO mCameraShakeEvent;
    public VoidEventSO mSceneAfterLoadEvent;

    private void Awake()
    {
        mConfiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;

        mConfiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        mConfiner2D.InvalidateCache();
    }


    // // Start is called before the first frame update
    // void Start()
    // {
    //     GetNewCameraBounds();
    // }

    private void OnEnable()
    {
        mCameraShakeEvent.mOnEventRaised += OntakeDamage;
        mSceneAfterLoadEvent.mOnEventRaised += OnSceneLoadAfter;
    }

    private void OnSceneLoadAfter()
    {
        GetNewCameraBounds();
    }

    private void OnDisable()
    {
        mCameraShakeEvent.mOnEventRaised -= OntakeDamage;
        mSceneAfterLoadEvent.mOnEventRaised -= OnSceneLoadAfter;
    }

    private void OntakeDamage()
    {
        mImpluseSource.GenerateImpulse();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
