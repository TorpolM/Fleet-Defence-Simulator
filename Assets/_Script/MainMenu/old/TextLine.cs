using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLine : MonoBehaviour
{
    public Transform Origin;
    public float offset;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,Origin.position);
        line.SetPosition(1,transform.TransformPoint(transform.localPosition + Vector3.left * offset));
        line.SetPosition(2,transform.TransformPoint(transform.localPosition));
    }
}
