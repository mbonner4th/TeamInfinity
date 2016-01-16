using UnityEngine;
using System.Collections;

public class Player : Character
{
    
	public GameObject projectile;
	public Ray2D mousePos;

	public override void OnCollision(Character other)
    {
        print("Player colliding!");
        GameObject.Destroy(other.gameObject);
    }

	public override void BaseUpdate(float dt)
	{
		base.BaseUpdate (dt);

		// Fire a bullet in the direction of the mouse when you click!
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Quaternion q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);

			GameObject go = (GameObject)Instantiate(projectile, transform.position, q);
			Rigidbody2D bulletRb = go.GetComponent<Rigidbody2D>();
			bulletRb.AddForce(go.transform.up * 1000.0f);
		}
	}
}
