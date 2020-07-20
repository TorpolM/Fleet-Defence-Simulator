using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    // Start is called before the first frame update
    public Material matOff;
    public Material matOn;

    public bool enable;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enable){
            (transform.GetChild(2)).GetComponent<MeshRenderer>().material = matOn;
        } else {
            (transform.GetChild(2)).GetComponent<MeshRenderer>().material = matOff;
        }
    }
}
