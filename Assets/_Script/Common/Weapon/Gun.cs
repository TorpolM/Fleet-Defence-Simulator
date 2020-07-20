using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shell;
    public float initialSpeed;      //meter per second;
    public float FireRate;      //interval-seconds;
    public float Power;
    public GameObject fcs;
    public GameObject effectFire;
    public AudioClip soundFire;
    public bool Fire = false;

    float lastFiretime = 0;
    GameObject firedShell;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Power = (initialSpeed / 10) * shell.GetComponent<Rigidbody>().mass;
        if(Fire){
            fireSingle(0);
        }
        if(fcs.GetComponent<GFCS>().shell == null && firedShell != null){
            fcs.GetComponent<GFCS>().shell = firedShell;
        }
    }

    bool fireSingle(int shellType){
        if(Time.time - lastFiretime > FireRate){
            firedShell = Instantiate (shell);
            firedShell.transform.parent = transform;
            firedShell.transform.localPosition = new Vector3(0f,0f,0f);
            firedShell.transform.localEulerAngles = new Vector3 (90,0,0);
            firedShell.transform.parent = null;
            firedShell.GetComponent<Rigidbody>().AddForce(transform.forward * Power,ForceMode.Impulse);
            GetComponent<AudioSource>().PlayOneShot(soundFire);
            GameObject fireEffect = Instantiate (effectFire);
            fireEffect.transform.parent = transform;
            fireEffect.transform.localPosition = new Vector3(0f,0f,0f);
            Destroy(fireEffect,5);
            lastFiretime = Time.time;
            return true;
        } else {
            return false;
        }
    }
}
