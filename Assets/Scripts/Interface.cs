using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {

	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.Box(new Rect(Screen.width*.50f,Screen.height*.80f,Screen.width*.50f,Screen.height*.20f), "Sup");
	}
}
