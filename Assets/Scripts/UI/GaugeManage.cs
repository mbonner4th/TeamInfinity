using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GaugeManage : MonoBehaviour {

	public Text textBox;
	public Slider healthBar;

	// Use this for initialization
	void Start () {
		textBox = gameObject.GetComponent<Text>();
		healthBar = transform.parent.gameObject.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		int val = (int)healthBar.value;
		textBox.text = "";
		textBox.text += val;
	}
}
