using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public GameObject obj;
    public int type;    //0:Ownship 1:AI
    public Sprite[] icons;


    RectTransform iconImage;
    Text iconText;
    float scale;
    float speed;
    float heading;
    float altitude;
    void Start()
    {
        iconImage = (transform.GetChild(0)).GetComponent<RectTransform>();
        iconText = (transform.GetChild(1)).GetComponent<Text>();
        iconImage.GetComponent<Image>().sprite = icons[type];
    }

    // Update is called once per frame
    void Update()
    {
        if(obj == null){
            Destroy(this.gameObject);
        }
        if(type == 0){
            speed = obj.GetComponent<ShipMove>().currentSpeed;
        }
        heading = obj.transform.localEulerAngles.y;
        altitude = obj.transform.position.y;

        scale = GameObject.FindGameObjectWithTag("GameController").GetComponent<Map>().scale;
        transform.localPosition = new Vector3(obj.transform.position.x / scale * 480,obj.transform.position.z / scale * 480,0f);
        iconImage.transform.localEulerAngles = new Vector3(0,0,-1 * obj.transform.localEulerAngles.y);
        iconText.text = "SPD: " + speed.ToString("000") + "\nCRS: " + heading.ToString("000") + "\nALT: " + altitude.ToString("000");
    }
}
