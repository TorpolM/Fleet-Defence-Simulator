using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalCam : MonoBehaviour
{
    public GameObject Controller;
    public GameObject turret;
    public GameObject crtRoom;
    public GameObject crtStation;

    GameObject[] rooms;

    Camera cam;
    bool camMode = false;


    void Start()
    {
        cam = this.GetComponent<Camera>();

        rooms = GameObject.FindGameObjectsWithTag("Room");
        crtRoom = rooms[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(crtRoom.GetComponent<Room>().crtStation == null){
            changeStation(crtRoom.GetComponent<Room>().stations[0]);
        }
        if(Input.GetKeyDown(KeyCode.KeypadPlus)){
            var index = crtRoom.GetComponent<Room>().stations.IndexOf(crtStation);
            if(index + 1 == crtRoom.GetComponent<Room>().stations.Count){
                changeStation(crtRoom.GetComponent<Room>().stations[0]);
            } else {
                changeStation(crtRoom.GetComponent<Room>().stations[index + 1]);
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadMinus)){
            var index = crtRoom.GetComponent<Room>().stations.IndexOf(crtStation);
            if(index == 0){
                changeStation(crtRoom.GetComponent<Room>().stations[crtRoom.GetComponent<Room>().stations.Count - 1]);
            } else {
                changeStation(crtRoom.GetComponent<Room>().stations[index - 1]);
            }
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter) && crtStation.GetComponent<Station>().useOrthographic){
            if(camMode){
                camMode = false;
                changeviewType(camMode);
            } else {
                camMode = true;
                changeviewType(camMode);
            }
        }
        if(Input.GetMouseButton(2) && !camMode){
            float X_Rotation = Input.GetAxis("Mouse X");
            float Y_Rotation = Input.GetAxis("Mouse Y");
            turret.transform.eulerAngles += new Vector3(0, X_Rotation * Time.deltaTime * 100, 0);
            transform.eulerAngles += new Vector3(-Y_Rotation * Time.deltaTime * 100, 0, 0);
        }
        if(Input.GetMouseButton(2) && camMode){
            float X_Rotation = Input.GetAxis("Mouse X");
            float Y_Rotation = Input.GetAxis("Mouse Y");
            turret.transform.localPosition += new Vector3(-X_Rotation * Time.deltaTime * 0.01f,-Y_Rotation * Time.deltaTime * 0.01f,0);
        }
        if(enabled && !camMode){
            cam.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * -500;
        }
        if(enabled && camMode){
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * -5;
        }


    }


    public void changeStation(GameObject station){
        crtStation = station;
        crtRoom.GetComponent<Room>().crtStation = crtStation;
        turret.transform.parent = crtStation.GetComponent<Station>().campos.transform;
        turret.transform.localPosition = new Vector3(0,0,0);
        turret.transform.localEulerAngles = new Vector3(0,0,0);
        cam.fieldOfView = crtStation.GetComponent<Station>().defaultFOV;
        changeviewType(false);
    }
    public void changeRoom(int index){
        if(index < rooms.Length){
            crtRoom = rooms[index];
            if(crtRoom.GetComponent<Room>().crtStation == null){
                changeStation(crtRoom.GetComponent<Room>().stations[0]);
            } else {
                changeStation(crtRoom.GetComponent<Room>().crtStation);
            }
        }
    }
    public void changeviewType(bool mode){
        if(mode){
            cam.orthographic = true;
            cam.orthographicSize = 1;
            turret.transform.localEulerAngles = new Vector3(0,0,0);
            transform.localEulerAngles = new Vector3(0,0,0);
            camMode = true;
        } else {
            cam.orthographic = false;
            turret.transform.localPosition = new Vector3(0,0,0);
            camMode = false;
        }
    }
}
