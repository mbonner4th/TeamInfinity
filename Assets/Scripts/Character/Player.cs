using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character
{
	public int water;
    public int ammo;
	public int maxHealth = 1000;
	public int maxWater = 1000;

	public Slider waterBar;
	public Slider healthBar;
	public Text ammoDisp;
	public GameObject projectile;
	public Ray2D mousePos;

	public override void BaseStart() 
	{
		base.BaseStart ();
		healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
		waterBar = GameObject.Find("WaterSlider").GetComponent<Slider>();
		ammoDisp = GameObject.Find("AmmoDisplay").GetComponent<Text>();
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
		}

        if (water == 0)
        {
            health -= 1;
            WriteText("You're dehydrating!");
        }
        else
        {
            water -= 1;
        }

		// Update all the relevant gauges and status displays with the current values
		healthBar.value = health;
		waterBar.value = water;
		//ammoDisp.text = ammoDisp.text.Substring(0,ammoDisp.text.IndexOf('\n')+1) + "x" + ammo;
		string rocks = "";
		for (int i=0; i<ammo; i++) {
			rocks += "o";
		}
		ammoDisp.text = ammoDisp.text.Substring(0,ammoDisp.text.IndexOf('\n')+1) + rocks;

		// Correct health and water if they're over the max
		if(health > maxHealth){health=maxHealth;}
		if(water > maxWater){water=maxWater;}
	}

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1)
        {
            level.OnPickPart(other.intensity);
			WriteText("Picked up a part!");
            sound.PlaySound(2);
        }
        else if (other.type == 2)
        {
            health += other.intensity;
			WriteText("Picked up " + other.intensity + " health!");
            sound.PlaySound(1);
        }
        else if (other.type == 3)
        {
            water += other.intensity;
			WriteText("Picked up water!");
            sound.PlaySound(1);
        }
        else if (other.type == 4)
        {
            ammo += other.intensity;
			WriteText("Picked up " + other.intensity + " rocks!");
            sound.PlaySound(1);
        }
        else if (other.type == 5)
        {
            level.OnPickPerson(other.intensity);
			WriteText("Saved a person!");
        }

        GameObject.Destroy(other.gameObject);
    }

	public override void onDeath()
	{
		level.GameOver (this);
	}
}
