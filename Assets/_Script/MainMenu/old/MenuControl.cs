using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject[] ships = new GameObject[4];
    public int currentShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            nextShip();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            prevShip();
        }
    }

    public void nextShip(){
        if (currentShip < 3){
                currentShip++;
        } else {
            currentShip = 0;
        }
        (ships[currentShip].transform.GetChild(0).gameObject).transform.localEulerAngles = new Vector3(-90f,0,-10f);
    }

    public void prevShip(){
        if (currentShip > 0){
                currentShip--;
        } else {
            currentShip = 3;
        }
        (ships[currentShip].transform.GetChild(0).gameObject).transform.localEulerAngles = new Vector3(-90f,0,-10f);
    }
    
}
