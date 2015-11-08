using UnityEngine;
using System.Collections;

public class MenuButtonClick : MonoBehaviour {

	// Use this for initialization
	public void OnPlayClick () {
		Application.LoadLevel (2);
	}
	
	// Update is called once per frame
	public void OnSceneSelectClick () {
		Application.LoadLevel (1);
	}
}
