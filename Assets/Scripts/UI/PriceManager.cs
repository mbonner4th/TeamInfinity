using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PriceManager : MonoBehaviour {

	public ShopManager shop;
	public Text textBox;
	public int priceIndex;

	// Use this for initialization
	void Start () {
		shop = transform.parent.parent.gameObject.GetComponent<ShopManager>();
		textBox = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		textBox.text = "$" + shop.prices[priceIndex] + " " + textBox.text.Substring(textBox.text.IndexOf('-'));
		//print (textBox.text.Substring (textBox.text.IndexOf ('-') + 1));
	}
}
