using UnityEngine;
using System.Collections;

public class Movement : Base
{
    public KeyCode forward;
    public KeyCode backward;
    public Vector3 axis;
    public float speed;

    public override void BaseStart()
    {

    }

    public override void BaseUpdate(float dt)
    {
        if (Input.GetKeyDown(forward)) {
            int tile = level.GetTile(transform.position + speed * axis);
            if (tile != -1 && !level.tiles[tile]) {
                transform.Translate(speed * axis);
            }
        } else if (Input.GetKeyDown(backward)) {
            int tile = level.GetTile(transform.position - speed * axis);
            if (tile != -1 && !level.tiles[tile]) {
                transform.Translate(-speed * axis);
            }
        }
    }
}
