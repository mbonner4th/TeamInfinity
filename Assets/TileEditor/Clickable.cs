using UnityEngine;
using System.Collections;

public class Clickable : TileEditorBase
{
    public float hitWidth = 45.0f;
    public float hitHeight = 45.0f;

	// Use this for initialization
	void Start()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update()
    {
        float xPos = transform.position.x * hitWidth;
        float yPos = transform.position.y * hitHeight;
	    if (Input.mousePosition.x > xPos && Input.mousePosition.x < xPos + hitWidth &&
            Input.mousePosition.y > yPos && Input.mousePosition.y < yPos + hitHeight &&
            Input.GetMouseButton(0)) {
                OnClick();
        }
	}

    public virtual void OnClick()
    {
        GameObject.Destroy(this.gameObject);
    }
}
