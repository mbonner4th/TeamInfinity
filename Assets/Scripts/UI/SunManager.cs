using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SunManager : Base {

	public Text sunText;
	public Text flashText;
	public GameObject SunDisplay;
	public GameObject MoonDisplay;
	public Image nightShade;

	public override void BaseStart ()
	{
		sunText = GameObject.Find("SunCounter").GetComponent<Text>();
		flashText = GameObject.Find("FlashCounter").GetComponent<Text>();
		nightShade = GameObject.Find("NightShade").GetComponent<Image>();
	}

	public override void BaseUpdate (float dt)
	{
		sunText.text = ""; sunText.text += level.time;
		float sunMod = 1.0f - (level.time - 20.0f)/40.0f; if(sunMod >= 1.0f){sunMod = 1.0f;}
		sunText.color = new Color ((255.0f-155.0f*sunMod)/255.0f, (150.0f-150.0f*sunMod)/255.0f, (0+255.0f*sunMod)/255.0f);
		flashText.text = ""; flashText.text += level.flashlightLvl;

		sunMod = 1.0f - (level.time - 25.0f)/25.0f; 
		if(sunMod >= 1.0f){sunMod = 1.0f;} if(sunMod <= 0.0f){sunMod = 0.0f;}
		nightShade.color = new Color(nightShade.color.r, nightShade.color.g, nightShade.color.b, (0+135.0f*sunMod)/255.0f);

		if (level.time <= level.maxTime/2) {
			SunDisplay.SetActive (false);
			MoonDisplay.SetActive (true);
		} else {
			SunDisplay.SetActive (true);
			MoonDisplay.SetActive (false);
		}
	}
}
