using UnityEngine;
using System.Collections;

public class Enemy : Character {

    public int damage = 1;

    public override void BaseStart()
    {
        base.BaseStart();
    }

    public override void BaseUpdate(float dt)
    {
        base.BaseUpdate(dt);        
    }

	public override void OnCollision(Character other)
    {
        //WriteText("Player hit, Get him");
		other.health -= damage;
    }
}
