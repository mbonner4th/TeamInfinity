using UnityEngine;
using System.Collections;

public class FaceMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);
		transform.rotation = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
	}
}
