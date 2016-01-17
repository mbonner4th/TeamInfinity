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
            if (!level.IsTileSolid(transform.position + speed * axis)) {
                transform.Translate(speed * axis);
            }
        } else if (Input.GetKeyDown(backward)) {
            if (!level.IsTileSolid(transform.position - speed * axis)) {
                transform.Translate(-speed * axis);
            }
        }
    }
}
