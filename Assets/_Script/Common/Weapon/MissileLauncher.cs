using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject elevation;
    public GameObject FCS;
    public GameObject reference;
    public GameObject[] missiles;
    public GameObject[] Rails;
    public float loadingTime;
    public float[] loadingWait;
    public bool[] loaded;
    public GameObject[] loadedMissiles;
    public bool[] isLoading;
    public bool aligned;
    public AudioClip soundLaunch;

    Quaternion loadpos;
    public Vector3 loadPosV3;
    void Start()
    {
        loadpos = reference.transform.localRotation;
        loadPosV3 = reference.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLoading[0] && !isLoading[1]){
            if(FCS != null){
                if(FCS.GetComponent<GMFCS>().trackingTarget != null){
                    reference.transform.rotation = Quaternion.Slerp(reference.transform.rotation,Quaternion.LookRotation(FCS.GetComponent<GMFCS>().trackingTarget.transform.position),1*Time.deltaTime);
                    if(Vector3.Angle(reference.transform.forward,(FCS.GetComponent<GMFCS>().trackingTarget.transform.position - reference.transform.position)) < 5){
                        aligned = true;
                    } else {
                        aligned = false;
                    }
                } else {
                    reference.transform.localRotation = Quaternion.Slerp(reference.transform.localRotation,loadpos,1*Time.deltaTime);
                }
            } else {
                reference.transform.localRotation = Quaternion.Slerp(reference.transform.localRotation,loadpos,1*Time.deltaTime);
            }
        } else {
            reference.transform.localRotation = Quaternion.Slerp(reference.transform.localRotation,loadpos,1*Time.deltaTime);
            Debug.Log(Vector3.Angle(reference.transform.localEulerAngles,loadPosV3));
            if(Vector3.Angle(reference.transform.localEulerAngles,loadPosV3) < 5){
                for(int cnt = 0;cnt < 2;cnt++){
                    if(isLoading[cnt]){
                        loadingWait[cnt] += -1f * Time.deltaTime;
                        if(loadingWait[cnt] < 0){
                            GameObject missile = Instantiate(missiles[0]);
                            missile.transform.parent = Rails[cnt].transform;
                            missile.transform.localPosition = new Vector3(0,0,0);
                            missile.transform.localEulerAngles = new Vector3(0,0,0);
                            loadedMissiles[cnt] = missile;
                            isLoading[cnt] = false;
                            loaded[cnt] = true;
                        }
                    }
                }
            }
        }
        for(int cnt = 0;cnt < 2;cnt++){
            if(loadedMissiles[cnt] != null){
                loadedMissiles[cnt].transform.localPosition = new Vector3(0,0,0);
            }
        }
        transform.localEulerAngles = new Vector3(0,reference.transform.localEulerAngles.y,0);
        elevation.transform.localEulerAngles = new Vector3(-reference.transform.localEulerAngles.x,0,0);

    }
    public void loadMissile(int rail){
        if(!isLoading[rail] && !loaded[rail]){
            isLoading[rail] = true;
            loadingWait[rail] = loadingTime;
        }
    }

    public void launchMissile(int rail){
        if(loaded[rail]){
            if(FCS != null){
                loadedMissiles[rail].GetComponent<Missile_SARH>().FCS = FCS;
            }
            loadedMissiles[rail].transform.parent = null;
            loadedMissiles[rail].GetComponent<Missile_SARH>().ignite = true;
            loadedMissiles[rail] = null;
            loaded[rail] = false;
            GetComponent<AudioSource>().PlayOneShot(soundLaunch);
        }
    }
}
