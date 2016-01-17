﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Base : MonoBehaviour
{
    public LevelManager level;
    public GameObject playerObject;
    public SoundManager sound;
    public Player player;
	public Text textBox;

	void Start()
    {
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerObject = GameObject.Find("Player");
        player = GameObject.Find("LevelManager").GetComponent<Player>();
		textBox = GameObject.Find("TextBox").GetComponent<Text>();
        sound = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        BaseStart();
        print("playing sound!");
        sound.PlaySound(1);
	}

    public virtual void BaseStart()
    {

    }
	
	void Update()
    {
        float dt = Time.deltaTime;
        BaseUpdate(dt);
	}

    public virtual void BaseUpdate(float dt)
    {

	}
	
	public void WriteText(string newText)
	{
		textBox.text += '\n' + newText;
		while (textBox.text.Split ('\n').Length > 4) {
			textBox.text = textBox.text.Substring (textBox.text.IndexOf ('\n') + 1);
		}
	}
}
