using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectController : MonoBehaviour {
	private bool isHeld = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public MoveableParentController getMoveableParent(){
		return GetComponentInParent<MoveableParentController> ();
	}
	public void toggleIsHeld(GameObject holder){

		MoveableParentController dependent = getMoveableParent();
		isHeld = !isHeld;
		if (dependent == null) {
			this.transform.parent = (isHeld) ? holder.transform : null;
		} else {
			dependent.transform.parent = (isHeld) ? Camera.allCameras[0].transform : null;
		}
	}
}
