using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Advanced : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public GameObject antenna;
    public float Pt;
    public float dB;
    public float Ft;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        targets.Clear();
    }
    void OnTriggerStay(Collider refrect){
        if(refrect.tag == "Entity" || refrect.tag == "Player"){
            var len = 3e8f / Ft;
		    var rcs = refrect.GetComponent<Entity>().RCS;
            var range = Vector3.Distance(antenna.transform.position,refrect.transform.position);
            var Pr = (Pt * Mathf.Pow(dB,2) * Mathf.Pow(len,2) * rcs) / (Mathf.Pow(4 * Mathf.PI,3) * Mathf.Pow(range,4));
            if(Pr > 1e-9f){
                targets.Add(refrect.gameObject);
            }
        }
	}
}
