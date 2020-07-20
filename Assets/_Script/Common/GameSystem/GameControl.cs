using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public bool isPlaying = false;
    public GameObject UI_Briefing;
    public GameObject UI_Opening;
    public GameObject UI_Ending;
    Objective[] Objectives;
    TimeScale ts;


    bool isOpend = false;
    void Start()
    {
        ts = GetComponent<TimeScale>();

        ts.scale = 0;
        UI_Briefing.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Objectives = GetComponents<Objective>();
        int cnt = 0;
        foreach(Objective obj in Objectives){
            if(obj.isComplate){
                cnt += 1;
            }
        }
        if(cnt == Objectives.Length){
            endScenario(0);
        }
        if(Input.GetKeyDown(KeyCode.Escape) && isPlaying){
            openBriefing();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !isPlaying){
            closeBriefing();
        }
    }


    public void closeBriefing(){
        UI_Briefing.SetActive(false);
        ts.scale = 1;
        if(!isOpend){
            UI_Opening.SetActive(true);
            isOpend = true;
        }
        isPlaying = true;
    }


    public void openBriefing(){
        UI_Briefing.SetActive(true);
        ts.scale = 0;
        isPlaying = false;
    }


    public void endScenario(int result){
        ts.scale = 0;
        isPlaying = false;
        UI_Ending.SetActive(true);
    }
}
