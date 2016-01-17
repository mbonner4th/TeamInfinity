using UnityEngine;
using System.Collections;

public class Paintable : Clickable
{
    public GameObject shownPickup;

    public void Start()
    {
        base.Start();
        GameObject newObject = (GameObject)GameObject.Instantiate(manager.pastableObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        shownPickup = newObject;
    }

    public override void OnClick()
    {
        manager.PaintTile(this);
    }
}
