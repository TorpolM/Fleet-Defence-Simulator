using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClickTargeting : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> targetList = new List<GameObject>();
    public GameObject Launcher;
    public GameObject missile;
    Quaternion stanbaypos;
    GameObject target;
    GameObject loadedMissile = null;
    float lastlaunchtime = 0f;
    void Start()
    {
        stanbaypos = Launcher.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetList.Count > 0){
            if(targetList[0] != null){
                target = targetList[0];
                if(loadedMissile == null && (Time.time - lastlaunchtime) > 5){
                    loadedMissile = Instantiate (missile);
                    loadedMissile.transform.parent = Launcher.transform;
                    loadedMissile.transform.localPosition = new Vector3(0f,0.0032f,0f);
                    loadedMissile.transform.localEulerAngles = new Vector3 (0,180,0);
                    loadedMissile.GetComponent<SimpleMissile>().target = target;
                }
            } else {
                targetList.RemoveAt(0);
                target = null;
            }
            if (target != null){
            var angle = Quaternion.Slerp (Launcher.transform.rotation, Quaternion.LookRotation (-1 * (target.transform.position - Launcher.transform.localPosition)),0.05f);
            Launcher.transform.rotation = angle;
            } else {
                var angle = Quaternion.Slerp (Launcher.transform.rotation,stanbaypos,0.05f);
			    Launcher.transform.rotation = angle;
            }
        }
        if(target != null && Input.GetKeyDown(KeyCode.Return) && loadedMissile != null){
            loadedMissile.transform.parent = null;
            loadedMissile.GetComponent<SimpleMissile>().ignite = true;
            loadedMissile = null;
            targetList.RemoveAt(0);
            lastlaunchtime = Time.time;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>().soundLaunch);
        }
    }
}
