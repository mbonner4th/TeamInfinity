using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelManager : Base
{
    public int req_artifacts;
    public int artifacts;
    public int guilt;
    public int leveltoload;

    public int[,] tileTypes;
    public GameObject[,] tileObjects;
    public bool[] tiles;
    public GameObject[] spawnObject;
    public GameObject[] tileType;
    public int[] depletedVersion;

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
        int depletedTile = 1;
        if (depletedVersion[cntTileType] != null)
        {
            depletedTile = depletedVersion[cntTileType];            
        }

        tileObjects[posX, posY].GetComponent<SpriteRenderer>().sprite = tileType[depletedTile].GetComponent<SpriteRenderer>().sprite;
        tileTypes[posX, posY] = depletedTile;
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
        if (leveltoload >= 0) {
            LoadLevel(Application.dataPath + "/Levels/Level", leveltoload);
            GenerateLevel();
        }
        InvokeRepeating("tickEnimies", 1.0f, 1.0f);
    }

    public override void BaseUpdate(float dt)
    {
		if (PauseMenu != null && Input.GetKeyUp (menuKey)) 
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
	}
	
	
	public void GameOver(Player lastPlayer)
	{
		if (!GameOverMenu.activeSelf) {
			GameOverMenu.SetActive (true);
			Time.timeScale = 0;
			gamePaused = true;
			
			if (lastPlayer.water == 0) {
				WriteText ("You didn't have enough water.");
			}
			if (lastPlayer.health == 0) {
				WriteText ("You didn't have enough food.");
			}
			if (lastPlayer.ammo == 0) {
				WriteText ("You didn't have enough rocks.");
			}
			if (guilt >= 10) {
				WriteText ("You didn't have enough love.");
			}
			if (artifacts == 0) {
				WriteText ("You didn't have enough artifacts.");
			}
		}
	}

	public void ExitToMainMenu()
	{
        print("ExitToMainMenu");
		Application.LoadLevel ("MainMenu");
		Time.timeScale = 1.0f;
		gamePaused = false;
	}
    
    void LoadSections(string fileNameBase)
    {
        for (int i = 0; i < numSectionTypes; ++i) {
            LoadSection(fileNameBase + i + ".txt", i);
        }
    }

    void LoadSection(string sectionFileName, int sectionNum)
    {
        print(sectionNum);
        StreamReader input = new StreamReader(sectionFileName);
        for (uint i = 0; i < sectionSize; ++i) {
            for (uint j = 0; j < sectionSize; ++j) {
                section[sectionNum, j, sectionSize - i - 1] = ReadNextNumber(input);
            }
        }
    }

    void LoadLevel(string baseFileName, int levelNum)
    {
        print(baseFileName + levelNum + ".txt");
        StreamReader input = new StreamReader(baseFileName + levelNum + ".txt");
        levelWidth = ReadNextNumber(input);
        levelHeight = ReadNextNumber(input);
        level = new int[levelWidth, levelHeight];
        for (uint i = 0; i < levelHeight; ++i) {
            for (uint j = 0; j < levelWidth; ++j) {
                level[j, (levelHeight - 1) - i] = ReadNextNumber(input);
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

        print(levelWidth);
        print(sectionSize);
        print(levelHeight);
        tileObjects = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
        tileTypes = new int[levelWidth * sectionSize, levelHeight * sectionSize];
    }

    int ReadNextNumber(StreamReader input)
    {
        string result = "";
        while (input.Peek() >= 0) {
            char c = (char)input.Read();
            if (c == '\r') {
                input.Read();
                break;
            } else if (c == '\n' || c == ' ') {
                break;
            }

            result += c;
        }

        return int.Parse(result);
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
                tileObjects[posX, posY] = (GameObject)GameObject.Instantiate(tileType[section[sectionNum, i, j]], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z), Quaternion.identity);
                tileTypes[posX, posY] = section[sectionNum, i, j];
                
                if (spawnObject[section[sectionNum, i, j]] != null) {
                    print(spawnObject[section[sectionNum, i, j]]);
                    GameObject newObject = (GameObject) GameObject.Instantiate(spawnObject[section[sectionNum, i, j]], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z - 1), Quaternion.identity);
                    if (newObject.name == "Player(Clone)") {
                        if (!GameObject.Find("Player")) {
                            newObject.name = "Player";
                        }
                    }
                }
            }
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

    public void addToEnemies(GameObject enemy)
    {
        //Debug.Log("added to enemies");
        enemies.Add(enemy);
    }

    public void tickEnimies()
    {
        Debug.Log("ticked");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.GetComponent<EnemyBase>() != null)
            {
                enemy.GetComponent<EnemyBase>().tickEnemy();
            }
            
        }
    }

}
