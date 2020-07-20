using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Plane_Attacker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject homeBase;
    public Vector3 targetPos;
    public int behivior;    //0:cruise 1:search 2:attack 3:defencive 4:seek 5:rtb

    int side;
    public List<GameObject> targetList = new List<GameObject>();
    GameObject target;
    float fuel;
    float maxSpeed;
    float sensorRange;
    float weaponRange;

    float lastShootTime;


    void Start()
    {
        behivior = 0;

        side = GetComponent<Entity>().side;
        fuel = GetComponent<Entity>().fuel;
        maxSpeed = GetComponent<Entity>().maxSpeed;
        sensorRange = GetComponent<Entity>().sensorRange / 10;
        weaponRange = ((GetComponent<Entity>().internalWeapons).GetComponent<Entity>().Range / 10) * 0.8f;

        lastShootTime = Time.time;
        targetPos += new Vector3(0f,1000f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetComponent<Entity>().isDestroyed){
            var rand = new System.Random();
            if(fuel < 0){
                behivior = 5;
            }
            if(Vector3.Distance (transform.position,targetPos) < sensorRange && behivior != 5){
                behivior = 1;
            }
            
            GetComponent<PlaneMove>().commandSpeed = maxSpeed;
            GetComponent<PlaneMove>().OnMove = true;
            if(behivior == 0){
                GetComponent<PlaneMove>().waypoint = targetPos;
            }
            if(behivior == 1 || behivior == 4){
                targetList.Clear();
                Collider[] contacts = Physics.OverlapSphere(transform.position,sensorRange);
                GameObject contact;
                foreach (Collider _x in contacts){
                    contact = _x.gameObject;
                    if(contact != null ){
                        if(contact.tag == "Player" || contact.tag == "Entity"){
                            int id = contact.GetComponent<Entity>().side;
                            int type = contact.GetComponent<Entity>().vehicleType;
                            if(side != id && id != 2 && type == 0){
                                targetList.Add(contact);
                            }
                        }
                    }
                }
            }
            if (targetList.Count > 0 && behivior == 1){
                if(Vector3.Distance (transform.position,(targetList[0]).transform.position) < weaponRange){
                    target = targetList[rand.Next(targetList.Count)];
                    behivior = 2;
                }
            }
            if(behivior == 2){
                int weapons = GetComponent<Entity>().weaponCount;
                if (weapons > 0 && (Time.time - lastShootTime) > 5){
                    GameObject weapon = Instantiate(GetComponent<Entity>().internalWeapons);
                    weapon.transform.parent = transform;
                    weapon.transform.localPosition = new Vector3(0f,-3f,0f);
                    weapon.transform.localEulerAngles = new Vector3(0f,0f,0f);
                    weapon.GetComponent<missile_ARH>().WPPos = target.transform.position;
                    weapon.transform.parent = null;
                    weapon.GetComponent<missile_ARH>().guidancePhase = 0;

                    GetComponent<Entity>().weaponCount += -1;
                    lastShootTime = Time.time;
                }
                if(weapons == 0 && (Time.time - lastShootTime) > 5){
                    behivior = 5;
                }
            }
            if(behivior == 5){
                GetComponent<PlaneMove>().waypoint = homeBase.transform.position;
                GetComponent<PlaneMove>().OnMove = true;
            }
        }
    }
}
