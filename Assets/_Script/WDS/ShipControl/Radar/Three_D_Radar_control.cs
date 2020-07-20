using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three_D_Radar_control : MonoBehaviour {
	float deg = 0f;
	float dir = 1f;
	// Use this for initialization

	public GameObject SwitchRPM;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (SwitchRPM.GetComponent<DialSwitch>().position == 0){
			dir = 0.5f;
		}
		if (SwitchRPM.GetComponent<DialSwitch>().position == 1){
			dir = 1.15f;
		}
		if (SwitchRPM.GetComponent<DialSwitch>().position == 2){
			dir = 1.5f;
		}
		

		deg += dir;

		transform.localEulerAngles =  new Vector3 (transform.localEulerAngles.x,deg,transform.localEulerAngles.z);

	}
}
