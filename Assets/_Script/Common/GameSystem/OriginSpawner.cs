using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public bool start;
    public bool complete;
    public int side;
    public GameObject[] target;
    public GameObject[] unitType;
    public int waveSize;
    public float waveInterval;
    public int waveCount;
    public float spawnRadius;   //m;
    public float spawmAltitude;     //m;

    float lastTime;
    int count = 0;
    void Start()
    {
        lastTime = 0f;
        complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        var rand = new System.Random();

        if((Time.time - lastTime) > waveInterval && count < waveCount){
            for(int cnt = 0;cnt < waveSize;cnt++){
                GameObject plane = Instantiate(unitType[rand.Next(unitType.Length)]);
                var offset = Random.insideUnitCircle * (spawnRadius / 10);
                plane.transform.parent = transform;
                plane.transform.localPosition = new Vector3(0f,spawmAltitude / 10,0f);
                plane.transform.localEulerAngles = new Vector3(0f,0f,0f);
                plane.transform.localPosition += new Vector3(offset.x,0f,offset.y);
                plane.transform.parent = null;
                plane.GetComponent<Entity>().side = side;
                plane.GetComponent<AI_Plane_Attacker>().targetPos = target[rand.Next(target.Length)].transform.position;
                plane.GetComponent<AI_Plane_Attacker>().homeBase = this.gameObject;
            }
            count += 1;
            lastTime = Time.time;
        }
        
        if(count == waveCount){
            complete = true;
        }
    }
}
