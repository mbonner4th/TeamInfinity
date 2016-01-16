using UnityEngine;
using System.Collections;

public class Player : Character
{
    public override void OnCollision(Character other)
    {
        print("Player colliding!");
        GameObject.Destroy(other.gameObject);
    }

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1) {
            health += other.intensity;
        }

        GameObject.Destroy(other.gameObject);
    }
}
