using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.Networking;
public class KeypadController : MonoBehaviour {
	public Text numText;
	public GameObject bg;
	private float colorTimer = -1;
	enum TextAction{
		Enter,Backspace, Number
	}
	private static readonly Dictionary<string, TextAction> actionItems
	= new Dictionary<string, TextAction>
	{
		{"enter", TextAction.Enter},
		{"backspace", TextAction.Backspace}
	};
	private string pw = "1234";
	// Use this for initialization
	void Start () {
		resetColor ();
		StartCoroutine (getPw ());
	}
	
	// Update is called once per frame
	void Update () {
		updateTimer ();

	}

	private void updateBgPos(){
		bg.transform.parent = this.transform;
	}
	private void updateTimer(){
		if (colorTimer != -1 && colorTimer <= 0) {
			resetColor ();
			colorTimer = -1;
		} else {
			colorTimer -= Time.deltaTime;
		}
	}
	public void onChildClicked(string val){
		TextAction action;
		action = (actionItems.TryGetValue (val, out action)) ? action : TextAction.Number;
		
		if (action == TextAction.Number) {
			numText.text += val;
		} else if (action == TextAction.Backspace) {
			numText.text = numText.text.Remove (numText.text.Length - 1);
		} else if (action == TextAction.Enter) {
			onAuth(authenticate());
		}
		Handheld.Vibrate ();
	}

	public void onAuth(bool isAuth){
		MeshRenderer renderer = bg.GetComponent<MeshRenderer> ();
		renderer.enabled = true;
		Material newMaterial = renderer.material;
		newMaterial.color = (isAuth) ? Color.green : Color.red;
		renderer.material = newMaterial;
		colorTimer = 1.0f;

	}

	public IEnumerator getPw ()
	{
		 	string url = "https://zackmatthews.com/keypad.txt";

			using (WWW www = new WWW(url))
			{
				yield return www;
				pw = www.text.Replace ("\n", "");
			}
		
//		using (UnityWebRequest www = UnityWebRequest.Get ("https://zackmatthews.com/keypad.txt")) {
//			yield return www.SendWebRequest();
//
//			if (www.isNetworkError || www.isHttpError) {
//				Debug.Log (www.error);
//			} else {
//				// Show results as text
//				Debug.Log (www.downloadHandler.text);
//				pw = www.downloadHandler.text;
//
//				// Or retrieve results as binary data
//				byte[] results = www.downloadHandler.data;
//			}
//		}
	}






//		string url = "";
//		using (WWW www = new WWW(url))
//		{
//			yield return www;
//			pw = www.text;
//		}
//	}

	public void resetColor(){
		MeshRenderer renderer = bg.GetComponent<MeshRenderer> ();
		renderer.enabled = false;
	}
	public bool authenticate(){
		return numText.text.Equals (pw);
	}
}
