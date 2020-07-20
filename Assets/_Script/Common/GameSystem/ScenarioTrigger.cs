using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject source;
    public int triggerType;     //0:waypoint 1:isAlive(DefendTarget) 2:isDestroy(AttackTarget) 3:SpawnerFinish
    public float radius;        //in meter;
    public List<GameObject> targets = new List<GameObject>();
    public int targetType;      //0:Blue 1:Red 2:Civil 3:Any;
    public bool Active = true;
    public bool Condition = false;
    public int MapID;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Active){
            if(triggerType == 0){
                if(targetType != 3){
                    targets.Clear();
                    foreach(GameObject target in GameObject.FindGameObjectsWithTag("Entity")){
                        if(target.GetComponent<Entity>().vehicleType == 0 || target.GetComponent<Entity>().vehicleType == 1){
                            if(targetType == target.GetComponent<Entity>().side){
                                targets.Add(target);
                            }
                        }
                    }
                }
                if(targetType == 3){
                    foreach(GameObject target in targets){
                        float dist = Vector3.Distance(source.transform.position,target.transform.position);
                        if(dist * 10 < radius){
                            Condition = true;
                        }
                    }
                }
            }
            if(triggerType == 1){
                if(source.GetComponent<Entity>().isDestroyed){
                    Condition = false;
                } else {
                    Condition = true;
                }
            }
            if(triggerType == 2){
                Condition =source.GetComponent<Entity>().isDestroyed;
            }
            if(triggerType == 3){
                Condition = source.GetComponent<OriginSpawner>().complete;
            }
        }
    }
}
