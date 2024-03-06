using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator mAnim;
    private Rigidbody2D mRb;

    // Start is called before the first frame update
    private void Awake()
    {
        mAnim = GetComponent<Animator>();
        mRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void  SetAnimation()
    {
        mAnim.SetFloat("velocity_x", Mathf.Abs( mRb.velocity.x));
    }
}
