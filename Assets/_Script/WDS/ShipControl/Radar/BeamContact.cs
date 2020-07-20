using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamContact : MonoBehaviour
{
    public List<GameObject> contactList = new List<GameObject> ();
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		contactList.Clear ();
	}
	void OnTriggerStay(Collider contact){
		contactList.Add (contact.gameObject);
	}
}
