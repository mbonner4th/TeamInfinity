using UnityEngine;
using System.Collections;

public class ExpandingChildren : Clickable
{
    public bool on = false;
    public GameObject shownObject;
    public GameObject shownPickup;
    public int xIndex;
    public int yIndex;

    public void Start()
    {
        base.Start();
        GameObject newObject = (GameObject)GameObject.Instantiate(manager.pastableObject, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        shownPickup = newObject;

        int width = Mathf.CeilToInt(Mathf.Sqrt(level.tileType.Length));
        int height = Mathf.FloorToInt(Mathf.Sqrt(level.tileType.Length));
        for (int i = 0; i < level.tileType.Length; ++i) {
            GameObject newChild = (GameObject)GameObject.Instantiate(level.tileType[i], new Vector3(transform.position.x + i % width, transform.position.y + 1 + i / width, transform.position.z - 1), Quaternion.identity);
            newChild.transform.parent = transform;
            ExpandingChild newChildScript = newChild.AddComponent<ExpandingChild>();
            newChildScript.tileID = i;
            newChild.SetActive(false);
            

            if (level.spawnObject[i] != null) {
                SpriteRenderer renderer = level.spawnObject[i].GetComponent<SpriteRenderer>();
                if (!renderer) {
                    renderer = level.spawnObject[i].GetComponentInChildren<SpriteRenderer>();
                }

                if (!renderer) {
                    continue;
                }

                newObject = (GameObject)GameObject.Instantiate(manager.pastableObject, new Vector3(transform.position.x + i % width, transform.position.y + 1 + i / width, transform.position.z - 2), Quaternion.identity);
                newObject.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
                newObject.transform.parent = transform;
                newObject.SetActive(false);
            }
        }
    }

    public void Toggle()
    {
        on = !on;
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(on);
        }
    }

    public override void OnClick()
    {
        if (manager.shownExpanding != null && manager.shownExpanding != this) {
            return;
        }

        if (!on) {
            manager.SetExpanding(this);
        } else {
            manager.SetExpanding(null);
        }
    }
}
