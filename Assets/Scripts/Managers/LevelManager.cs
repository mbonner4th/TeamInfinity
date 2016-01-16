using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int parts;
    public int guilt;

    public int[,] tileIDs;
    public Vector4[] tiles;

	public KeyCode menuKey;
	public bool gamePaused = false;
	public GameObject GameMenu;

	void Start()
	{
		//GameMenu = GameObject.Find("PauseMenu");
		//GameMenu.SetActive(false);
	}

	void Update()
    {
		if (GameMenu != null && Input.GetKeyUp (menuKey)) 
		{
			UpdateMenu();
		}
	}

	public void UpdateMenu()
	{
		if(GameMenu.activeSelf)
		{
			GameMenu.SetActive(false);
			Time.timeScale = 1.0f;
			gamePaused = false;
		}
		else
		{
			GameMenu.SetActive(true);
			Time.timeScale = 0;
			gamePaused = true;
		}
	}

	public void ExitToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
		Time.timeScale = 1.0f;
		gamePaused = false;
	}

    public virtual void OnPickPart(int intensity)
    {
        parts += intensity;
    }

    public virtual void OnPickPerson(int intensity)
    {
        guilt -= intensity;
    }
}
