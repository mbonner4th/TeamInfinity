using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class TileManager : TileEditorBase
{
    public float xOffset;
    public float yOffset;
    public GameObject pastableObject;
    public GameObject tilePickerType;
    public GameObject[,] tilePickers;
    public float section;
    public int currentSectionNum;
    public int tileToPaint;

    public GameObject paintSelectorType;
    public Vector3 paintSelectionPos;
    public int[,] tileIDs;

    public void Start()
    {
        base.Start();
        currentSectionNum = level.numSectionTypes;
        tileIDs = new int[level.sectionSize, level.sectionSize];
        for (int i = 0; i < level.sectionSize; ++i) {
            for (int j = 0; j < level.sectionSize; ++j) {
                tileIDs[i, j] = 0;
            }
        }

        tilePickers = new GameObject[level.sectionSize, level.sectionSize];
        for (int i = 0; i < level.sectionSize; ++i) {
            for (int j = 0; j < level.sectionSize; ++j) {
                tilePickers[i, j] = (GameObject) GameObject.Instantiate(tilePickerType, new Vector3(xOffset + i, yOffset + j, 0), Quaternion.identity);
            }
        }

        int width = Mathf.CeilToInt(Mathf.Sqrt(level.tileType.Length));
        int height = Mathf.FloorToInt(Mathf.Sqrt(level.tileType.Length));
        for (int i = 0; i < level.tileType.Length; ++i) {
            GameObject newChild = (GameObject)GameObject.Instantiate(pastableObject, new Vector3(paintSelectionPos.x + i % width, paintSelectionPos.y + 1 + i / width, paintSelectionPos.z - 1), Quaternion.identity);
            newChild.transform.parent = transform;
            PaintSelector newChildScript = newChild.AddComponent<PaintSelector>();
            newChildScript.tileID = i;
            newChild.GetComponent<SpriteRenderer>().sprite = level.tileType[i].GetComponent<SpriteRenderer>().sprite;


            if (level.spawnObject[i] != null) {
                SpriteRenderer renderer = level.spawnObject[i].GetComponent<SpriteRenderer>();
                Sprite sprite = null;
                if (!renderer) {
                    continue;
                } else {
                    sprite = renderer.sprite;
                }

                if (!sprite) {
                    continue;
                }

                GameObject newObject = (GameObject)GameObject.Instantiate(pastableObject, new Vector3(paintSelectionPos.x + i % width, paintSelectionPos.y + 1 + i / width, paintSelectionPos.z - 2), Quaternion.identity);
                newObject.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
                newChildScript.spawnPickup = newObject;
            }
        }
    }

    public void SetSectionNum()
    {
        string sectionNumStr = GameObject.Find("SectionNumInput").GetComponent<InputField>().text;
        if (sectionNumStr == " ") {
            currentSectionNum = 0;
        } else {
            currentSectionNum = int.Parse(sectionNumStr);
        }
    }

    public void SetTileToPaint()
    {
        string inputStr = GameObject.Find("TileTypeInput").GetComponent<InputField>().text;
        if (inputStr == " ") {
            tileToPaint = 0;
        } else {
            tileToPaint = int.Parse(inputStr);
        }
    }

    public void Save()
    {
        WriteSection(Application.dataPath + "/Levels/Section", currentSectionNum);
        if (currentSectionNum == level.numSectionTypes) {
            ++currentSectionNum;
            ++level.numSectionTypes;
        }
    }

    public void Open()
    {
        for (int i = 0; i < level.sectionSize; ++i) {
            for (int j = 0; j < level.sectionSize; ++j) {
                int tileID = level.section[currentSectionNum, i, j];
                tileIDs[i, j] = tileID;
                tilePickers[i, j].GetComponent<SpriteRenderer>().sprite = level.tileType[tileID].GetComponent<SpriteRenderer>().sprite;
                if (level.spawnObject[tileID] != null) {
                    SpriteRenderer renderer = level.spawnObject[tileID].GetComponent<SpriteRenderer>();
                    if (renderer == null) {
                        renderer = level.spawnObject[tileID].GetComponentInChildren<SpriteRenderer>();
                    }

                    if (renderer != null) {
                        tilePickers[i, j].GetComponent<Paintable>().shownPickup.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
                        tilePickers[i, j].GetComponent<Paintable>().shownPickup.SetActive(true);
                    }
                } else {
                    tilePickers[i, j].GetComponent<Paintable>().shownPickup.SetActive(false);
                }
            }
        }
    }

    public void WriteSection(string fileNameStem, int sectionNum)
    {
        StreamWriter output = new StreamWriter(fileNameStem + sectionNum + ".txt");
        for (int i = 0; i < level.sectionSize; ++i) {
            for (int j = 0; j < level.sectionSize; ++j) {
                output.Write(tileIDs[j, (level.sectionSize - 1) - i]);
                if (j != level.sectionSize - 1)
                {
                    output.Write(" ");
                }
            }
            output.Write("\n");
        }
        output.Close();
    }

    public void PaintTile(Paintable paintable)
    {
        paintable.GetComponent<SpriteRenderer>().sprite = level.tileType[tileToPaint].GetComponent<SpriteRenderer>().sprite;
        if (level.spawnObject[tileToPaint] != null) {
            SpriteRenderer renderer = level.spawnObject[tileToPaint].GetComponent<SpriteRenderer>();
            if (!renderer) {
                renderer = level.spawnObject[tileToPaint].GetComponentInChildren<SpriteRenderer>();
            }

            if (renderer) {
                paintable.GetComponent<Paintable>().shownPickup.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
                paintable.GetComponent<Paintable>().shownPickup.SetActive(true);
            }
        } else {
            paintable.GetComponent<Paintable>().shownPickup.SetActive(false);
        }
        tileIDs[Mathf.RoundToInt(paintable.transform.position.x - xOffset), Mathf.RoundToInt(paintable.transform.position.y - yOffset)] = tileToPaint;
    }
}
