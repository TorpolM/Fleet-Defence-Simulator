using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MapBase;
    public GameObject MapImage;
    public float scale = 16400;


    public bool isMapDisplay;
    void Start()
    {
        isMapDisplay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMapDisplay){
            MapBase.active = true;
        } else {
            MapBase.active = false;
        }


        if(Input.GetKeyDown(KeyCode.M)){
            if(isMapDisplay){
                isMapDisplay = false;
            } else {
                isMapDisplay = true;
            }
        }
        if(isMapDisplay){
            scale += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * -100000f;
        }
    }


    public void mapOffset(){
        MapImage.transform.position += new Vector3(Input.GetAxis("Mouse X") * 10f,Input.GetAxis("Mouse Y") * 10f,0f);
    }
}
