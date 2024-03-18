using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D mConfiner2D;
    public CinemachineImpulseSource mImpluseSource;

    public VoidEventSO mCameraShakeEvent;


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


    // Start is called before the first frame update
    void Start()
    {
        GetNewCameraBounds();

    }

    private void OnEnable()
    {
        mCameraShakeEvent.mOnEventRaised += OntakeDamage;
    }

    private void OnDisable()
    {
        mCameraShakeEvent.mOnEventRaised -= OntakeDamage;
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
