using UnityEngine;
using System.Collections;

public class MenuButtonClick : MonoBehaviour {

	public GameObject credits;
	void Start()
	{
		credits.SetActive (false);
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
	public void OnCreditClick()
	{
		credits.SetActive (true);
	}
	public void CreditsBackClick()
	{
		credits.SetActive (false);
	}
	public void OnContinueClick()
	{
		PlayerPrefs.SetInt ("FromContinue", 1);

		Application.LoadLevel (PlayerPrefs.GetInt ("Scene"));
	}
}
