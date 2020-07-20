using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addtoTargetfromMapIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addtoTarget(){
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MapClickTargeting>().targetList.Add((transform.parent).GetComponent<Icon>().obj);
    }
}
