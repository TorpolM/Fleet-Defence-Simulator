using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public GameObject Controller; 
    GameObject ship;
    void Start()
    {
        ship = (Controller.GetComponent<MenuControl>().ships)[Controller.GetComponent<MenuControl>().currentShip];
    }

    // Update is called once per frame
    void Update()
    {
        ship = (Controller.GetComponent<MenuControl>().ships)[Controller.GetComponent<MenuControl>().currentShip];
        transform.parent = ship.transform;
        transform.localPosition = Vector3.MoveTowards (transform.localPosition, new Vector3 (-242,96,0),10);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(12f,90f,0f)), 0.1f);
    }
}
