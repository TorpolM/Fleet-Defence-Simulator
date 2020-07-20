using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalCameraControl : MonoBehaviour
{
    public GameObject Controller;

    GameObject camTgt;
    Vector3 targetPos; 
    float mouseInputX;
    float mouseInputY;
    void Start()
    {

    }


    void Update()
    {
        if (Controller.GetComponent<ViewControl>().camTgt != camTgt) {
            camTgt = Controller.GetComponent<ViewControl>().camTgt;
            transform.parent = camTgt.transform;
            transform.localPosition = new Vector3 (0,5,50);
        }
        if(camTgt != null){
            targetPos = camTgt.transform.position;
        }

        if (enabled && Input.GetMouseButton(1)){
            mouseInputX = Input.GetAxis("Mouse X");
            mouseInputY = Input.GetAxis("Mouse Y");
        } else {
            mouseInputX = mouseInputY = 0;
        }
        if(enabled){
            transform.position = transform.position + transform.forward * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 800f;
        }


        transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 100f);
        transform.RotateAround(targetPos, transform.right, -mouseInputY * Time.deltaTime * 100f);
        transform.LookAt(targetPos);
    }


    public void onEntityDelete(GameObject entity){
        if(entity == camTgt){
            transform.parent = null;
        }
    }
}
