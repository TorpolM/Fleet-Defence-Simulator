using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    // Start is called before the first frame update
    public float warheadAF;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
    }
    void OnTriggerStay(Collider hitObj){
		if(hitObj.tag == "Player" || hitObj.tag == "Entity"){
			Debug.Log(hitObj);
			hitObj.GetComponent<Entity>().onHit(GetComponent<Entity>().warheadAF);
			GetComponent<Entity>().destroyEntity();
		}
	}
}
