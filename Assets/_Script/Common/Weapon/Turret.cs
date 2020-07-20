using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Barrel;
    public GameObject[] Guns;
    public GameObject reference;
    public Quaternion Angle;
    public float maxTurnRate = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotation = Vector3.Lerp(new Vector3(Barrel.transform.localEulerAngles.x,transform.localEulerAngles.y,0f),reference.transform.localEulerAngles,10f * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0f,rotation.y,0f);
        Barrel.transform.localEulerAngles = new Vector3(rotation.x,0f,0f);
    }
}
