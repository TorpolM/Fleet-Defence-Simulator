using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMFCS : MonoBehaviour
{
    public GameObject radar;
    public GameObject cScanAxis;
    public Vector3 designatePos;
    public float rangeGate;
    public GameObject trackingTarget;
    public bool enable;
    public bool slaving;
    public bool tracking;
    public int direction;
    public bool isGuiding;
    public bool isLost;
    
    public List<GameObject> missiles = new List<GameObject>();

    Radar_Advanced radarData;
    bool lastTracking;
    Quaternion standbyPos;
    void Start()
    {
        radarData = radar.GetComponent<Radar_Advanced>();
        standbyPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(enable){
            radar.SetActive(true);
            if(slaving){
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction * designatePos),0.5f * Time.deltaTime);
            }
            trackingTarget = null;
            foreach(GameObject target in radarData.targets){
                if(Vector3.Distance(designatePos,target.transform.position) < (rangeGate / 10)){
                    trackingTarget = target;
                    slaving = false;
                    tracking = true;
                    isLost = false;
                }
            }
            if(tracking && trackingTarget != null){
                designatePos = trackingTarget.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction * trackingTarget.transform.position),1f * Time.deltaTime);
            }
            if(trackingTarget == null){
                tracking = false;
            }
            if(lastTracking && !tracking){
                isLost = true;
            }
            if(missiles.Count > 0){
                isGuiding = true;
            } else {
                isGuiding = false;
            }
            if(cScanAxis != null){
                cScanAxis.transform.localEulerAngles += new Vector3(0,0,1800 * Time.deltaTime);
            }
        } else {
            trackingTarget = null;
            slaving = false;
            isLost = false;
            tracking = false;
            transform.rotation = Quaternion.Slerp(transform.rotation,standbyPos,1f * Time.deltaTime);
            radar.SetActive(false);
        }
        missiles.Clear();
        lastTracking = tracking;
    }
}
