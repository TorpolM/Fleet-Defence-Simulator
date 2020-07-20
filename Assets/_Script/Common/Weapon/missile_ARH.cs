using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile_ARH : MonoBehaviour {
	public Vector3 WPPos;
	public GameObject Explosion;
	public GameObject ab;
	public int guidancePhase;	//-1:stored 0:cruise 1:search 2:terminal;

	Transform seeker;
	public GameObject target;
	float seekerFOV;
    float seekerRange;
    float maxSpeed;
    float maxTurnRate;
	int missileType;

	
	void Start () {
		seeker = transform.FindChild("seeker");
		seekerFOV = GetComponent<Entity>().seekerFOV;
		seekerRange = GetComponent<Entity>().sensorRange / 10;
		maxSpeed = GetComponent<Entity>().maxSpeed;
		maxTurnRate = GetComponent<Entity>().maxTurnRate;
		missileType = GetComponent<Entity>().missileType;

		seeker.transform.localScale = new Vector3(Mathf.Sin(seekerFOV * Mathf.Deg2Rad) * seekerRange * 50f,Mathf.Sin(seekerFOV * Mathf.Deg2Rad) * seekerRange * 50f,seekerRange * 50f);
		seeker.transform.localPosition = new Vector3(0f,0f,(seekerRange * 50f / 100) + 2.5f);
		guidancePhase = 0;
		ab.SetActive (false);
		target = null;
	}

	// Update is called once per frame
	void Update () {
		
		if(Vector3.Distance (transform.position, WPPos) <= seekerRange && guidancePhase == 0){
			guidancePhase = 1;
		}
		if(guidancePhase == 1 && seeker.GetComponent<Radar>().target != null){
			if(missileType == 0 && (seeker.GetComponent<Radar>().target).GetComponent<Entity>().vehicleType != 2){
				target = seeker.GetComponent<Radar>().target;	
			}
			if(missileType == 1 && (seeker.GetComponent<Radar>().target).GetComponent<Entity>().vehicleType == 0){
				target = seeker.GetComponent<Radar>().target;	
			}
			if(target != null){
				guidancePhase = 2;
			}
		}
		if(guidancePhase == 2 && target == null){
			guidancePhase = 1;
		}


		if (guidancePhase == 0){
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (WPPos - transform.position), Time.deltaTime * maxTurnRate * maxSpeed * 0.0514f);
		}
		if(guidancePhase == 2){
			var arrivalTime = Vector3.Distance(target.transform.position,transform.position) / GetComponent<Rigidbody>().velocity.magnitude;
            var predictionPos = target.transform.position + arrivalTime * target.GetComponent<Rigidbody>().velocity;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (predictionPos - transform.position),Time.deltaTime * maxTurnRate * maxSpeed * 0.0514f);
		}
		if(guidancePhase > -1){
			ab.SetActive (true);
			GetComponent<Rigidbody>().velocity = (transform.forward * maxSpeed * 0.0514f);
		}
	}

	void OnTriggerStay(Collider hitObj){
		if(hitObj.tag == "Player" || hitObj.tag == "Entity"){
			Debug.Log(hitObj);
			hitObj.GetComponent<Entity>().onHit(GetComponent<Entity>().warheadAF);
			GetComponent<Entity>().destroyEntity();
		}
	}
}
