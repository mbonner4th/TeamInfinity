using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour {

	private Player player;
	private Slider waterBar;
	private Slider healthBar;

	public int healthIncr = 50;
	public int maxHealthIncr = 100;
	public int waterIncr = 50;
	public int maxWaterIncr = 100;
	public int ammoIncr = 1;
	public int damageIncr = 10;

	public int[] prices = new int[6];

	/*public int healthPrice = 5;
	public int waterPrice = 5;
	public int ammoPrice = 5;
	public int maxHealthPrice = 25;
	public int maxWaterPrice = 25;
	public int damagePrice = 25;*/

	public void Start ()
	{
		prices[0] = 5; //healthPrice
		prices[1] = 5; //waterPrice
		prices[2] = 5; //ammoPrice
		prices[3] = 25; //maxHealthPrice
		prices[4] = 25; //maxWaterPrice
		prices[5] = 25; //damagePrice
	}

	public void Update()
	{
		Text shopText = GameObject.Find("ShopText").GetComponent<Text>();
		shopText.text = "You come across a mysterious vending machine. You have $" + player.money + ".";

		healthBar.value = player.health; healthBar.maxValue = player.maxHealth;
		waterBar.value = player.water; waterBar.maxValue = player.maxWater;
		player.moneyDisp.text = player.moneyDisp.text.Substring(0,player.moneyDisp.text.IndexOf('\n')+1) + "$" + player.money;
		player.artifactDisp.text = player.artifactDisp.text.Substring(0,player.artifactDisp.text.IndexOf('\n')+1) + player.level.artifacts + "/" + player.level.req_artifacts;
		string rocks = "";
		for (int i=0; i<player.ammo; i++) {
			rocks += "o";
		}
		player.ammoDisp.text = player.ammoDisp.text.Substring(0,player.ammoDisp.text.IndexOf('\n')+1) + rocks;
		if(player.health > player.maxHealth){player.health=player.maxHealth;}
		if(player.water > player.maxWater){player.water=player.maxWater;}
	}

	public void PrepareShop(Player customer)
	{
		player = customer;
		healthBar = GameObject.Find("ShopHealthSlider").GetComponent<Slider>();
		waterBar = GameObject.Find("ShopWaterSlider").GetComponent<Slider>();
	}
	
	public void shopRestoreHealth()
	{
		if (player.money >= prices[0]) {
			player.health += healthIncr;
			player.money -= prices[0];
			prices[0] += 5;
		}
	}

	public void shopUpgradeHealth()
	{
		if (player.money >= prices[3]) {
			player.maxHealth += maxHealthIncr;
			player.health += healthIncr;
			player.money -= prices[3];
			prices[3] += 10;
		}
	}
	
	public void shopRestoreWater()
	{
		if (player.money >= prices[1]) {
			player.water += waterIncr;
			player.money -= prices[1];
			prices[1] += 5;
		}
	}
	
	public void shopUpgradeWater()
	{
		if (player.money >= prices[4]) {
			player.maxWater += maxWaterIncr;
			player.water += waterIncr;
			player.money -= prices[4];
			prices[4] += 10;
		}
	}
	
	public void shopRestoreAmmo()
	{
		if (player.money >= prices[2]) {
			player.ammo += ammoIncr;
			player.money -= prices[2];
			prices[2] += 2;
		}
	}
	
	public void shopUpgradeDamage()
	{
		if (player.money >= prices[5]) {
			player.damage += damageIncr;
			player.money -= prices[5];
			prices[5] += 10;
		}
	}

	/*public void shopping(int healthUp, int maxHealthUp, int waterUp, int maxWaterUp, int ammoUp, int damageUp)
	{
		player.maxHealth += maxHealthUp;
		player.health += healthUp + maxHealthUp;
		player.maxWater += maxWaterUp;
		player.water += waterUp + maxWaterUp;
		player.ammo += ammoUp;
		player.damage += damageUp;
	}*/
}
