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

    public ExpandingChildren shownExpanding;
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
                tilePickers[i, j].GetComponent<SpriteRenderer>().sprite = level.tileType[tileID].GetComponent<SpriteRenderer>().sprite;
                if (level.spawnObject[tileID] != null) {
                    SpriteRenderer renderer = level.spawnObject[tileID].GetComponent<SpriteRenderer>();
                    if (!renderer) {
                        renderer = level.spawnObject[tileID].GetComponentInChildren<SpriteRenderer>();
                    }

                    if (renderer) {
                        tilePickers[i, j].GetComponent<ExpandingChildren>().shownPickup.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
                    }
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
                output.Write(" ");
            }
            output.Write("\n");
        }
        output.Close();
    }

    public void SetExpanding(ExpandingChildren shown)
    {
        if (shownExpanding != null) {
            shownExpanding.Toggle();
        }

        shownExpanding = shown;

        if (shownExpanding != null) {
            shownExpanding.Toggle();
        }
    }

    public void ChildClicked(ExpandingChild child)
    {
        shownExpanding.GetComponent<SpriteRenderer>().sprite = child.GetComponent<SpriteRenderer>().sprite;
        if (level.spawnObject[child.tileID] != null) {
            SpriteRenderer renderer = level.spawnObject[child.tileID].GetComponent<SpriteRenderer>();
            if (!renderer) {
                renderer = level.spawnObject[child.tileID].GetComponentInChildren<SpriteRenderer>();
            }

            if (renderer) {
                shownExpanding.shownPickup.GetComponent<SpriteRenderer>().sprite = renderer.sprite;
            }
        }
        tileIDs[Mathf.RoundToInt(shownExpanding.transform.position.x - xOffset), Mathf.RoundToInt(shownExpanding.transform.position.y - yOffset)] = child.tileID;

        SetExpanding(null);
    }
}
