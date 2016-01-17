using UnityEngine;
using System.Collections;

public class CreatTilePickers : TileEditorBase
{
    public GameObject tilePicker;
    public int bounds;

	// Use this for initialization
	void Start () {
        base.Start();
        for (int i = 0; i < 5; ++i) {
            for (int j = 0; j < 5; ++j) {
                GameObject.Instantiate(tilePicker, new Vector3(manager.xOffset + i, manager.yOffset + j, 0), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}
}
