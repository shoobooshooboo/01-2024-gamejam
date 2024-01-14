using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FearSlider : MonoBehaviour
{
    private BoolHolder boolHolder;
    public GameObject fearSlider;
    public Image sliderFill;
    private TextMeshProUGUI fearText;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        boolHolder = GetComponentInParent<BoolHolder>();
        slider = fearSlider.GetComponent<Slider>();
        fearText = slider.GetComponentInChildren<TextMeshProUGUI>();
        slider.value = 0f;
        fearSlider.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(slider.value >= 1f){
            fearText.text = "FROZEN";
        }
        if ((int)(boolHolder.fearSlider * 10000) > (int)(slider.value * 10000))
        {
            if(slider.value == 0f) {
                fearSlider.SetActive(true);
            }
            slider.value += .01f;
        }
        else if((int)(boolHolder.fearSlider * 10000) < (int)(slider.value * 10000))
        {
            slider.value -= .005f;
        }
        //Debug.Log("direct value: " + slider.value);
        //Debug.Log("held value: " + boolHolder.fearSlider);
    }
}
