using UnityEngine;
using System.Collections;

public class Character : Base
{
    public int team;

    public override void BaseStart() 
    {
	
	}

    public override void BaseUpdate(float dt)
    {
	
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

    }
}
