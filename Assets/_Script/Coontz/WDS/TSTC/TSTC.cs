using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTC : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WDS;
    public RectTransform screen;
    public GameObject grip;
    public GameObject[] trackButtons;
    public GameObject resetButton;
    public GameObject[] trackLamps;
    public GameObject scaleDial;
    public Vector3 cursorPos;
    public RectTransform[] symbols;
    public float scale;


    RectTransform HI;
    RectTransform heading;
    WDSComputer computer;
    GameObject ownship;

    void Start()
    {
        HI = screen.transform.GetChild(1).transform.GetChild(1).GetComponent<RectTransform>();
        heading = screen.transform.GetChild(2).GetComponent<RectTransform>();
        computer = WDS.GetComponent<WDSComputer>();
        ownship = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame 295
    void Update()
    {
        var range = Mathf.Pow(2,scaleDial.GetComponent<DialSwitch>().position) * 10;
        scale = range * 1.852f * 1000f;
        screen.GetComponent<RadarScreen>().scale = scale;
        cursorPos = screen.transform.InverseTransformPoint(grip.transform.position);
        cursorPos = new Vector3(cursorPos.x,cursorPos.y,0);

        for(int cnt = 2;cnt < screen.transform.childCount;cnt++){
            var dist = Vector3.Distance(screen.transform.GetChild(cnt).transform.localPosition,cursorPos);
            if(dist < 50){
                cursorPos.z = screen.transform.GetChild(cnt).transform.localScale.z;
            }
        }
        
        var alt = cursorPos.z * 10f * 3.04f + 1;
        HI.transform.localPosition = new Vector3(100.5f,alt / 70000 * 295 + -190,-0.002f);

        for(int cnt = 0;cnt < 6;cnt++){
            computer.inputing[cnt] = trackButtons[cnt].GetComponent<ButtonSwitch>().pushed;
            trackLamps[cnt].GetComponent<Lamp>().enable = computer.enabled[cnt];
        }
        computer.release = resetButton.GetComponent<ButtonSwitch>().pushed;

        for(int cnt = 0;cnt < 6;cnt++){
            Vector3 trackPos = computer.trackPos[cnt];
            if(trackPos == new Vector3(0,0,-10)){
                symbols[cnt].transform.localPosition = new Vector3(420,420,0);
            } else {
                symbols[cnt].transform.localPosition = new Vector3(trackPos.x / (scale / 10) * 550f,trackPos.z / (scale / 10) * 550f,0f);
            }
        }

        heading.transform.localEulerAngles = new Vector3(0,0,-1f * ownship.GetComponent<ShipMove>().currentAngle - 90);
    }
}
