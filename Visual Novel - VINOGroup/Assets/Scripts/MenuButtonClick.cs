using UnityEngine;
using System.Collections;

public class MenuButtonClick : MonoBehaviour {


	void Start()
	{
		//PlayerPrefs.SetInt ("UnlockedLevels", 0);
	}
	// Use this for initialization
	public void OnPlayClick () {
		PlayerPrefs.SetInt ("FromContinue", 0);
		Application.LoadLevel (2);
	}
	
	// Update is called once per frame
	public void OnSceneSelectClick () {
		Debug.Log (PlayerPrefs.GetInt ("Scene"));
		Application.LoadLevel (1);
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
	public void onexitclick()
	{
		Application.Quit ();
	}

	public void OnContinueClick()
	{
		PlayerPrefs.SetInt ("FromContinue", 1);

		Application.LoadLevel (PlayerPrefs.GetInt ("Scene"));
	}
}
