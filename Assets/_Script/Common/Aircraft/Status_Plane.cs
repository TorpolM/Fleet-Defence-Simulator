using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Plane : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed;
    public float maxTurnRate;
    public float maxAltitude;
    public float fuel;
    public float sensorRange;
    public GameObject internalWeapons;
    public int weaponCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuel += -1 * Time.deltaTime;
    }
}
