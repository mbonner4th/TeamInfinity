﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelManager : Base
{
    public int req_artifacts;
    public int artifacts;
    public int guilt;
    public string leveltoload;

    public int[,] tileTypes;
    public GameObject[,] tileObjects;
    public Character[,] tileCharacters;
    public GameObject[,] tilePickups;
    public bool[] tiles;
    public GameObject[] spawnObject;
    public GameObject[] tileType;
    public int[] depletedVersion;

    public List<int>[] randomSectionLayout;
    public List<int>[] randomSectionLayoutFrequency;

    public int numSectionTypes;
    public int sectionSize;
    public int[,,] section;
    public int[,] level;
    public int levelWidth;
    public int levelHeight;

    public float tileSpacing;

    public Vector3 startPosition;

    public KeyCode menuKey;
    public bool gamePaused = false;
    public GameObject PauseMenu;
	public GameObject GameOverMenu;
	public GameObject ShopMenu;

    
    public System.Collections.Generic.List<GameObject> enemies;

    public void SetTileDepleted(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return;
        }

        int cntTileType = tileTypes[posX, posY];
        if (depletedVersion[cntTileType] != 0)
        {
            int newVersion = depletedVersion[cntTileType];
            SetTile(posX, posY, newVersion);
        }
    }

    public void TryToMoveCharacter(Vector3 distance, Character characterToMove)
    {
        Character other = GetCharacter(GetTileByPosition(characterToMove.transform.position + distance));
        if (other != null)
        {
            // do damage to other
        }
        else if (!IsTileSolid(characterToMove.transform.position + distance))
        {
            SetCharacter(GetTileByPosition(characterToMove.transform.position), null);
            characterToMove.transform.Translate(distance);
            SetCharacter(GetTileByPosition(characterToMove.transform.position), characterToMove);
        }
    }

    public Character GetCharacter(Vector2 tilePosition)
    {
        int posX = Mathf.RoundToInt(tilePosition.x);
        int posY = Mathf.RoundToInt(tilePosition.y);
        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return null;
        }

        return tileCharacters[posX, posY];
    }

    public void SetCharacter(Vector2 tilePosition, Character characterToSet)
    {
        int posX = Mathf.RoundToInt(tilePosition.x);
        int posY = Mathf.RoundToInt(tilePosition.y);
        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return;
        }

        tileCharacters[posX, posY] = characterToSet;
    }


    public Vector2 GetTileByPosition(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return new Vector2(-1.0f, -1.0f);
        }
        return new Vector2(posX * 1.0f, posY * 1.0f);
    }

    public bool IsTileSolid(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1)) {
            return true;
        }
        return tiles[tileTypes[posX, posY]];
    }

    public bool IsTileWater(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1)) {
            return false;
        }
        
        return tileTypes[posX, posY] == 2;
    }

    public bool IsTileCamp(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return false;
        }

        return tileTypes[posX, posY] == 10;
    }

    void Awake()
    {
        numSectionTypes = 71;
        sectionSize = 5;
        section = new int[numSectionTypes, sectionSize, sectionSize];
        LoadSections(Application.dataPath + "/Levels/Section");
        LoadRandomSectionLayout(Application.dataPath + "/Levels/RandomSectionTypes.txt");
        if (leveltoload != "") {
            LoadLevel(Application.dataPath + "/Levels/Level" + leveltoload + ".txt");
            GenerateLevel();
        }
        InvokeRepeating("tickEnimies", 1.0f, 1.0f);
    }




	public override void BaseUpdate(float dt)
    {
		if (PauseMenu != null && Input.GetKeyUp (menuKey) && (!GameOverMenu.activeSelf) && (!ShopMenu.activeSelf))
		{
			UpdateMenu();
		}
        //tickEnimies();
	}

	public void UpdateMenu()
	{
		if(PauseMenu.activeSelf)
		{
			PauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
			gamePaused = false;
		}
		else
		{
			PauseMenu.SetActive(true);
			Time.timeScale = 0;
			gamePaused = true;
		}
	}

    public void EndLevel()
    {
        WriteText("Congratulations!");
        LoadLevel(Application.dataPath + "/Levels/Level" + 2 + ".txt");
        GenerateLevel();

		/*ShopMenu.SetActive (true);
		Time.timeScale = 0;
		gamePaused = true;*/
	}
	
	
	public void GameOver(Player lastPlayer)
	{
		if (!GameOverMenu.activeSelf) {
			GameOverMenu.SetActive (true);
			//Time.timeScale = 0;
			gamePaused = true;
			GameOverMenu.GetComponent<GameOverManager>().GameOver (lastPlayer, guilt, artifacts);
		}
	}

	public void ExitToMainMenu()
	{
		//print("ExitToMainMenu");
		Time.timeScale = 1.0f;
		gamePaused = false;
		Application.LoadLevel ("MainMenu");
	}
    
    void LoadSections(string fileNameBase)
    {
        for (int i = 0; i < numSectionTypes; ++i) {
            LoadSection(fileNameBase + i + ".txt", i);
        }
    }

    void LoadSection(string sectionFileName, int sectionNum)
    {
        StreamReader input = new StreamReader(sectionFileName);
        for (uint i = 0; i < sectionSize; ++i) {
            for (uint j = 0; j < sectionSize; ++j) {
                section[sectionNum, j, sectionSize - i - 1] = ReadNextNumber(input);
            }
        }
    }

    void LoadRandomSectionLayout(string fileName)
    {
        StreamReader input = new StreamReader(fileName);
        int numberRandomSectionTypes = ReadNextNumber(input) + 1;
        randomSectionLayout = new List<int>[numberRandomSectionTypes];
        randomSectionLayoutFrequency = new List<int>[numberRandomSectionTypes];
        int next = ReadNextNumber(input);
        for (int i = 1; i < numberRandomSectionTypes; ++i) {
            int sectionID = -next;
            print(i);
            print(sectionID);
            randomSectionLayout[sectionID] = new List<int>();
            randomSectionLayoutFrequency[sectionID] = new List<int>();
            next = ReadNextNumber(input);
            while (next > 0) {
                randomSectionLayout[sectionID].Add(next);
                next = ReadNextNumber(input);
                randomSectionLayoutFrequency[sectionID].Add(next);
                next = ReadNextNumber(input);
            }
        }
    }

    void LoadLevel(string fileName)
    {
        StreamReader input = new StreamReader(fileName);
        levelWidth = ReadNextNumber(input);
        levelHeight = ReadNextNumber(input);
        level = new int[levelWidth, levelHeight];
        for (uint i = 0; i < levelHeight; ++i) {
            for (uint j = 0; j < levelWidth; ++j) {
                int nextSectionNum = ReadNextNumber(input);
                if (nextSectionNum < 0) {
                    nextSectionNum = -nextSectionNum;
                    nextSectionNum = SelectRandomSection(nextSectionNum);
                }
                level[j, (levelHeight - 1) - i] = nextSectionNum;
                /*
                if (level[i, j] == 0) {
                    int[] possibleSections = new int[numSectionTypes];
                    for (int k = 0; k < numSectionTypes; ++k) {
                        possibleSections[k] = k;
                    }

                    if (i > 0) {
                        bool[] match = new bool[sectionSize];
                        for (int k = 0; k < sectionSize; ++k) {
                            match[k] = tiles[section[level[i - 1, j], sectionSize - 1, k]];
                        }

                        for (int k = 0; k < possibleSections.Length; ++k) {
                            for (int a = 0; a < sectionSize; ++a) {
                                if (!match[a] && tiles[section[possibleSections[k], 0, a]]) {
                                    possibleSections[k] = -1;
                                    break;
                                }
                            }
                        }
                    }

                    if (j > 0) {
                        bool[] match = new bool[sectionSize];
                        for (int k = 0; k < sectionSize; ++k) {
                            match[k] = tiles[section[level[i, j - 1], k, sectionSize - 1]];
                        }

                        for (int k = 0; k < possibleSections.Length; ++k) {
                            for (int a = 0; a < sectionSize; ++a) {
                                if (possibleSections[k] != -1 && !match[a] && tiles[section[possibleSections[k], a, 0]]) {
                                    possibleSections[k] = -1;
                                    break;
                                }
                            }
                        }
                    }
                    int[] possible = new int[sectionSize];
                    int actuallyPossible = 0;
                    for (uint k = 0; k < possibleSections.Length; ++k) {
                        if (possibleSections[k] != -1) {
                            possible[actuallyPossible] = possibleSections[k];
                            ++actuallyPossible;
                        }
                    }
                    level[j, levelWidth - 1 - i] = possible[Random.Range(0, actuallyPossible + 1)];
                }
                */
            }
        }

        if (tileObjects != null) {
            for (int i = 0; i < tileObjects.GetLength(0); ++i) {
                for (int j = 0; j < tileObjects.GetLength(1); ++j) {
                    if (tileObjects[i, j] != null) {
                        GameObject.Destroy(tileObjects[i, j]);
                    }

                    if (tilePickups[i, j] != null) {
                        GameObject.Destroy(tilePickups[i, j]);
                    }
                }
            }
        }
        
        tileObjects = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
        tilePickups = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
        tileTypes = new int[levelWidth * sectionSize, levelHeight * sectionSize];
    }

    public int SelectRandomSection(int sectionType)
    {
        int totalFrequency = 0;
        for (int i = 0; i < randomSectionLayoutFrequency[sectionType].Count; ++i) {
            totalFrequency += randomSectionLayoutFrequency[sectionType][i];
        }

        int sectionFreq = Random.Range(0, totalFrequency);
        int currentFrequency = 0;
        for (int i = 0; i < randomSectionLayoutFrequency[sectionType].Count; ++i) {
            currentFrequency += randomSectionLayoutFrequency[sectionType][i];
            if (currentFrequency > sectionFreq) {
                return randomSectionLayout[sectionType][i];
            }
        }

        return randomSectionLayout[sectionType][0];
    }

    int ReadNextNumber(StreamReader input)
    {
        string result = "";
        while (input.Peek() >= 0) {
            char c = (char)input.Read();
            if (c == '\r') {
                input.Read();
                break;
            } else if (c == '\n' || c == ' ' || c == '\t') {
                break;
            }

            result += c;
        }

        if (result == "") {
            return -9999;
        } else {
            return int.Parse(result);
        }
    }

    // ============================================= Level Generation =============================================//
    void GenerateLevel()
    {
        for (int i = 0; i < levelWidth; ++i) {
            for (int j = 0; j < levelHeight; ++j) {
                GenerateSection(i * sectionSize, j * sectionSize, level[i, j]);
            }
        }
    }
    
    void GenerateSection(int tilePositionX, int tilePositionY, int sectionNum)
    {
        for (int i = 0; i < sectionSize; ++i) {
            for (int j = 0; j < sectionSize; ++j) {
                int posX = tilePositionX + i;
                int posY = tilePositionY + j;
                print(posX);
                print(posY);
                SetTile(posX, posY, section[sectionNum, i, j]);
            }
        }
    }

    void SetTile(int posX, int posY, int tileID)
    {
        if (tileObjects[posX, posY] != null) {
            GameObject.Destroy(tileObjects[posX, posY]);
        }

        if (tilePickups[posX, posY] != null) {
            GameObject.Destroy(tilePickups[posX, posY]);
        }

        tileObjects[posX, posY] = (GameObject)GameObject.Instantiate(tileType[tileID], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z), Quaternion.identity);
        tileTypes[posX, posY] = tileID;

        if (spawnObject[tileID] != null) {
            GameObject newObject = null;
            if (spawnObject[tileID].name == "Player") {
                //if (!GameObject.Find("Player")) {
                    newObject = (GameObject)GameObject.Instantiate(spawnObject[tileID], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z - 1), Quaternion.identity);
                    newObject.name = "Player";
                //}
            } else {
                newObject = (GameObject)GameObject.Instantiate(spawnObject[tileID], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z - 1), Quaternion.identity);
            }

            tilePickups[posX, posY] = newObject;
        }
    }

    /*
    void GenerateTiling()
    {
        tiles = new GameObject[(int)bounds.x, (int)bounds.y];
        bool[,] specials = new bool[(int)bounds.x, (int)bounds.y];

        for (uint i = 0; i < bounds.x; ++i) {
            for (uint j = 0; j < bounds.y; ++j) {
                specials[i, j] = false;
            }
        }

        for (uint i = 0; i < specialTiles.Length; ++i) {
            tiles[(int)specialTiles[i].x, (int)specialTiles[i].y] =
                (GameObject)GameObject.Instantiate(tileType[(int)specialTiles[i].z],
                new Vector3(startPosition.x + specialTiles[i].x * tileSpacing, startPosition.y + specialTiles[i].y * tileSpacing, 0), Quaternion.identity);
            specials[(int)specialTiles[i].x, (int)specialTiles[i].y] = true;
        }

        for (uint i = 0; i < bounds.x; ++i) {
            for (uint j = 0; j < bounds.y; ++j) {
                if (!specials[i, j]) {
                    tiles[i, j] = (GameObject)GameObject.Instantiate(tileType[0], new Vector3(startPosition.x + i * tileSpacing, startPosition.y + j * tileSpacing, 0), Quaternion.identity);
                }
            }
        }
    }
    */

    public virtual void OnPickPart(int intensity)
    {
        artifacts += intensity;
    }

    public virtual void OnPickPerson(int intensity)
    {
        guilt -= intensity;
    }

    public bool addToEnemies(GameObject enemy)
    {
        //Debug.Log("added to enemies");
        if(GetCharacter(GetTileByPosition(enemy.transform.position)) != null){
            enemies.Add(enemy);
            return true;
        }
        else{
            return false;
        }
        
    }

    public void tickEnimies()
    {
//Debug.Log("ticked");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().tickEnemy();
            }
            
        }
    }

}
