using UnityEngine;
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
        if (playerObject != null) {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null) {
            sound = camera.GetComponent<SoundManager>();
        }

		textBox = GameObject.Find("TextBox").GetComponent<Text>();
        BaseStart();
	}

    public virtual void BaseStart()
    {

    }
	
	void Update()
    {
        float dt = Time.deltaTime;
        playerObject = GameObject.Find("Player");
        if (playerObject != null) {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null) {
            sound = camera.GetComponent<SoundManager>();
        }
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
