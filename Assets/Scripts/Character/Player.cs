using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character
{
	public int water;
    public int ammo = 20;
	public int money = 50;
	public int rescueCount = 0;
	
	public int maxHealth = 1000;
	public int maxWater = 1000;

	public Slider waterBar;
	public Slider healthBar;
	public Text ammoDisp;
	public Text moneyDisp;
	public Text artifactDisp;
	public Text rescueDisp;
	public GameObject projectile;
	public Ray2D mousePos;
	public SpriteRenderer playerImg;

	private bool dehydrating = false;

	public override void BaseStart() 
	{
		base.BaseStart ();
		healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
		waterBar = GameObject.Find("WaterSlider").GetComponent<Slider>();
		ammoDisp = GameObject.Find("AmmoDisplay").GetComponent<Text>();
		moneyDisp = GameObject.Find("MoneyDisplay").GetComponent<Text>();
		artifactDisp = GameObject.Find("ArtifactDisplay").GetComponent<Text>();
		rescueDisp = GameObject.Find("RescueCounter").GetComponent<Text>();

        level.cntVisionRadius = level.baseVisionRadius;
        level.ToggleFog(player.transform.position, level.cntVisionRadius);
		playerImg = GameObject.Find("PlayerImage").GetComponent<SpriteRenderer>();
        if (damage == 0)
        {
            damage = 50;
        }
	}

	public override void BaseUpdate(float dt)
	{
		base.BaseUpdate (dt);

		if (level.gamePaused) {
			// don't do anything if the game is paused
			return;
		}

		// Fire a bullet in the direction of the mouse when you click!
		if (Input.GetMouseButtonDown (0) && ammo > 0) {
			ammo--;
			WriteText ("You threw a rock! You have " + ammo + " left.");
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Quaternion q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);

			GameObject go = (GameObject)Instantiate(projectile, transform.position, q);
			Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D>();
			bulletRb.AddForce(go.transform.up * 1000.0f);
            sound.PlaySound(0);
			Projectile newBullet = go.GetComponent<Projectile>();
			newBullet.damage = Mathf.FloorToInt(damage * level.damageMod);
		}

        if (water == 0)
        {
            health -= 1;
			if (!dehydrating) {
				dehydrating = true;
            	WriteText("You're dehydrating!");
			}
        }
        else
        {
            water -= 1;
			dehydrating = false;
        }

		// Update all the relevant gauges and status displays with the current values
		healthBar.value = health; healthBar.maxValue = maxHealth;
		waterBar.value = water; waterBar.maxValue = maxWater;

		Rect rectangle = healthBar.GetComponent<RectTransform> ().rect;
		healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2( maxHealth*150/1000,rectangle.height);
		rectangle = waterBar.GetComponent<RectTransform> ().rect;
		waterBar.GetComponent<RectTransform> ().sizeDelta = new Vector2( maxWater*150/1000,rectangle.height);

		moneyDisp.text = moneyDisp.text.Substring(0,moneyDisp.text.IndexOf('\n')+1) + "$" + money;
		artifactDisp.text = artifactDisp.text.Substring(0,artifactDisp.text.IndexOf('\n')+1) + level.artifacts + "/" + level.req_artifacts;
		rescueDisp.text = ""; rescueDisp.text += rescueCount;

		/*string rocks = "";
		for (int i=0; i<ammo; i++) {
			rocks += "o";
		}
		ammoDisp.text = ammoDisp.text.Substring(0,ammoDisp.text.IndexOf('\n')+1) + rocks;*/
		ammoDisp.text = "x" + ammo;

		float guiltyColor = 1.0f - (0.8f * level.guilt / 100.0f);
		playerImg.color = new Color (guiltyColor, guiltyColor, guiltyColor);

		// Correct health and water if they're over the max
		if(health > maxHealth){health=maxHealth;}
		if(water > maxWater){water=maxWater;}
	}

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1)
        {
            level.OnPickPart(other.intensity);
            money += Mathf.FloorToInt(other.intensity * level.moneyMod);
			WriteText("You found a cool artifact! It must be valuable.");
            sound.PlaySound(5);
        }
        else if (other.type == 2)
        {
            health += other.intensity;
			WriteText("You gobbled a snack and gained " + Mathf.FloorToInt(other.intensity * level.pickupIntensity) + " health!");
            sound.PlaySound(4);
        }
        else if (other.type == 3)
        {
            water += Mathf.FloorToInt(other.intensity * level.waterMod);
			WriteText("You found some water from a cactus!");
            sound.PlaySound(4);
        }
        else if (other.type == 4)
        {
            ammo += other.intensity;
			if (other.intensity == 1){
				WriteText("You found a rock! Use it well!");
			} else{
				WriteText("You found " + Mathf.FloorToInt(other.intensity * level.pickupIntensity) + " rocks!");
			}
            sound.PlaySound(5);
		}
		else if (other.type == 5)
		{
			level.OnPickPerson(other.intensity);
			rescueCount++;
			WriteText("You saved a person's life! You feel good.");
            sound.PlaySound(10);
		}
		else if (other.type == 6)
		{
			player.money += Mathf.FloorToInt(other.intensity * level.moneyMod);
			WriteText("You found a rare gemstone! Lucky!");
            sound.PlaySound(9);
		}

        GameObject.Destroy(other.gameObject);
    }

	public override void onDeath()
	{

		level.GameOver (this);
	}
}
