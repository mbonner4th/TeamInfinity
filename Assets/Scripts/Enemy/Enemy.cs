using UnityEngine;
using System.Collections;

public class Enemy : Character {

	public GameObject drop;

    public void Awake()
    {
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        if (!level.addToEnemies(this.gameObject))
        {
            GameObject.Destroy(this.gameObject);
        }

      //  Debug.Log("i'm awake");

    }

    public override void BaseStart()
    {
        base.BaseStart();
        if (damage == 0)
        {
            damage = 10;
        }
    }

    public override void BaseUpdate(float dt)
    {
        base.BaseUpdate(dt);
    }

	public override void OnCollision(Character other)
    {
		//other.health -= damage;
    }

	public override void onDeath ()
	{
		if (drop != null) {
			level.cleanUp.Add((GameObject)GameObject.Instantiate (drop, transform.position, Quaternion.identity));
		}
		base.onDeath();
	}

}
