using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_radarRotate : MonoBehaviour
{
    public float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y + RotateSpeed,transform.localEulerAngles.z);
    }
}
