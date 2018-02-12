using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonController : MonoBehaviour {
	public KeypadController parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClicked(){
		parent.onChildClicked (this.name); 
	}
}
