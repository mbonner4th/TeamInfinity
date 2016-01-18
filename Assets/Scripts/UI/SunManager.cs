using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunManager : Base {

	public Text sunText;
	public Text flashText;
	public GameObject SunDisplay;
	public GameObject MoonDisplay;

	public override void BaseStart ()
	{
		sunText = GameObject.Find("SunCounter").GetComponent<Text>();
		flashText = GameObject.Find("FlashCounter").GetComponent<Text>();
	}

	public override void BaseUpdate (float dt)
	{
		sunText.text = ""; sunText.text += level.time;
		float sunMod = 1.0f - (level.time - level.maxTime / 2.0f)/30.0f; if(sunMod <= 0.0f){sunMod = 0.0f;}
		sunText.color = new Color ((255.0f-155.0f*sunMod)/255.0f, (150.0f-150.0f*sunMod)/255.0f, (0+255.0f*sunMod)/255.0f);
		flashText.text = ""; flashText.text += level.flashlightLvl;

		if (level.time <= level.maxTime/2) {
			SunDisplay.SetActive (false);
			MoonDisplay.SetActive (true);
		} else {
			SunDisplay.SetActive (true);
			MoonDisplay.SetActive (false);
		}
	}
}
