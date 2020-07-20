using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaSweep : MonoBehaviour
{
    public float RPM;
    public bool isRotate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0f,RPM / 60 * 360 * Time.deltaTime,0f);
    }
}
