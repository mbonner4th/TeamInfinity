using UnityEngine;
using System.Collections;

public class ShopManager : Base {

	public int healthIncr = 50;
	public int maxHealthIncr = 100;
	public int waterIncr = 50;
	public int maxWaterIncr = 100;
	public int ammoIncr = 1;
	public int damageIncr = 10;

	public int healthPrice = 5;
	public int waterPrice = 5;
	public int ammoPrice = 5;
	public int maxHealthPrice = 25;
	public int maxWaterPrice = 25;
	public int damagePrice = 25;


	public void shopUpgradeHealth()
	{
		if (player.money >= maxHealthPrice) {
			player.maxHealth += maxHealthIncr;
			player.health += healthIncr;
			player.money -= maxHealthPrice;
		}
	}

	public void shopRestoreHealth()
	{
		if (player.money >= healthPrice) {
			player.health += healthIncr;
			player.money -= healthPrice;
		}
	}

	public void shopUpgradeWater()
	{
		if (player.money >= maxWaterPrice) {
			player.maxWater += maxWaterIncr;
			player.water += waterIncr;
			player.money -= maxWaterPrice;
		}
	}
	
	public void shopRestoreWater()
	{
		if (player.money >= waterPrice) {
			player.water += waterIncr;
			player.money -= waterPrice;
		}
	}
	
	public void shopRestoreAmmo()
	{
		if (player.money >= ammoPrice) {
			player.ammo += ammoIncr;
			player.money -= ammoPrice;
		}
	}
	
	public void shopUpgradeDamage()
	{
		if (player.money >= damagePrice) {
			player.damage += damageIncr;
			player.money -= damagePrice;
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
