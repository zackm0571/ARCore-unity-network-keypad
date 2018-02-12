using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {
	//LineRenderer lineRenderer;
	public GameObject holder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject obj = getObjectTouchedByRaycast ();
		if (obj != null) {
			/**/
			checkKeyPad(obj);
			checkMoveable(obj);

			/**/
		}

		//drawLineRenderer();
	}

	private void checkKeyPad(GameObject obj){
		KeyButtonController button = obj.GetComponent<KeyButtonController> ();
		if (button != null) {
			button.onClicked();
		}
	}

	private void checkMoveable(GameObject obj){
		MoveableObjectController moveableObj = obj.GetComponent<MoveableObjectController> ();
		if (moveableObj != null) {
			moveableObj.toggleIsHeld (holder);
		}
	}

//	private LineRenderer getLineRenderer(){
//		if (lineRenderer == null) {
//			lineRenderer = GetComponent<LineRenderer> ();
//		}
//		return lineRenderer;
//	}
//
	private Camera getCamera(){
		return Camera.allCameras[0];
	}

//	private void drawLineRenderer(){
//		
//		MoveableObjectController obj = holder.GetComponent<MoveableObjectController> ();
//		if (obj != null) {
//			Vector3 offset = new Vector3 (0f, -1f, 0f);
//
//			getLineRenderer ().SetPosition(0, getCamera ().transform.position + offset);
//			getLineRenderer ().SetPosition(1, obj.transform.position);
//		}
//	}
//	private void hideLineRenderer(){
//		getLineRenderer ().SetPosition (0, Vector3.zero);
//		getLineRenderer ().SetPosition (1, Vector3.zero);
//	}
		
	public GameObject getObjectTouchedByRaycast(){
		if (Input.touchCount > 0) {
			Debug.LogError ("Touch");
			Touch touch = Input.GetTouch (0);
			if (touch.phase != TouchPhase.Ended)
				return null;
			Ray ray = Camera.allCameras[0].ScreenPointToRay(touch.position);
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast (ray, out hit)) {
				print (hit.collider.name);
				return hit.collider.gameObject;
			}
		}
		return null;
	}
}
