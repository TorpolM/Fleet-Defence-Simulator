using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDSComputer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TSTC;
    public Vector3[] trackPos;
    public Vector3[] tVectors;
    public float[] eta;
    public bool[] enabled;
    public bool[] inputing;
    public bool release;

    public GameObject view;
    Vector3[] sPos = new Vector3[6];
    float[] dt = new float[6];
    bool[] inputed = new bool[6];
    GameObject ownship;
    Vector3 cursorPos;

    float scale;
    void Start()
    {
        ownship = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        scale = TSTC.GetComponent<TSTC>().scale / 10;
        cursorPos = TSTC.GetComponent<TSTC>().cursorPos;
        cursorPos = ownship.transform.position + new Vector3(cursorPos.x / 550f * scale,cursorPos.z,cursorPos.y / 550f * scale);

        for(int cnt = 0;cnt < 6;cnt++){
            if (inputed[cnt] == false && inputing[cnt] == true){
                sPos[cnt] = cursorPos;
                dt[cnt] = Time.time;
                enabled[cnt] = true;
            }
            if (inputed[cnt] == true && inputing[cnt] == false){
                dt[cnt] = Time.time - dt[cnt];
                tVectors[cnt] = (cursorPos - sPos[cnt]) / dt[cnt];
            }
            if(inputing[cnt]){
                trackPos[cnt] = cursorPos;
            } else {
                trackPos[cnt] += tVectors[cnt] * Time.deltaTime;
                var angle = Vector3.Angle(tVectors[cnt],(ownship.transform.position - trackPos[cnt]));
                eta[cnt] = Vector3.Distance(ownship.transform.position,trackPos[cnt]) / (Mathf.Cos(angle * Mathf.Deg2Rad) * tVectors[cnt].magnitude);
            }
            if(inputing[cnt] && release){
                enabled[cnt] = false;
            }
            if(trackPos[cnt].z > 3000){
                enabled[cnt] = false;
            }
            if(!enabled[cnt]){
                trackPos[cnt] = new Vector3(0,0,-10);
                tVectors[cnt] = new Vector3(0,0,-10);
                eta[cnt] = 0;
            }
            inputed[cnt] = inputing[cnt];
        }
        //view.transform.position = trackPos[0];


    }
}
