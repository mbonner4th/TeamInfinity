using UnityEngine;
using System.Collections;

public class Projectile : Base
{
	public int team;
	public int countdown = 50;
	public int damage = 20;
	
	public override void BaseStart() 
	{
		countdown = 50;
	}
	
	public override void BaseUpdate(float dt)
	{
		base.BaseUpdate (dt);

		if (level.gamePaused) {
			// don't do anything if the game is paused
			return;
		}

		if (countdown-- <= 0 || level.IsTileSolid(transform.position)) {
			GameObject.Destroy (gameObject);
		}
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		Character otherCharacter = other.GetComponent<Character>();
		if (otherCharacter == null) {
			return;
		}
		
		if (otherCharacter.team != team) {
			OnCollision(otherCharacter);
		}
	}
	
	public virtual void OnCollision(Character other)
	{
		other.health -= damage;
		GameObject.Destroy (gameObject);
	}
}
