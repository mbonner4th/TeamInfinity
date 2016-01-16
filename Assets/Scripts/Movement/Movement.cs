using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public KeyCode forward;
    public KeyCode backward;
    public Vector3 axis;
    public uint speed;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(forward)) {
            transform.Translate(speed * axis);
        } else if (Input.GetKeyDown(backward)) {
            transform.Translate(-speed * axis);
        }
    }
}
