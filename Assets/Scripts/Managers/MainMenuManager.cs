using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public Slider[] sliders;
    public string[] settings;

    public void LoadLevel(int index)
    {
        Application.LoadLevel(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetSetting(int sliderIndex)
    {
        float setting = sliders[sliderIndex].value;
        print("Changing settings");
        PlayerPrefs.SetFloat(settings[sliderIndex], setting);
    }

    public void MuteVolume()
    {
        PlayerPrefs.SetFloat(settings[3], 0);
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
