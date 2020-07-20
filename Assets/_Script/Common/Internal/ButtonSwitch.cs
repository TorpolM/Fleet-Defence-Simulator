using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public bool pushed;
    Transform knob;
    private AudioSource[] sources;
    void Start()
    {
        knob = ((transform.GetChild(0)).transform.GetChild(0)).transform.GetChild(0);
        sources = gameObject.GetComponents<AudioSource>();
    }
    void FixedUpdate()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pushed){
            knob.localPosition = new Vector3(0f,0.0036f,0f);
        } else {
            knob.localPosition = new Vector3(0f,0.007183165f,0f);
        }
        pushed = false;
    }
}
