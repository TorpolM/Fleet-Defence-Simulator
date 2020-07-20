using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    //params
    public float commandAngle;
    public float commandSpeed;
    public ParticleSystem wake;
    
    //datas
    public float currentAngle;
    public float currentSpeed;

    bool isWakePlay;
    bool isWakePlayLast = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(commandSpeed > currentSpeed) {
            currentSpeed += 1 * Time.deltaTime;
        }
        if(commandSpeed < currentSpeed) {
            currentSpeed += -1 * Time.deltaTime;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,commandAngle,0),Time.deltaTime * 0.032f * Mathf.Abs(currentSpeed) * 0.0514f);
        GetComponent<Rigidbody>().velocity = (transform.forward * currentSpeed * 0.0514f);
        currentAngle = transform.eulerAngles.y;

        if(Mathf.Abs(commandSpeed) > 1){
            isWakePlay = true;
        } else {
            isWakePlay = false;
        }

        if(isWakePlay && isWakePlay != isWakePlayLast){
            wake.Play (true);
        }
        if(!isWakePlay && isWakePlay != isWakePlayLast){
            wake.Stop (true, ParticleSystemStopBehavior.StopEmitting);
        }
        isWakePlayLast = isWakePlay;
    }
}
