using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text title;
    public Text summery;
    public Text detail;
    public Text obj_Name;
    public Text obj_stat;
    
    Scenario sce;
    Objective[] objs;

    void Start()
    {
        sce = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scenario>();
        objs = GameObject.FindGameObjectWithTag("GameController").GetComponents<Objective>();
        title.text = sce.title;
        summery.text = sce.summery;
        detail.text = sce.detail;
    }

    // Update is called once per frame
    void Update()
    {
        string text_obj_n = "";
        string text_obj_s = "";
        foreach(Objective obj in objs){
            if(obj.isImmediative){
                text_obj_n += ("<b> " + obj.title + "</b>\n");
            } else {
                text_obj_n += (" " + obj.title + "\n");
            }
            if(obj.isComplate){
                if(obj.Type == 0){
                    text_obj_s += (":<color=lime>" + obj.status + "</color>\n");
                }
                if(obj.Type == 1){
                    text_obj_s += (":<color=lime>" + obj.status + "</color>\n");
                }
            } else {
                if(obj.Type == 0){
                    text_obj_s += (":<color=silver>" + obj.status + "</color>\n");
                }
                if(obj.Type == 1){
                    text_obj_s += (":<color=red>" + obj.status + "</color>\n");
                }
            }
        }
        obj_Name.text = text_obj_n;
        obj_stat.text = text_obj_s;
    }
}
