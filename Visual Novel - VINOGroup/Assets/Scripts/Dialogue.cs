using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {
	Stack<int> previouslinenumbers = new Stack<int> ();
	string[] choices1seg;
	string[] choices2seg;
	string[] choices3seg;
	string[] choices4seg;
	public Text dialoguetext;
	List<int> choicesstart;
	bool FromResetChoice,dialoguefinished;
	int choiceends,choicegoto;
	public float letterpause;
	public GameObject BackGround;
	// Panel for choices
	public GameObject screenCanvas;
	public Toggle choice1;
	public Toggle choice2;
	public Toggle choice3;
	public Toggle choice4;
	public Text choicelabel1;
	public Text choicelabel2;
	public Text choicelabel3;
	public Text choicelabel4;
	public Text SaveGameText;
	Coroutine typetextcoroutine;
	int linenumber;
	int endofchoices = -1;
	int endofselectschoice;
	string audiocliploc;
	Sprite background;
	AudioSource audiosource;
	DialogueParser dp;
	public Sprite[] fruitSprites;
	charController cc;
	bool choicepanelvisible;
	public string words = "";

		// Use this for initialization
	void Start () {
		SaveGameText.text = "";
		typetextcoroutine = null;
		dialoguefinished = true;
		choicesstart = new List<int> ();
		choicepanelvisible = false;
		dp = GameObject.Find ("DialogueParser").GetComponent<DialogueParser> ();
		cc = GameObject.Find ("Character").GetComponent<charController> ();
		Debug.Log (PlayerPrefs.GetInt ("FromContinue"));
		if (PlayerPrefs.GetInt ("FromContinue") == 0) {
			linenumber = 0;
		} else {
			linenumber = PlayerPrefs.GetInt ("LineNumber");
			choiceends = PlayerPrefs.GetInt("ChoiceEnds");
			choicegoto = PlayerPrefs.GetInt("Choicegoto");
			endofchoices = PlayerPrefs.GetInt("EndOfChoices");
			//MouseButtonClick(false);
			
		}
	}
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel(0);
		}
	}
	public void MouseButtonClick(bool fromprevious)
	{	
		if (dialoguefinished == true) {
			if (choicepanelvisible == false) {
				if (linenumber < dp.scenedialogues.Count) {

					if (endofchoices != -1) {
						if (linenumber == choiceends)
							linenumber = choicegoto;
					}

					// Load Objects Dynamically.
					//Debug.Log(dp.scenedialogues[linenumber].background);
					if (dp.scenedialogues [linenumber].background != "")
						BackGround.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (dp.scenedialogues [linenumber].background.ToString ()) as Sprite;
					//BackGround.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BG/Scene1/schoolgate") as Sprite;
			
					if (dp.scenedialogues [linenumber].sound != "") {
						AudioClip clipObj = (AudioClip)Resources.Load (dp.scenedialogues [linenumber].sound);
						if (clipObj == null)
							Debug.Log ("Error Loading the sound resource");
						GetComponent<AudioSource> ().clip = clipObj;
						GetComponent<AudioSource> ().Play ();
					}
			
			
					if (dp.scenedialogues [linenumber].gotochoice != 0) {
						endofchoices = dp.scenedialogues [linenumber].gotochoice;
						SetChoices (dp.scenedialogues [linenumber].gotochoices);
					}
					if (typetextcoroutine != null)
						StopCoroutine (typetextcoroutine);
					typetextcoroutine = StartCoroutine (TypeText (dp.scenedialogues [linenumber].dialogue.ToString ()));
					dialoguefinished = false;
					audiocliploc = dp.scenedialogues [linenumber].sound;
					if (audiocliploc != null) {
						// Code Related to Audio
					}
					if (dp.scenedialogues [linenumber].background != "") {
						//code related to background change goes here...
					}
					words = dp.scenedialogues [linenumber].dialogue;
					cc.setcharacter (dp.scenedialogues [linenumber].characher_NAME, dp.scenedialogues [linenumber].pose, dp.scenedialogues [linenumber].position);
					//	StartCoroutine (start ());
					if (fromprevious == false)
						previouslinenumbers.Push (linenumber);
					linenumber++;
				} else {
					if (Application.loadedLevel != Application.levelCount - 1)
					{
						PlayerPrefs.SetInt("UnlockedLevels",PlayerPrefs.GetInt("UnlockedLevels")+1);
						PlayerPrefs.SetInt ("FromContinue", 0);
						Application.LoadLevel (Application.loadedLevel + 1);
					}
					else if(Application.loadedLevel == Application.levelCount-1)
					{
						PlayerPrefs.SetInt("UnlockedLevels",PlayerPrefs.GetInt("UnlockedLevels")+1);
					}
					//else
						//Load End Game Scene...
				}
			}
		} else {
			if (typetextcoroutine != null)
				StopCoroutine (typetextcoroutine);
			dialoguetext.text = dp.scenedialogues [linenumber - 1].dialogue.ToString ();
			dialoguefinished = true;
		}
	}

	void ResetChoices()
	{
		FromResetChoice = true;
		choice1.isOn = false;
		choice2.isOn = false;
		choice3.isOn = false;
		choice4.isOn = false;
	}

	/// <summary>
	/// Sets the choices on the screen as per the document.
	/// </summary>
	/// <param name="gotochoices">Gotochoices.</param>
	void SetChoices(string gotochoices){
		choicesstart.Clear ();
		endofchoices = dp.scenedialogues[linenumber].gotochoice;
			string[] choices = gotochoices.Split('-');
		ResetChoices ();
		if (choices.Length == 2) {
			string[] choices1 = choices [0].Split (':');
			string[] choices2 = choices [1].Split (':');

			choices1seg = choices1[1].Split('%');
			choices2seg = choices2[1].Split('%');

			choicelabel1.text = choices1 [0];
			choicelabel2.text = choices2 [0];
			choicelabel3.text = string.Empty;
			choicelabel4.text = string.Empty;
			choice3.enabled = false;
			choice4.enabled = false;
			GameObject backgroundchoice4 = choice4.transform.GetChild(0).gameObject;
			backgroundchoice4.SetActive(false);
			GameObject backgroundchoice3 = choice3.transform.GetChild(0).gameObject;
			backgroundchoice3.SetActive(false);
		}
		else if (choices.Length == 3) {
			string[] choices1 = choices [0].Split (':');
			string[] choices2 = choices [1].Split (':');
			string[] choices3 = choices[2].Split(':');

			choices1seg = choices1[1].Split('%');
			choices2seg = choices2[1].Split('%');
			choices3seg = choices3[1].Split('%');

			choicelabel1.text = choices1 [0];
			choicelabel2.text = choices2 [0];
			choicelabel3.text = choices3[0];
			choicelabel4.text = string.Empty;
			GameObject backgroundchoice3 = choice3.transform.GetChild(0).gameObject;
			backgroundchoice3.SetActive(true);
			GameObject backgroundchoice4 = choice4.transform.GetChild(0).gameObject;
			backgroundchoice4.SetActive(false);
			choice3.enabled = true;
			choice4.enabled = false;

		}
		else if (choices.Length == 4) {
			string[] choices1 = choices [0].Split (':');
			string[] choices2 = choices [1].Split (':');
			string[] choices3 = choices[2].Split(':');
			string[] choices4 = choices[3].Split(':');
			
			choices1seg = choices1[1].Split('%');
			choices2seg = choices2[1].Split('%');
			choices3seg = choices3[1].Split('%');
			choices4seg = choices4[1].Split('%');
			
			choicelabel1.text = choices1 [0];
			choicelabel2.text = choices2 [0];
			choicelabel3.text = choices3[0];
			choicelabel4.text = choices4[0];
			GameObject backgroundchoice3 = choice3.transform.GetChild(0).gameObject;
			backgroundchoice3.SetActive(true);
			GameObject backgroundchoice4 = choice4.transform.GetChild(0).gameObject;
			backgroundchoice4.SetActive(true);
			choice3.enabled = true;
			choice4.enabled = true;
			
		}
		choicepanelvisible = true;
		screenCanvas.SetActive(true);
		FromResetChoice = false;
	}
	// Update is called once per frame
//	void Update () {
//		if (Input.GetMouseButtonDown (0) && choicepanelvisible == false) {
//	
//			MouseButtonClick();	
//		}
//	}

	public void ToggleValueSelected()
	{

		if (choice1.isOn) {
			linenumber = int.Parse(choices1seg[0].Trim()) - 1;
			if(choices1seg[1].Trim() != string.Empty)
			{
				choiceends = int.Parse(choices1seg[1].Trim()) ;
				choicegoto = int.Parse(choices1seg[2].Trim()) - 1;
			}
			else
				endofchoices = -1;
		}
		else if (choice2.isOn) {
			linenumber = int.Parse(choices2seg[0].Trim())- 1;
			if(choices2seg[1].Trim() != string.Empty)
			{
				choiceends = int.Parse(choices2seg[1].Trim());
				choicegoto = int.Parse(choices2seg[2].Trim())- 1;
			}
			else
				endofchoices = -1;
		}
		else if (choice3.isOn) {
			linenumber = int.Parse(choices3seg[0].Trim())- 1;
			if(choices3seg[1].Trim() != string.Empty)
			{
				choiceends = int.Parse(choices3seg[1].Trim());
				choicegoto = int.Parse(choices3seg[2].Trim())-1;
			}
			else
				endofchoices = -1;
		}
		else if (choice4.isOn) {
			linenumber = int.Parse(choices4seg[0].Trim())- 1;
			if(choices4seg[1].Trim() != string.Empty)
			{
				choiceends = int.Parse(choices4seg[1].Trim());
				choicegoto = int.Parse(choices4seg[2].Trim())-1;
			}
			else
				endofchoices = -1;
		}

		choicepanelvisible = false;
		screenCanvas.SetActive(false);
		if(FromResetChoice == false)
		MouseButtonClick (false);

	}
	IEnumerator TypeText (string dialogue) {

		dialoguetext.text = string.Empty;
		foreach (char letter in dialogue.ToCharArray()) {
			dialoguetext.text += letter;
			yield return new WaitForSeconds (letterpause);
		} 
		dialoguefinished = true;
	}

	public void previousclick()
{
		previouslinenumbers.Pop ();
               linenumber = previouslinenumbers.Pop ();
		MouseButtonClick (true);
	}

	IEnumerator SavingGame () {
		
		PlayerPrefs.SetInt ("LineNumber", linenumber-1);
		PlayerPrefs.SetInt ("Scene",Application.loadedLevel);
		PlayerPrefs.SetInt("ChoiceEnds",choiceends);
		PlayerPrefs.SetInt("Choicegoto",choicegoto);
		PlayerPrefs.SetInt("EndOfChoices",endofchoices);
		SaveGameText.text = "Saved...";
		yield return new WaitForSeconds(2);
		SaveGameText.text = "";
	}

	public void SaveButtonClick()
	{
		StartCoroutine (SavingGame());
//		PlayerPrefs.SetString ("Dialoguefinished", true);
	//	PlayerPrefs.SetString("choicepanelvisible",choicepanelvisible.ToString());
	//	PlayerPrefs.SetInt ("EndOfChoices", endofchoices.ToString());
	//	typetextcoroutine = null;
	//	dialoguefinished = true;
	//	choicesstart = new List<int> ();
	//	choicepanelvisible = false;
	}
	

}
