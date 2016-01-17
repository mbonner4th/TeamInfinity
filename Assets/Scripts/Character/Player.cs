using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character
{
    public int water;
    public int ammo;

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

	public override void OnCollision(Character other)
    {
        //print("Player colliding!");
        //GameObject.Destroy(other.gameObject);
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
	}

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1)
        {
            level.OnPickPart(other.intensity);
            print("Picked up a part!");
        }
        else if (other.type == 2)
        {
            health += other.intensity;
			if(health > 100){health=100;}
			print("Picked up health!");
        }
        else if (other.type == 3)
        {
            water += other.intensity;
			if(water > 100){water=100;}
            print("Picked up water!");
        }
        else if (other.type == 4)
        {
            ammo += other.intensity;
            print("Picked up ammo!");
        }
        else if (other.type == 5)
        {
            level.OnPickPerson(other.intensity);
            print("Saved a person!");
        }

        GameObject.Destroy(other.gameObject);
    }
}
