using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    // Start is called before the first frame update
    public string name;
    public GameObject campos;
    public GameObject title;
    public bool useOrthographic;
    public float defaultFOV;
    public GameObject[] shortCut;   //0~9
    public GameObject shortCutBS;
    public GameObject shortCutEnter;
    public GameObject shortCutSpace;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        title.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void showTitle(){
        title.SetActive(true);
        title.transform.LookAt(GameObject.FindGameObjectWithTag("Cam_Int").transform.position);
    }

}
