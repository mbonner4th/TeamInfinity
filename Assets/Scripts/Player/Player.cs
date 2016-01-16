using UnityEngine;
using System.Collections;

public class Player : Character
{
    public override void OnCollision(Character other)
    {
        print("Player colliding!");
        GameObject.Destroy(other.gameObject);
    }
}
