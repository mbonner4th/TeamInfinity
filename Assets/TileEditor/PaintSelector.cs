using UnityEngine;
using System.Collections;

public class PaintSelector : Clickable
{
    public int tileID;
    public GameObject spawnPickup;

    public override void OnClick()
    {
        manager.tileToPaint = tileID;
    }
}
