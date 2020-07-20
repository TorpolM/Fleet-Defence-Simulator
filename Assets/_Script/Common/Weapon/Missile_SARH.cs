using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_SARH : MonoBehaviour
{
    public GameObject Explosion;
	public GameObject ab;
	public int guidancePhase;	//-1:stored 0:cruise 1:search 2:terminal;

	Transform seeker;
	public GameObject target;
    public GameObject FCS;
	float seekerFOV;
    float seekerRange;
    float maxSpeed;
    float maxTurnRate;
	int missileType;
    public bool ignite;
    float currentSpd = 10;

    bool lastIgnit;
    float ft;
    public float range = 0f;
	
	void Start () {
		seekerFOV = GetComponent<Entity>().seekerFOV;
		maxSpeed = GetComponent<Entity>().maxSpeed;
		maxTurnRate = GetComponent<Entity>().maxTurnRate;
		missileType = GetComponent<Entity>().missileType;
		ab.SetActive (false);
		target = null;
        GetComponent<CapsuleCollider>().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if(ignite == true){
            if(ab != null){
                ab.SetActive(true);
            }
            if(FCS != null){
                target = FCS.GetComponent<GMFCS>().trackingTarget;
                FCS.GetComponent<GMFCS>().missiles.Add(this.gameObject);
            }
            if(target != null) {
                if(Vector3.Angle(transform.forward,(target.transform.position - transform.position)) < seekerFOV){
                    GetComponent<Rigidbody>().velocity = (transform.forward * 2400 * 0.0514f);
                    var arrivalTime = Vector3.Distance(target.transform.position,transform.position) / GetComponent<Rigidbody>().velocity.magnitude;
                    var predictionPos = target.transform.position + arrivalTime * target.GetComponent<Rigidbody>().velocity;
				    transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (predictionPos - transform.position), 0.5f * Time.deltaTime);
                }
			}
            if(currentSpd < maxSpeed){
                currentSpd += 800 * Time.deltaTime;
            }
            if(range < GetComponent<Entity>().Range){
                GetComponent<Rigidbody>().velocity = (transform.forward * currentSpd * 0.0514f);
                range += transform.forward.magnitude * 2400 * 0.0514f * Time.deltaTime;
            } else {
                GetComponent<Rigidbody>().useGravity = true;
            }
        } else {
            ft = Time.time;
        }
        if(Time.time - ft > 3){
            GetComponent<CapsuleCollider>().enabled = true;
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
