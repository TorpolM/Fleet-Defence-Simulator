using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantaograph : MonoBehaviour
{
    public GameObject upperArm;
    public GameObject forwardArm;
    public GameObject grip;
    public GameObject mid;

    float ru;
    float rf;
    Vector3 origin = new Vector3(0.002042909f,0.05713859f,0.005f);
    // Start is called before the first frame update
    void Start()
    {
        ru = Vector3.Distance(transform.InverseTransformPoint(upperArm.transform.position),transform.InverseTransformPoint(forwardArm.transform.position));
        rf = Vector3.Distance(transform.InverseTransformPoint(forwardArm.transform.position),transform.InverseTransformPoint(grip.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        var x2 = transform.InverseTransformPoint(grip.transform.position).x;
        var y2 = transform.InverseTransformPoint(grip.transform.position).z;
        float r2 = rf * 1.001f;
        var x1 = transform.InverseTransformPoint(upperArm.transform.position).x;
        var y1 = transform.InverseTransformPoint(upperArm.transform.position).z;
        float r1 = ru * 1.001f;
       
        var l = Mathf.Sqrt(Mathf.Pow(x2 - x1,2) + Mathf.Pow(y2 - y1,2));
        var theta1 = Mathf.Atan2(y2 - y1,x2 - x1);
        var theta2 = Mathf.Acos((Mathf.Pow(l,2) + Mathf.Pow(r1,2) - Mathf.Pow(r2,2)) / (2 * l * r1));
        var xi1 = x1 + r1 * Mathf.Cos(theta1 + theta2);
        var yi1 = y1 + r1 * Mathf.Sin(theta1 + theta2);
        var xi2 = x1 + r1 * Mathf.Cos(theta1 - theta2);
        var yi2 = y1 + r1 * Mathf.Sin(theta1 - theta2);

        mid.transform.localPosition = new Vector3 (xi1,0f,yi1);

        var angle = Vector3.SignedAngle(upperArm.transform.up,mid.transform.position - upperArm.transform.position,upperArm.transform.forward);
        upperArm.transform.localEulerAngles = new Vector3(0f,0f,upperArm.transform.localEulerAngles.z + angle);
        angle = Vector3.SignedAngle(transform.InverseTransformVector(forwardArm.transform.up),grip.transform.localPosition - transform.InverseTransformPoint(forwardArm.transform.position),forwardArm.transform.forward);
        if(angle < 0){
            angle += 360;
        }
        forwardArm.transform.localEulerAngles = new Vector3(0f,0f,forwardArm.transform.localEulerAngles.z + angle);
    }
}
