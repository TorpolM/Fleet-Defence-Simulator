using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlipFade : MonoBehaviour
{
    public float Elevation;
    // Start is called before the first frame update
    float alpha = 196f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color32(0,255,0,(byte)alpha);
        alpha += -0.27f;
        if (alpha < 0){
            Destroy(this.gameObject);
        }
    }
}
