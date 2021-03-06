﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public Slider[] sliders;
    public string[] settings;

    public void Start()
    {
        if (PlayerPrefs.GetInt("muteSound", 0) == 1) {
            GameObject.Find("MuteSound").GetComponent<Toggle>().isOn = true;
        } else {
            GameObject.Find("MuteSound").GetComponent<Toggle>().isOn = false;
        }

        if (PlayerPrefs.GetInt("muteMusic", 0) == 1) {
            GameObject.Find("MuteMusic").GetComponent<Toggle>().isOn = true;
        } else {
            GameObject.Find("MuteMusic").GetComponent<Toggle>().isOn = false;
        }

        GameObject.Find("Options").SetActive(false);

        for (int i = 0; i < sliders.Length; ++i) {
            if (settings[i] == "difficulty") {
                sliders[i].value = (Mathf.Pow(PlayerPrefs.GetFloat(settings[i], 1.0f), -1.0f)) * 10.0f;
            } else {
                sliders[i].value = PlayerPrefs.GetFloat(settings[i], 1.0f);
            }
        }
    }

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

        if (settings[sliderIndex] == "difficulty") {
            PlayerPrefs.SetFloat(settings[sliderIndex], 10.0f / setting);
        } else {
            PlayerPrefs.SetFloat(settings[sliderIndex], setting);
        }
    }

    public void MuteSound()
    {
        if (GameObject.Find("MuteSound").GetComponent<Toggle>().isOn) {
            PlayerPrefs.SetInt("muteSound", 1);
        } else {
            PlayerPrefs.SetInt("muteSound", 0);
        }
    }

    public void MuteMusic()
    {
        if (GameObject.Find("MuteMusic").GetComponent<Toggle>().isOn) {
            PlayerPrefs.SetInt("muteMusic", 1);
        } else {
            PlayerPrefs.SetInt("muteMusic", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
