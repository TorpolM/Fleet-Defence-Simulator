using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAC : MonoBehaviour
{
    public GameObject WDS;
    public GameObject[] FCS;
    public GameObject[] sourceButton;
    public GameObject sourceResetButton;
    public GameObject[] designateButton;
    public GameObject designateResetButton;
    public GameObject scaleSw;
    public GameObject[] trackLamps;
    public GameObject[] FCSLamps;
    public GameObject[] FCSEnableLamp;
    public GameObject[] FCSSlaveLamp;
    public GameObject[] FCSTrackLamp;
    public GameObject[] FCSGuidingLamp;
    public GameObject[] FCSLostLamp;
    public RectTransform[] FCSsymbols;
    public RectTransform[] symbols;
    public RectTransform screen;
    public RectTransform screen2;

    float scale;
    RectTransform heading;
    WDSComputer computer;
    GameObject ownship;
    int channel = -1;
    int[] channels = new int[5];
    RectTransform[] mpdSymbols = new RectTransform[18];
    void Start()
    {
        heading = screen.transform.GetChild(0).GetComponent<RectTransform>();
        computer = WDS.GetComponent<WDSComputer>();
        ownship = GameObject.FindGameObjectWithTag("Player");
        for(int cnt = 0;cnt < 18;cnt++){
            mpdSymbols[cnt] = screen2.transform.GetChild(cnt + 1).GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float range;
        channel = -1;
        if(scaleSw.GetComponent<Switch>().position == 0){
            range = 80;
        } else {
            range = 160;
        }
        scale = range * 1.852f * 1000f;
        for(int cnt = 0;cnt < 6;cnt++){
            Vector3 trackPos = computer.trackPos[cnt];
            if(trackPos == new Vector3(0,0,-10)){
                symbols[cnt].transform.localPosition = new Vector3(420,420,0);
            } else {
                symbols[cnt].transform.localPosition = new Vector3(trackPos.x / (scale / 10) * 550f,trackPos.z / (scale / 10) * 550f,0f);
            }
        }
        for(int cnt = 0;cnt < 6;cnt++){
            if(sourceButton[cnt].GetComponent<ButtonSwitch>().pushed){
                channel = cnt;
                trackLamps[cnt].GetComponent<Lamp>().enable = true;
            } else {
                trackLamps[cnt].GetComponent<Lamp>().enable = false;
            }
        }
        for(int cnt = 0;cnt < 3;cnt++){
            if(channel > -1 && designateButton[cnt].GetComponent<ButtonSwitch>().pushed){
                FCS[cnt].GetComponent<GFCS>().enable = true;
                FCS[cnt].GetComponent<GFCS>().slaving = true;
                channels[cnt] = channel;
                FCSLamps[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSLamps[cnt].GetComponent<Lamp>().enable = false;
            }
            if(designateResetButton.GetComponent<ButtonSwitch>().pushed && designateButton[cnt].GetComponent<ButtonSwitch>().pushed){
                FCS[cnt].GetComponent<GFCS>().enable = false;
                FCS[cnt].GetComponent<GFCS>().slaving = false;
            }
            if(FCS[cnt].GetComponent<GFCS>().enable){
                FCSEnableLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSEnableLamp[cnt].GetComponent<Lamp>().enable = false;
            }
            if(FCS[cnt].GetComponent<GFCS>().slaving){
                FCS[cnt].GetComponent<GFCS>().designatePos = computer.trackPos[channels[cnt]];
                FCSSlaveLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSSlaveLamp[cnt].GetComponent<Lamp>().enable = false;
            }
            if(FCS[cnt].GetComponent<GFCS>().tracking){
                FCSTrackLamp[cnt].GetComponent<Lamp>().enable = true;
                FCSsymbols[cnt].transform.localPosition = new Vector3(FCS[cnt].GetComponent<GFCS>().designatePos.x / (scale / 10) * 550f,FCS[cnt].GetComponent<GFCS>().designatePos.z / (scale / 10) * 550f,0f);
            } else {
                FCSTrackLamp[cnt].GetComponent<Lamp>().enable = false;
                FCSsymbols[cnt].transform.localPosition = new Vector3(420,420,0);
            }
            if(FCS[cnt].GetComponent<GFCS>().isLost){
                FCSLostLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSLostLamp[cnt].GetComponent<Lamp>().enable = false;
            }

        }
        for(int cnt = 3;cnt < 5;cnt++){
            if(channel > -1 && designateButton[cnt].GetComponent<ButtonSwitch>().pushed){
                FCS[cnt].GetComponent<GMFCS>().enable = true;
                FCS[cnt].GetComponent<GMFCS>().slaving = true;
                channels[cnt] = channel;
                FCSLamps[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSLamps[cnt].GetComponent<Lamp>().enable = false;
            }
            if(designateResetButton.GetComponent<ButtonSwitch>().pushed && designateButton[cnt].GetComponent<ButtonSwitch>().pushed){
                FCS[cnt].GetComponent<GMFCS>().enable = false;
                FCS[cnt].GetComponent<GMFCS>().slaving = false;
            }
            if(FCS[cnt].GetComponent<GMFCS>().enable){
                FCSEnableLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSEnableLamp[cnt].GetComponent<Lamp>().enable = false;
            }
            if(FCS[cnt].GetComponent<GMFCS>().slaving){
                FCS[cnt].GetComponent<GMFCS>().designatePos = computer.trackPos[channels[cnt]];
                FCSSlaveLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSSlaveLamp[cnt].GetComponent<Lamp>().enable = false;
            }
            if(FCS[cnt].GetComponent<GMFCS>().tracking){
                FCSTrackLamp[cnt].GetComponent<Lamp>().enable = true;
                FCSsymbols[cnt].transform.localPosition = new Vector3(FCS[cnt].GetComponent<GMFCS>().designatePos.x / (scale / 10) * 550f,FCS[cnt].GetComponent<GMFCS>().designatePos.z / (scale / 10) * 550f,0f);
            } else {
                FCSTrackLamp[cnt].GetComponent<Lamp>().enable = false;
                FCSsymbols[cnt].transform.localPosition = new Vector3(420,420,0);
            }
            if(FCS[cnt].GetComponent<GMFCS>().isLost){
                FCSLostLamp[cnt].GetComponent<Lamp>().enable = true;
            } else {
                FCSLostLamp[cnt].GetComponent<Lamp>().enable = false;
            }
            if(FCS[cnt].GetComponent<GMFCS>().isGuiding){
                FCSGuidingLamp[cnt - 3].GetComponent<Lamp>().enable = true;
            } else {
                FCSGuidingLamp[cnt - 3].GetComponent<Lamp>().enable = false;
            }
        }
        heading.transform.localEulerAngles = new Vector3(0,0,-1f * ownship.GetComponent<ShipMove>().currentAngle - 90);
        for(int cnt = 0;cnt < 6;cnt++){
            var spd = computer.tVectors[cnt].magnitude * 0.514f * 10f;
            spd = spd * 0.458f;
            mpdSymbols[cnt+12].transform.localPosition = new Vector3(mpdSymbols[cnt+12].transform.localPosition.x,-368 + spd,mpdSymbols[cnt+12].transform.localPosition.z);
            var alt = computer.trackPos[cnt].y * 10f * 3.04f + 1;
            mpdSymbols[cnt+6].transform.localPosition = new Vector3(mpdSymbols[cnt+6].transform.localPosition.x,alt * 0.0046f -276f,mpdSymbols[cnt+6].transform.localPosition.z);
            int eta = (int)(computer.eta[cnt] * 0.65) + 13;
            string bar = "";
            for(int cnt2 = 0;cnt2 < eta;cnt2++){
                bar += "-";
            }
            mpdSymbols[cnt].GetComponent<Text>().text = bar;
        }
    }
}
