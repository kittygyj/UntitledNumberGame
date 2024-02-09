using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    [SerializeField] float flashingSpeed = 0.7f;

    [SerializeField] float a=0;
    float flashingtimer;
    bool isFading=true;
    // Start is called before the first frame update
    void Start()
    {
        flashingtimer = flashingSpeed;
        //float alpha =  gameObject.GetComponent<TMP_Text>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        a=gameObject.GetComponent<TMP_Text>().color.a;
        flashingtimer-=Time.deltaTime;
        if(flashingtimer<0)
        {
            flashingtimer=0;
            isFading = !isFading;
            flashingtimer=flashingSpeed;
            
        }
        else
        {
            if(isFading)
            {
                float alpha = 255*flashingtimer/flashingSpeed;
                Color32 c = gameObject.GetComponent<TMP_Text>().color;
                c.a = (byte)alpha;
                gameObject.GetComponent<TMP_Text>().color = c;
            }
            else
            {
                float alpha = 255*(flashingSpeed-flashingtimer)/flashingSpeed;
                Color32 c = gameObject.GetComponent<TMP_Text>().color;
                c.a = (byte)alpha;
                gameObject.GetComponent<TMP_Text>().color = c;
            }
        }
    }
}