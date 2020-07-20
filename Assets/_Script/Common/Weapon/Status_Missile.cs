using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Missile : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;    //0:anti-air 1:anti-surface
    public int guidance;    //0:SARH 1:ARH,IR 2:unguided
    public float seekerFOV;
    public float seekerRange;
    public float maxSpeed;
    public float maxTurnRate;
    public float Range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
