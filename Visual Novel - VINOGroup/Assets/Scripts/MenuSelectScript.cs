using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelectScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("FromContinue", 0);

		// TO Hide and Unhide Unlocked Levels
		int UnlockedLevels = PlayerPrefs.GetInt ("UnlockedLevels")+1;
		Debug.Log (PlayerPrefs.GetInt ("UnlockedLevels"));
		for(int i=0;i< Application.levelCount-2;i++)
		{
			GameObject.Find("Scene"+i).SetActive(true);
		}
		for(int i=UnlockedLevels;i< Application.levelCount-2;i++)
		{
			GameObject.Find("Scene"+i).SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
	}

	public void Scene1Click()
	{

		Application.LoadLevel (3);
	}
	public void Scene0Click()
	{
		
		Application.LoadLevel (2);
	}
	public void Scene2Click()
	{
		Application.LoadLevel (4);
	}
}
