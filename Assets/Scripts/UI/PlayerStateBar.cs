using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{

    public Image mHealthImage;
    public Image mHealthDelayImage;
    public Image mPowerImage;


    // µ÷ÕûÑªÁ¿
    public void OnHealthChange(float percent)
    {
        mHealthImage.fillAmount = percent;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(mHealthDelayImage.fillAmount > mHealthImage.fillAmount) {
            mHealthDelayImage.fillAmount -= Time.deltaTime;
        }
    }
}
