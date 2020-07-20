using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialSwitch : MonoBehaviour
{
    public int position;
    public int numPosition;
    public float initAngle;
    public float moveAngle;

    public AudioClip[] sources;
    AudioSource source;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform knob = ((transform.GetChild(0)).transform.GetChild(0)).transform.GetChild(0);
        knob.localEulerAngles = new Vector3(0f,initAngle + moveAngle * position,0f);
    }

    public void SoundCW(){
        AudioClip clip = sources[0];
        source.PlayOneShot(clip);
        position += -1;
    }
    public void SoundCCW(){
        AudioClip clip = sources[1];
        source.PlayOneShot(clip);
        position += 1;
    }
}
