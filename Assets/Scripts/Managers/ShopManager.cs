﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour {

	private Player player;
	private Slider waterBar;
	private Slider healthBar;

	public int healthIncr = 300;
	public int maxHealthIncr = 200;
	public int waterIncr = 300;
	public int maxWaterIncr = 200;
	public int ammoIncr = 3;
	public int damageIncr = 15;
	public int batteryIncr = 10;
	public int visionIncr = 1;

	public int[] prices = new int[9];

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
		prices[6] = 2; //batteryPrice
		prices[7] = 10; //maxBatteryPrice
		prices[8] = 30; //visionPrice
	}

	public void Update()
	{
		Text shopText = GameObject.Find("ShopText").GetComponent<Text>();
		shopText.text = "You come across a mysterious vending machine. You have $" + player.money + ".";

		healthBar.value = player.health; healthBar.maxValue = player.maxHealth;
		waterBar.value = player.water; waterBar.maxValue = player.maxWater;

		Rect rectangle = healthBar.GetComponent<RectTransform> ().rect;
		healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2( player.maxHealth*150/1000,rectangle.height);
		rectangle = waterBar.GetComponent<RectTransform> ().rect;
		waterBar.GetComponent<RectTransform> ().sizeDelta = new Vector2( player.maxWater*150/1000,rectangle.height);

		// Update all the relevant gauges and status displays with the current values
		player.moneyDisp.text = player.moneyDisp.text.Substring(0,player.moneyDisp.text.IndexOf('\n')+1) + "$" + player.money;
		player.artifactDisp.text = player.artifactDisp.text.Substring(0,player.artifactDisp.text.IndexOf('\n')+1) + player.level.artifacts + "/" + player.level.req_artifacts;
		player.rescueDisp.text = ""; player.rescueDisp.text += player.rescueCount;

		player.ammoDisp.text = "x" + player.ammo;
		
		float guiltyColor = 1.0f - (0.8f * player.level.guilt / 100.0f);
		player.playerImg.color = new Color (guiltyColor, guiltyColor, guiltyColor);

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
			prices[0] += 1;
		}
	}

	public void shopUpgradeHealth()
	{
		if (player.money >= prices[3]) {
			player.maxHealth += maxHealthIncr;
			player.health += maxHealthIncr;
			player.money -= prices[3];
			prices[3] += 5;
		}
	}
	
	public void shopRestoreWater()
	{
		if (player.money >= prices[1]) {
			player.water += waterIncr;
			player.money -= prices[1];
			prices[1] += 1;
		}
	}
	
	public void shopUpgradeWater()
	{
		if (player.money >= prices[4]) {
			player.maxWater += maxWaterIncr;
			player.water += maxWaterIncr;
			player.money -= prices[4];
			prices[4] += 5;
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
			prices[5] += 5;
		}
	}
	
	public void shopRestoreBattery()
	{
		if (player.money >= prices[6]) {
			player.level.flashlightLvl = player.level.maxFlashlightLvl;
			player.money -= prices[6];
			prices[6] += 1;
		}
	}
	
	public void shopUpgradeBattery()
	{
		if (player.money >= prices[7]) {
			player.level.maxFlashlightLvl += batteryIncr;
			player.level.flashlightLvl = player.level.maxFlashlightLvl;
			player.money -= prices[7];
			prices[7] += 15;
		}
	}

	public void shopUpgradeVision()
	{
		if (player.money >= prices[8]) {
			player.level.baseVisionRadius += visionIncr;
			player.money -= prices[8];
			prices[8] += 50;
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
