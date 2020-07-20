using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Common Parameters;
    public int MapID;   //0:enableMapIcon 1:disableMapIcon;
    public int side;    //0:Blue 1:Red 2:Civilian;
    public int vehicleType;     //0:ship 1:plane 2:weapon(missile) 3:weapon(shell/bomb);
    public float HP;    //recommand weight of object(Ton);
    public bool isDestroyed = false;
    public float maxSpeed;
    public float maxTurnRate;
    public float sensorRange;
    public float RCS;
    public GameObject effectDestrtoy;   //generated effect on destroy;
    public GameControl effectDamage;
    public AudioClip soundCruise;   //sound on crusing;
    public AudioClip soundDestroy;  //sound on destroy;
    public AudioClip soundLaunch;

    //Ship Parameters;

    //Plane Parameters;
    public float maxAltitude;
    public float fuel;
    public GameObject internalWeapons;
    public int weaponCount;

    //Missile Parameters;
    public int missileType;    //0:anti-air 1:anti-surface
    public int guidanceSystem;    //0:SARH 1:ARH,IR 2:unguided
    public float seekerFOV;
    public float Range;
    public float warheadAF;     //recommend:warhead weight in kg


    float maxHP;

    void Start()
    {
        maxHP = HP;
        if(soundCruise != null){
            GetComponent<AudioSource>().clip = soundCruise;
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(vehicleType == 1 && transform.position.y < 0){
            deleteEntity();
        }
        if(vehicleType == 3 && transform.position.y < 0){
            deleteEntity();
        }

        if(HP < 0 && !isDestroyed){
            destroyEntity();
        }
        if(HP < (maxHP / 2)){
        }
    }

    void fuelOut(){
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void onHit(float damage){
        HP = HP - damage;
    }

    public void destroyEntity(){
        if(vehicleType == 1){
            GetComponent<AudioSource>().Stop();
            GameObject explosion = Instantiate (effectDestrtoy) as GameObject;
            explosion.transform.parent = transform;
            explosion.transform.localPosition = new Vector3(0,0,0);
            explosion.transform.localEulerAngles = new Vector3(90,0,0);
            GetComponent<Rigidbody>().useGravity = true;
            RCS = 0;
        }
        if(vehicleType == 2 || vehicleType == 3){
            GameObject explosion = Instantiate (effectDestrtoy) as GameObject;
            explosion.transform.position = transform.position;
            AudioSource sound = explosion.AddComponent<AudioSource>();
            sound.PlayOneShot(soundDestroy);
            deleteEntity();
            Destroy(explosion,5);
        }
        if(soundDestroy != null){
            GetComponent<AudioSource>().PlayOneShot(soundDestroy);
        }
        isDestroyed = true;
    }

    public void deleteEntity(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ExternalCameraControl>().onEntityDelete(this.gameObject);
        Destroy(this.gameObject);
    }
}
