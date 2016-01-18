using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public Text loseText;
	public GameObject GameOverButtons;
	public string outText = "";
	public bool giveUp = false;

	public void GameOver(Player lastPlayer, int guilt, int artifacts, float light, float time)
	{
		GameOverButtons.SetActive (false);
		loseText = GameObject.Find("LastText").GetComponent<Text>();
		outText = "";
		string dramaText = "";
		
		if (lastPlayer.water == 0) {
			dramaText += "You didn't have enough water.\n";
		}
		if (lastPlayer.health == 0) {
			dramaText += "You didn't have enough food.\n";
		}
		if (lastPlayer.ammo == 0) {
			dramaText += "You didn't have enough rocks.\n";
		}
		if (guilt >= 15) {
			dramaText += "You didn't have enough love.\n";
		}
		if (lastPlayer.money == 0) {
			dramaText += "You didn't have enough money.\n";
		}
		if (artifacts == 0) {
			dramaText += "You didn't have enough artifacts.\n";
		}
		if (light == 0 && time <= 30) {
			dramaText += "You didn't have enough light.\n";
		}
		StartCoroutine(DramaticText (dramaText));
	}

	public IEnumerator DramaticText(string strComplete)
	{
		int i = outText.Length;
		while (i < strComplete.Length) {
			outText += strComplete [i++];
			loseText.text = outText;
			if (!giveUp) {
				yield return new WaitForSeconds (0.02F);
			} else {
				yield return new WaitForSeconds (0.1F);
			}
		}

		yield return new WaitForSeconds (0.5F);
		if (!giveUp) {
			GameOverButtons.SetActive (true);
		} else {
			giveUp = false;
			Application.LoadLevel ("MainMenu");
		}
	}

	public void GiveUp()
	{
		if (!giveUp) {
			giveUp = true;
			string dramaText = outText;
			dramaText += "You didn't have enough... DETERMINATION.";
			StartCoroutine (DramaticText (dramaText));
		}
	}
}
