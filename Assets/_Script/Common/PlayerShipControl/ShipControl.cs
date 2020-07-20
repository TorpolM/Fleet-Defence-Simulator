using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipControl : MonoBehaviour
{
    GameObject ownShip;
    public Slider sliderThrottle;
    public RectTransform commandArrow;
    public RectTransform currentArrow;


    public float commandAngle;
    public int currentThrottle;
    public float ownShipSpeed;
    public Canvas MainUI;

    void Start()
    {
        ownShip = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentThrottle = (int)sliderThrottle.value;
        float targetSpd = currentThrottle * 6f;

        currentArrow.transform.localEulerAngles = new Vector3(0f,0f,-1 * ownShip.transform.localEulerAngles.y);
        commandArrow.transform.localEulerAngles = new Vector3(0f,0f,-1 * commandAngle);

        ownShip.GetComponent<ShipMove>().commandSpeed = targetSpd;
        ownShip.GetComponent<ShipMove>().commandAngle = commandAngle;
    }


    public void courseSet(){
        Vector2 mousePos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MainUI.GetComponent<RectTransform>(),Input.mousePosition,null, out mousePos);
        commandAngle = Mathf.Atan2(mousePos.x - 824,mousePos.y + 394) * Mathf.Rad2Deg;
    }
}
