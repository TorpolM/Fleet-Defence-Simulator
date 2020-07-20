using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform Icon;
    public RectTransform Map;

    GameObject[] Entities;
    GameObject[] Triggers;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Entities = GameObject.FindGameObjectsWithTag("Entity");
        foreach (GameObject _x in Entities){
            if(_x.GetComponent<Entity>().MapID == 0){
                RectTransform newIcon = Instantiate(Icon);
                newIcon.transform.parent = Map.transform;
                newIcon.GetComponent<Icon>().obj = _x;
                newIcon.GetComponent<Icon>().type = _x.GetComponent<Entity>().side + 1;
                _x.GetComponent<Entity>().MapID = 1;
            }
        }
        Triggers = GameObject.FindGameObjectsWithTag("sTrigger");
        foreach (GameObject _x in Triggers){
            if(_x.GetComponent<ScenarioTrigger>().MapID == 0 && _x.GetComponent<ScenarioTrigger>().triggerType == 0){
                RectTransform newIcon = Instantiate(Icon);
                newIcon.transform.parent = Map.transform;
                newIcon.GetComponent<Icon>().obj = _x;
                newIcon.GetComponent<Icon>().type = 4;
                _x.GetComponent<ScenarioTrigger>().MapID = 1;
            }
        }


        
    }
}
