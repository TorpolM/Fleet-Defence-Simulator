using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform cav;
    public GameObject blip;
    public AnimationCurve curve;
    void Start()
    {
        cav = transform.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0f,0f,-3f);
        for(int cnt = 0;cnt < Random.Range(16,64);cnt++){
            GameObject echo = Instantiate(blip);
            echo.transform.parent = cav;
            var size = Random.Range(0.01f,0.8f);
            size = CurveWeightedRandom(curve) * 0.7f + 0.05f;
            echo.transform.localScale = new Vector3(size,size,1f);
            echo.transform.localEulerAngles = new Vector3(0f,0f,0f);
            var dist = CurveWeightedRandom(curve) * 550f;
            var angle = transform.localEulerAngles.z;
            echo.transform.localPosition = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * -dist,Mathf.Sin(angle * Mathf.Deg2Rad) * -dist,0f);
            Destroy(echo,size);
        }
    }


    public static float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }
}
