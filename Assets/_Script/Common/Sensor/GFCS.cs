using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFCS : MonoBehaviour
{
    public GameObject radar;
    public Vector3 designatePos;
    public float rangeGate;
    public GameObject trackingTarget;
    public bool enable;
    public bool slaving;
    public bool tracking;
    public int direction;
    public Transform target;
    public Vector3 offset;
    public GameObject Turret;
    public GameObject shell;
    GameObject Gun;
    GameObject turretReference;
    Vector3 targetPrePos;
    float Power;
    float minDist;
    Radar_Advanced radarData;
    Quaternion standbyPos;
    public bool isLost;
    bool lastTracking;
    void Start()
    {
        targetPrePos = new Vector3(0,0,0);
        minDist = 100000;
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
            foreach(GameObject target in  radarData.targets){
                if(Vector3.Distance(designatePos,target.transform.position) < (rangeGate / 10)){
                    trackingTarget = target;
                    slaving = false;
                    isLost = false;
                    tracking = true;
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
        } else {
            slaving = false;
            tracking = false;
            trackingTarget = null;
            isLost = false;
            transform.rotation = Quaternion.Slerp(transform.rotation,standbyPos,1f * Time.deltaTime);
            radar.SetActive(false);
        }
        if(tracking){
        Gun = Turret.GetComponent<Turret>().Guns[0];
        turretReference = Turret.GetComponent<Turret>().reference;
        target = trackingTarget.transform;
        Power = Gun.GetComponent<Gun>().initialSpeed / 10;
        if(Turret != null){
            if(target != null){
                var targetPos = target.position + offset;
                var arrivalTime = Vector3.Distance(targetPos,Gun.transform.position) / Power;
                var predictionPos = targetPos + target.GetComponent<Rigidbody>().velocity * arrivalTime;
                turretReference.transform.LookAt(predictionPos);
                var adjacent = Vector3.Distance(Gun.transform.position,predictionPos);
                var fallingFactor =  0.5f * Physics.gravity.y * Mathf.Pow(arrivalTime, 2);
                var counterUpFactor = predictionPos + Vector3.up * fallingFactor;
                var opposite =  Vector3.Distance(predictionPos,counterUpFactor);
                var hypotenuse = Vector3.Distance(Gun.transform.position, counterUpFactor);
                var theta = -Mathf.Acos((Mathf.Pow(hypotenuse, 2) + Mathf.Pow(adjacent, 2) - Mathf.Pow(opposite, 2)) / (2 * hypotenuse * adjacent));
                turretReference.transform.LookAt(predictionPos + Vector3.up * (-1 * fallingFactor));
            } else {
                turretReference.transform.localEulerAngles = new Vector3(-10f,0f,0f);
            }
        }
        }
        lastTracking = tracking;
    }
}
