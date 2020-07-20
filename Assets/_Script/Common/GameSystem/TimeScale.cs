using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeScale : MonoBehaviour
{
    public Slider sliderScale; 
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderScale.value == -2){
            scale = 0;
        }
        if(sliderScale.value == -1){
            scale = 0.5f;
        }
        if(sliderScale.value == 0){
            scale = 1;
        }
        if(sliderScale.value == 1){
            scale = 2;
        }
        if(sliderScale.value == 2){
            scale = 4;
        }
        Time.timeScale = scale;
    }
}
