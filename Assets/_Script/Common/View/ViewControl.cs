using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    public Camera ExternalCam;
    public Camera InternalCam;
    public GameObject camTgt;


    GameObject ownShip;
    GameObject[] Entities;
    int EntitiyIndex;

    public int viewMode;    //0:External-Own 1:External-Obj 2:Internal-1 3:Internal-2 ...
    void Start()
    {
        viewMode = 0;
        ownShip = GameObject.FindWithTag("Player");
        EntitiyIndex = 0;
        camTgt = ownShip;
    }

    // Update is called once per frame
    void Update()
    {
        Entities = GameObject.FindGameObjectsWithTag("Entity");
        if(Entities == null){
            //viewMode = 0;
        }
        if(viewMode == 0){
            ExternalCam.enabled = true;
            InternalCam.enabled = false;
            camTgt = ownShip;
        }
        if(viewMode == 1){
            ExternalCam.enabled = true;
            InternalCam.enabled = false;
        }
        if(viewMode >= 2){
            ExternalCam.enabled = false;
            InternalCam.enabled = true;
        }


        if(Input.GetKeyDown(KeyCode.F1)){
            viewMode = 0;
        }
        if(Input.GetKeyDown(KeyCode.F5)){
            viewMode = 2;
            InternalCam.GetComponent<InternalCam>().changeRoom(0);
        }
        if(Input.GetKeyDown(KeyCode.F6)){
            viewMode = 3;
            InternalCam.GetComponent<InternalCam>().changeRoom(1);
        }
        if(Input.GetKeyDown(KeyCode.F7)){
            viewMode = 4;
            InternalCam.GetComponent<InternalCam>().changeRoom(2);
        }
        if(Input.GetKeyDown(KeyCode.F8)){
            viewMode = 5;
            InternalCam.GetComponent<InternalCam>().changeRoom(3);
        }
        if(Input.GetKeyDown(KeyCode.F2) && Entities != null){
            viewMode = 1;
            if(EntitiyIndex < Entities.Length){
                camTgt = Entities[EntitiyIndex];
            }
        }
        if(Input.GetKeyDown(KeyCode.F3) && viewMode == 1 && EntitiyIndex < (Entities.Length - 1)){
            EntitiyIndex += 1;
            if(EntitiyIndex < Entities.Length){
                camTgt = Entities[EntitiyIndex];
            }
        }
        if(Input.GetKeyDown(KeyCode.F4) && viewMode == 1 && EntitiyIndex > 0){
            EntitiyIndex += -1;
            if(EntitiyIndex < Entities.Length){
                camTgt = Entities[EntitiyIndex];
            }
        }
    }
}
