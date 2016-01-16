using UnityEngine;
using System.Collections;

public class Projectile : Base
{
	public int team;
	public int countdown;
	
	public override void BaseStart() 
	{
		countdown = 50;
	}
	
	public override void BaseUpdate(float dt)
	{
		if (countdown-- <= 0) {
			GameObject.Destroy (gameObject);
		}
	}
	
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		Character otherCharacter = other.GetComponent<Character>();
		if (otherCharacter == null) {
			return;
		}
		
		//print("Collide!!");
		if (otherCharacter.team != team) {
			OnCollision(otherCharacter);
		}
	}
	
	public virtual void OnCollision(Character other)
	{
		print("Player colliding!");
		GameObject.Destroy(other.gameObject);
		GameObject.Destroy (gameObject);
	}
}
