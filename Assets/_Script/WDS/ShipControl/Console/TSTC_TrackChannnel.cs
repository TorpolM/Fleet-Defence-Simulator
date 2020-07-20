using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTC_TrackChannnel : MonoBehaviour
{
    public GameObject InputButton;
    public GameObject ResetButton;
    public GameObject Lamp;
    public GameObject cursor;
    public GameObject symbol;

    public string channel;
    public float posX = -330;
    public float posY = 330;
    public float posZ;

    public float dPosX;
    public float dPosY;
    public bool enabled;

    float cursorX;
    float cursorY;
    float sX;
    float sY;
    float vX;
    float vY;
    float dir;
    float dt;
    float time;
    bool lastpushed = false;

    void Start()
    {
        enabled = false;
        dPosX = -330f;
        dPosY = 330f;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = GetComponent<RadarSweep>().scale;

        bool pushed = InputButton.GetComponent<ButtonSwitch>().pushed;



        if (lastpushed == false && pushed == true){
            sX = (cursorX / 445f) * scale;
            sY = (cursorY / 445f) * scale;
            enabled = true;
        }

        if (lastpushed == true && pushed == false){
            vX = (((cursorX / 445f) * scale) - sX) / time;
            vY = (((cursorY / 445f) * scale) - sY) / time;
        }

        if (pushed){
            posX = (cursorX / 445f) * scale;
            posY = (cursorY / 445f) * scale;

            time++;
        } else {
            posX += vX;
            posY += vY;

            time = 0f;
        }

        if (enabled){
            dPosX = (posX / scale) * 445f;
            dPosY = (posY / scale) * 445f;
        }

        if (Mathf.Sqrt((dPosX * dPosX) + (dPosY * dPosY)) > 445){
            dPosX = -330f;
            dPosY = 330f;
        }
        if ((ResetButton.GetComponent<ButtonSwitch>().pushed == true && pushed == true)){
            posX = -330f;
            posY = 330f;
            vX = 0f;
            vY = 0f;
            dPosX = -330f;
            dPosY = 330f;
            enabled = false;
        }


        lastpushed = pushed;


        if (pushed){
            Lamp.GetComponent<Lamp>().enable = true;
        } else {
            Lamp.GetComponent<Lamp>().enable = false;
        }

        symbol.transform.localPosition = new Vector3(dPosX,dPosY,0f);
    }
}
