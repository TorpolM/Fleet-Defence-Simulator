using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    public Vector3 waypoint;
    public float commandSpeed;
    public float currentSpeed;
    public bool OnMove;
    
    float maxTurnRate;
    void Start()
    {
		maxTurnRate = GetComponent<Entity>().maxTurnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance (transform.position, waypoint) <= 100 && OnMove == true){
            OnMove = false;
        } else {
            OnMove = true;
        }

        if(commandSpeed > currentSpeed) {
            currentSpeed += 10 * Time.deltaTime;
        }
        if(commandSpeed < currentSpeed) {
            currentSpeed += -10 * Time.deltaTime;
        }

        if(OnMove){
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (waypoint - transform.position), Time.deltaTime * maxTurnRate);
        }
        if(!GetComponent<Entity>().isDestroyed){
            GetComponent<Rigidbody>().velocity = (transform.forward * currentSpeed * 0.0514f);
        }
    }
}
