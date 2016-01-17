using UnityEngine;
using System.Collections;

public class FaceMouse : Base {

	// Update is called once per frame
	void Update () {
		if (level.gamePaused) {
			// don't do anything if the game is paused
			return;
		}

		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);
		transform.rotation = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
	}
}
