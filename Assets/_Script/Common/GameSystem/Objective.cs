using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // Start is called before the first frame update
    public string title;
    public string status;
    public bool isComplate;
    public int Type;        //0:Complate 1:failed;
    public bool isImmediative;
    public GameObject[] triggers;

    string[] statusList = {"Incomplete","Failed","Complete","OK"};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject trigger in triggers){
            isComplate = trigger.GetComponent<ScenarioTrigger>().Condition;
        }
        if(!isComplate){
            status = statusList[Type];
        } else {
            status = statusList[Type + 2];
        }
    }
}
