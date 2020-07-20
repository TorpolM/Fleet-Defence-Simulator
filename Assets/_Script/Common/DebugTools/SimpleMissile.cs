using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMissile : MonoBehaviour
{
    public GameObject target;
	public GameObject Explosion;
	public GameObject smoke;
    public bool ignite;
    bool enableFuze = false;
    float timer = 0;
    void Start()
    {
        smoke.SetActive(false);
        ignite = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ignite == true){
            if(smoke != null){
            smoke.SetActive(true);
            }
            GetComponent<Rigidbody>().velocity = (transform.forward * 2400 * 0.0514f);
            if (target != null && Vector3.Angle(transform.forward,(target.transform.position - transform.position)) < 90) {
                var arrivalTime = Vector3.Distance(target.transform.position,transform.position) / GetComponent<Rigidbody>().velocity.magnitude;
                var predictionPos = target.transform.position + arrivalTime * target.GetComponent<Rigidbody>().velocity;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (predictionPos - transform.position), 0.5f * Time.deltaTime);
			}
            GetComponent<Rigidbody>().velocity = (transform.forward * 2400 * 0.0514f);
        }

    }
    void OnTriggerStay(Collider hitObj){
		if(hitObj.tag == "Player" || hitObj.tag == "Entity"){
			if(ignite == true){
				hitObj.GetComponent<Entity>().onHit(GetComponent<Entity>().warheadAF);
                GetComponent<Entity>().destroyEntity();
			}
		}
	}
}
