using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScenarioLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string scenarioFile;
    void Start()
    {
        scenarioFile = Application.dataPath + "/UserScenarios/test.xml";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
