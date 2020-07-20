using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pantaographGrip : MonoBehaviour
{
    public GameObject origin;
    public GameObject cursor;
    
    public Vector3 cursorPos;
    Vector3 lpos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cursorPos =  cursor.transform
    }
    public void correctPos(Vector3 pos){
        lpos = origin.transform.InverseTransformPoint(pos);
        lpos = new Vector3(lpos.x,0f,lpos.z);
        cursor.transform.localPosition = lpos;
    }
}
