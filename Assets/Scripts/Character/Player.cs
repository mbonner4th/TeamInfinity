using UnityEngine;
using System.Collections;

public class Player : Character
{
    public int water;
    public int ammo;

    public override void OnCollision(Character other)
    {
        //print("Player colliding!");
        GameObject.Destroy(other.gameObject);
    }

    public override void OnPickup(Pickup other)
    {
        if (other.type == 1) {
            level.OnPickPart(other.intensity);
            //print("Picked up a part!");
        }
        else if (other.type == 2)
        {
            health += other.intensity;
            //print("Picked up health!");
        }
        else if (other.type == 3)
        {
            water += other.intensity;
            //print("Picked up water!");
        }
        else if (other.type == 4)
        {
            ammo += other.intensity;
            //print("Picked up ammo!");
        }
        else if (other.type == 5)
        {
            level.OnPickPerson(other.intensity);
            //print("Saved a person!");
        }

        GameObject.Destroy(other.gameObject);
    }
}
