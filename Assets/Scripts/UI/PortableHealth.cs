using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PortableHealth : MonoBehaviour {

	public Character tracking;
	public Slider healthBar;

	// Use this for initialization
	void Start () {
		tracking = transform.parent.parent.gameObject.GetComponent<Character>();
		healthBar = gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.WorldToScreenPoint(new Vector2(tracking.transform.position.x,
		                                                                tracking.transform.position.y+0.3f));
		healthBar.value = tracking.health;
	}
}
