using UnityEngine;
using System.Collections;

public class RealTimeMovement : MonoBehaviour
{
    public KeyCode forward;
    public KeyCode backward;
    public Vector3 axis;
    public float currentSpeed;
    public float maxSpeed = 9999999.0f;
    public float acceleration;

	void Start()
    {
        currentSpeed = 0;
	}
	
	void Update()
    {
        float dt = Time.deltaTime;

        if (Input.GetKey(forward)) {
            if (currentSpeed < 0) { currentSpeed = 0; }
            currentSpeed += acceleration * dt;
        } else if (Input.GetKey(backward)) {
            if (currentSpeed > 0) { currentSpeed = 0; }
            currentSpeed -= acceleration * dt;
        } else {
            currentSpeed = 0;
        }

        transform.Translate(currentSpeed * axis);
	}
}
