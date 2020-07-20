using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        target = null;
    }
    void OnTriggerStay(Collider refrect){
        if(refrect.tag == "Entity" || refrect.tag == "Player"){
		    target = refrect.gameObject;
        }
	}
}
