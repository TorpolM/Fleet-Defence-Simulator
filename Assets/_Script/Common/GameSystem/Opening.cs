using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    // Start is called before the first frame update
    public Image effect;
    public AudioClip soundGQ;

    float alpha = 1;
    float time = 0;
    void Start()
    {
        effect.GetComponent<AudioSource>().PlayOneShot(soundGQ);
    }

    // Update is called once per frame
    void Update()
    {
        alpha = effect.color.a;
        alpha = alpha - (0.3f * Time.deltaTime);
        effect.color = new Color(0,0,0,alpha);
        if(alpha < 0 && time == 0){
            time = Time.time;
        }
        if(alpha < 0 && Time.time - time > 8){
            effect.gameObject.SetActive(false);
        }
    }
}
