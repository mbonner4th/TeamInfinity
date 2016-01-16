using UnityEngine;
using System.Collections;

public class Character : Base
{
    public int team;
    public int health;

    public override void BaseStart() 
    {
	
	}

    public override void BaseUpdate(float dt)
    {
		if (health <= 0) {
			onDeath ();
		}
	}

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Character otherCharacter = other.GetComponent<Character>();
        if (otherCharacter == null) {
            return;
        }

        if (otherCharacter.team == 10) {
            OnPickup(other.GetComponent<Pickup>());
        } else if (otherCharacter.team != team) {
            OnCollision(otherCharacter);
        }
    }

    public virtual void OnCollision(Character other)
    {

    }

    public virtual void OnPickup(Pickup pickup)
    {

    }

	public virtual void onDeath()
	{
		GameObject.Destroy (gameObject);
	}
}
