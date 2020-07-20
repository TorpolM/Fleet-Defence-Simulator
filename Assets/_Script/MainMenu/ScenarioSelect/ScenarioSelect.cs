using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScenarioSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public static string selectedScenario;
    void Start()
    {
        selectedScenario = Application.dataPath + "/UserScenarios/test.xml";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
