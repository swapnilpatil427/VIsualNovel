using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class charController : MonoBehaviour {
	public GameObject leftcharposition;
	public GameObject middlecharposition;
	public GameObject rightcharposition;
	public Text chartext;
	public Image characterLogo;
	// Use this for initialization
	void Start () {

	}

	public void setcharacter(string name,string character, string position)
	{
		Sprite charsprite;
		leftcharposition.GetComponent<SpriteRenderer> ().sprite = null;
		middlecharposition.GetComponent<SpriteRenderer> ().sprite = null;
			rightcharposition.GetComponent<SpriteRenderer>().sprite = null;
		chartext.text = name.ToUpper();
		if (character != string.Empty && character != "0") {
			charsprite = Resources.Load<Sprite> (character) as Sprite;
			if (position.Contains ("L")) {
				leftcharposition.GetComponent<SpriteRenderer> ().sprite = charsprite;
			} else if (position.Contains ("M")) {

				middlecharposition.GetComponent<SpriteRenderer> ().sprite = charsprite;
			} else if (position.Contains ("R")) {
				rightcharposition.GetComponent<SpriteRenderer> ().sprite = charsprite;
			}
		
			chartext.text = name.ToUpper ();
			characterLogo.GetComponent<Image> ().sprite = charsprite;
		}
		//Debug.Log (name);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
