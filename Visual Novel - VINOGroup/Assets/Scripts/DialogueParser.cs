using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;


public class DialogueParser : MonoBehaviour {
	public List<SceneDialogue> scenedialogues;

	// Use this for initialization
	void Start () {
		scenedialogues = new List<SceneDialogue> ();
		int level = Application.loadedLevel;
		Debug.Log ("Scene"+(level-2));
		LoadDialogueFromFile ("Scene0");
	//LoadDialogueFromFile ("Scene"+(level-2));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LoadDialogueFromFile(string Filename)
	{
		string file = Application.dataPath +"/Resources/Dialogue_files/" + Filename +".txt";
		StreamReader sr;
		string line;
		int count = 0;
		Regex regx = new Regex ("//.*$");
		using (sr = new StreamReader (file)) {
			do{
				line = sr.ReadLine();
				count ++;
				if (line != null && !regx.IsMatch(line))
				{
					if(line.Trim() != "")
					{
						string[] entries = line.Split('~');
						if (entries.Length > 0)
							LoadList(entries,count);
					}
				}

			}
			while(line != null);
		}

	
	}

	void LoadList(string[] record,int count)
	{
		try{

		SceneDialogue sc = new SceneDialogue (
			record [0].Trim(),
			record [1].Trim(),
			record [2].Trim(),
			record[3].Trim(),
			record[4].Trim(),
				record[5].Trim(),
			int.Parse (record [6].Trim()),
			record[7].Trim()
			);
		scenedialogues.Add (sc);
		//Debug.Log (sc.characher_NAME + "linenumber:" + count);
		//Debug.Log (sc.dialogue + sc.pose);
		}
		catch(Exception E)
		{
			Debug.Log("Error in Line Number: " + count); 
		}
	}	
}



public class SceneDialogue
{
	public SceneDialogue(string character, string Dialogue, string pose, string position,string background,string sound,int gotochoice, string gotochoices)
	{
		this.characher_NAME = character;
		this.dialogue = Dialogue;
		this.pose = pose;
		this.position = position;
		this.background = background;
		this.gotochoice = gotochoice;
		this.gotochoices = gotochoices;
		this.sound = sound;
	}
	public string characher_NAME {
		get;
		set;
	}
	public string dialogue {
		get;
		set;
	}
	public string pose {
		get;
		set;
	}
	public string position {
		get;
		set;
	}
	public string background  {
		get;
		set;
	}
	public string sound {
		get;
		set;
	}
	public int gotochoice {
		get;
		set;
	}
	public string gotochoices
	{
		get;
		set;
	}

}
