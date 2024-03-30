using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer mSpriteRender;
    public Sprite mOpenSprite;
    public Sprite mCloseSprite;
    public bool mIsDone;

    void IInteractable.TriggerAction()
    {
        Debug.Log("open chest");
        if(!mIsDone)
        {
            OpenChest();
        }
    }

    private void Awake()
    {
        mSpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        mSpriteRender.sprite = mIsDone ? mOpenSprite : mCloseSprite;
    }

    void OpenChest()
    {
        GetComponent<AudioDefination>()?.PlayAudioClip();
        mSpriteRender.sprite = mOpenSprite;
        mIsDone = true;
        this.gameObject.tag = "Untagged";
    }
}
