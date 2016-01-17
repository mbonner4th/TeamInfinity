using UnityEngine;
using System.Collections;

public class ExpandingChildren : Clickable
{
    public bool on = false;

    public override void OnClick()
    {
        for (int i = 0; i < transform.childCount; ++i) {
            on = !on;
            transform.GetChild(i).gameObject.SetActive(on);
        }
    }
}
