using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSweep : MonoBehaviour
{
    public GameObject Blips;

    public GameObject RadarAnttena;
    public GameObject[] Beams = new GameObject[18];
    public GameObject SwitchRange;
    
    public GameObject SweepLine;
    public GameObject cursor;
    public GameObject HI;


    public float scale = 3500f;
    public List<GameObject> echoList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SweepLine.transform.localEulerAngles = new Vector3(SweepLine.transform.localEulerAngles.x,SweepLine.transform.localEulerAngles.y,-1f * RadarAnttena.transform.localEulerAngles.y);


        if (SwitchRange.GetComponent<DialSwitch>().position == 0){
			scale = 4630f;
		}
		if (SwitchRange.GetComponent<DialSwitch>().position == 1){
			scale = 9260f;
		}
		if (SwitchRange.GetComponent<DialSwitch>().position == 2){
			scale = 18520f;
		}


        for(int cnt = 0;cnt < 18;cnt++){
            GameObject Beam = Beams[cnt];
            foreach(GameObject contact in Beam.GetComponent<BeamContact>().contactList){
                float rawDist = Vector3.Distance(RadarAnttena.transform.position,contact.transform.position);
                float PPIDist = rawDist * Mathf.Cos((cnt * 2.4f) * Mathf.Deg2Rad);
                float Elevation = rawDist * Mathf.Sin((cnt * 2.4f) * Mathf.Deg2Rad);

                if ((PPIDist / scale) * 512f < 450f){
                    GameObject echo = Instantiate (Blips) as GameObject;
                    echo.transform.parent = transform;
                    echo.transform.SetAsFirstSibling();
                    echo.transform.localScale = new Vector3 (2f,1f + 3f * (PPIDist / scale),2f);

                    var angle = -1f * RadarAnttena.transform.localEulerAngles.y + 90f;

                    echo.transform.localEulerAngles = new Vector3 (0f, 0f, angle);
                    echo.transform.localPosition = new Vector3 (((PPIDist / scale) * 512f) * Mathf.Cos(angle * Mathf.Deg2Rad),((PPIDist / scale) * 512f) * Mathf.Sin(angle * Mathf.Deg2Rad),0);
                    echo.GetComponent<BlipFade>().Elevation = Elevation;
                    echoList.Add(echo);
                }
            }
        }


        

    }
}
