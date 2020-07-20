using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScreen : MonoBehaviour
{
    public GameObject antenna;
    public GameObject sweepLine;
     public GameObject blip;
    public float scale;
    public AnimationCurve curve;

    GameObject beam;

    List<GameObject> targets = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sweepLine.transform.localEulerAngles = new Vector3(0f,0f,-1 * antenna.transform.eulerAngles.y - 90f);
        for(int cnt = 0;cnt < Random.Range(8,32);cnt++){
            GameObject echo = Instantiate(blip);
            echo.transform.parent = transform;
            var size = CurveWeightedRandom(curve) * 0.5f + 0.01f;
            echo.transform.localScale = new Vector3(size,size,1f);
            echo.transform.localEulerAngles = new Vector3(0f,0f,0f);
            var dist = CurveWeightedRandom(curve) * 550f;
            var angle = sweepLine.transform.localEulerAngles.z;
            echo.transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * -dist,Mathf.Sin(angle * Mathf.Deg2Rad) * -dist,0f);
            Destroy(echo,size);
        }

        for(int cnt = 0;cnt < antenna.transform.GetChildCount();cnt++){
            beam = antenna.transform.GetChild(cnt).transform.GetChild(0).gameObject;
            targets = beam.GetComponent<Radar_Advanced>().targets;
            for(int cnt2 = 0;cnt2 < targets.Count;cnt2++){
                var target = targets[cnt2];
                GameObject echo = Instantiate(blip);
                echo.transform.parent = transform;
                var size = target.GetComponent<Entity>().RCS / 36;
                 var pos = target.transform.position - antenna.transform.position;
                echo.transform.localScale = new Vector3(size,size,pos.y);
                echo.transform.localEulerAngles = new Vector3(0f,0f,0f);
                if(pos.x / (scale / 10) * 550f < 550 && pos.z / (scale / 10) * 550f < 550f){
                    echo.transform.localPosition = new Vector3(pos.x / (scale / 10) * 550f,pos.z / (scale / 10) * 550f,0f);
                }
                Destroy(echo,size);
            }
        }

    }


    public static float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}
