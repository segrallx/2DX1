using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{

    private PlayerInputController mPlayerInput;
    public Transform mPlayerTrans;
    private Animator mAnimator;
    public GameObject mSignSprite;
    private bool mCanPress;
    private IInteractable mTargetItem;
    private Collider2D mColl;


    private void Awake()
    {
        mAnimator = GetComponentInChildren<Animator>();
        mPlayerInput = new PlayerInputController();
        mPlayerInput.Enable();
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        mPlayerInput.Gameplay.Confirm.started += OnConfirm;
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (mCanPress)
        {
            mTargetItem.TriggerAction();
        }
    }

    private void OnActionChange(object arg1, InputActionChange change)
    {
        if(change == InputActionChange.ActionStarted)
        {
            var d  = ((InputAction) arg1).activeControl.device;
            switch(d.device)
            {
                case Keyboard:
                    mAnimator.Play("keyboard");
                    break;
            }
        }
    }

    private void Update()
    {
        mSignSprite.GetComponent<SpriteRenderer>().enabled = mCanPress;
        mSignSprite.transform.localScale = mPlayerTrans.localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag( "Interactable"))
        {
            mCanPress = true;
            mTargetItem = collision.GetComponent<IInteractable>();
            mColl = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(mCanPress && collision==mColl)
        {
            mCanPress = false;
        }

    }

}
