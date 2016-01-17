using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character
{
	public int water;
    public int ammo;
	public int money = 0;
	public int damage = 50;
	public int maxHealth = 1000;
	public int maxWater = 1000;

	public Slider waterBar;
	public Slider healthBar;
	public Text ammoDisp;
	public Text moneyDisp;
	public Text artifactDisp;
	public GameObject projectile;
	public Ray2D mousePos;

	private bool dehydrating = false;

	public override void BaseStart() 
	{
		base.BaseStart ();
		healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
		waterBar = GameObject.Find("WaterSlider").GetComponent<Slider>();
		ammoDisp = GameObject.Find("AmmoDisplay").GetComponent<Text>();
		moneyDisp = GameObject.Find("MoneyDisplay").GetComponent<Text>();
		artifactDisp = GameObject.Find("ArtifactDisplay").GetComponent<Text>();
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
			WriteText ("Pew! " + ammo + " ROCKS remaining!");
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Quaternion q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);

			GameObject go = (GameObject)Instantiate(projectile, transform.position, q);
			Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D>();
			bulletRb.AddForce(go.transform.up * 1000.0f);
            sound.PlaySound(0);
			Projectile newBullet = go.GetComponent<Projectile>();
			newBullet.damage = damage;
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

		string rocks = "";
		for (int i=0; i<ammo; i++) {
			rocks += "o";
		}
		ammoDisp.text = ammoDisp.text.Substring(0,ammoDisp.text.IndexOf('\n')+1) + rocks;
		//ammoDisp.text = ammoDisp.text.Substring(0,ammoDisp.text.IndexOf('\n')+1) + "x" + ammo;


		// Correct health and water if they're over the max
		if(health > maxHealth){health=maxHealth;}
		if(water > maxWater){water=maxWater;}
	}

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1)
        {
            level.OnPickPart(other.intensity);
			money += other.intensity;
			WriteText("You found a cool artifact! It must be valuable.");
            sound.PlaySound(2);
        }
        else if (other.type == 2)
        {
            health += other.intensity;
			WriteText("You gobbled a snack and gained " + other.intensity + " health!");
            sound.PlaySound(1);
        }
        else if (other.type == 3)
        {
            water += other.intensity;
			WriteText("You found a bit of water!");
            sound.PlaySound(1);
        }
        else if (other.type == 4)
        {
            ammo += other.intensity;
			if (other.intensity == 1){
				WriteText("You found a rock! Use it well!");
			} else{
				WriteText("You found " + other.intensity + " rocks!");
			}
            sound.PlaySound(1);
		}
		else if (other.type == 5)
		{
			level.OnPickPerson(other.intensity);
			WriteText("You saved a person's life! You feel good.");
		}
		else if (other.type == 6)
		{
			player.money += other.intensity;
			WriteText("You found some gold coins! Lucky!");
		}

        GameObject.Destroy(other.gameObject);
    }

	public override void onDeath()
	{
		level.GameOver (this);
	}
}
