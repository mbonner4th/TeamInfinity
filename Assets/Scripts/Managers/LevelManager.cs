using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class LevelManager : Base
{
    public string folderName = "Levels/BenLevels/";

    public int req_artifacts;
    public int artifacts;
    public int guilt;
    public float time;
    public float maxTime = 60;
    public float flashlightLvl;
    public float maxFlashlightLvl = 30;
    public bool flashlightMessage;
    public bool nightMessage;
    public int cntVisionRadius;
    public int baseVisionRadius = 3;
    //public int startingTime = 1800; // 30 seconds
    public int leveltoload;

    public int[,] tileTypes;
    public GameObject[,] tileObjects;
    public Character[,] tileCharacters;
    public GameObject[,] tilePickups;
    public GameObject[,] fogObjects;
    public GameObject dayFogObjectType;
    public GameObject nightFogObjectType;
    public bool[] tiles;
    public GameObject[] spawnObject;
    public GameObject[] tileType;
    public int[] depletedVersion;

    public List<int>[] randomLevelLayout;
    public List<int>[] randomSectionLayout;
    public List<int>[] randomSectionLayoutFrequency;

    public int numSectionTypes;
    public int sectionSize;
    public int[, ,] section;
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
    //public GameObject MainUI;
    public GameObject UITextBox;
    public GameObject UITextPanel;
    public GameObject UIHealthSlider;
    public GameObject UIWaterSlider;

    public int persHealth;
    public int persWater;
    public int persAmmo;
    public int persMoney;
    public int persDamage;
    public int persMaxHealth;
    public int persMaxWater;

    public System.Collections.Generic.List<GameObject> enemies;
    public System.Collections.Generic.List<GameObject> cleanUp;
    public Character[] characterList;
    public int helplessPeople = 0;

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
        if (gamePaused)
        {
            return;
        }

        Character other = GetCharacter(GetTileByPosition(characterToMove.transform.position + distance));
        if (other != null)
        {
            print("in the way");
            int hit = characterToMove.damage;
            if (hit >= 50)
            {
                hit = hit / 2;
            }
            other.health -= hit;
            characterToMove.health -= other.damage;
            // do damage to other
            //hit
        }
        else if (!IsTileSolid(characterToMove.transform.position + distance))
        {
            //print("can move");
            SetCharacter(GetTileByPosition(characterToMove.transform.position), null);
            characterToMove.transform.Translate(distance);
            SetCharacter(GetTileByPosition(characterToMove.transform.position), characterToMove);
        }
        else
        {
            print("can't move");
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
        Character temp = tileCharacters[posX, posY];
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
        //print(tileCharacters);

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

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
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

        if (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1))
        {
            return false;
        }

        return (tileTypes[posX, posY] == 2 || tileTypes[posX, posY] == 12);
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

    public bool IsTileOutOfBounds(Vector3 position)
    {
        Vector3 tilePos = position - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        return (posX < 0 || posY < 0 || posX >= tileTypes.GetLength(0) || posY >= tileTypes.GetLength(1));
    }

    public void ActiveAllFog()
    {
        //print("Activating all fog");
        //for (int i = 0; i < levelWidth * sectionSize; ++i)
        //{
        //    for (int j = 0; j < levelHeight * sectionSize; ++j)
        //    {
        //        fogObjects[i, j].SetActive(true);
        //    }
        //}
    }

    public void ToggleFog(Vector3 pos, int radius)
    {
        ActiveAllFog();
        Vector3 tilePos = pos - startPosition;
        tilePos /= tileSpacing;
        int posX = Mathf.RoundToInt(tilePos.x);
        int posY = Mathf.RoundToInt(tilePos.y);

        fogObjects[posX, posY].SetActive(false);

        for (int i = 0; i < radius; ++i)
        {
            for (int j = 0; j < radius; ++j)
            {
                Vector3 currentTile = new Vector3(posX + i + 1, posY + j + 1);
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX + i + 1, posY + j + 1].SetActive(false); }

                currentTile = new Vector3(posX + i + 1, posY);
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX + i + 1, posY].SetActive(false); }

                currentTile = new Vector3(posX + i + 1, posY - (j + 1));
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX + i + 1, posY - (j + 1)].SetActive(false); }

                currentTile = new Vector3(posX, posY - (j + 1));
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX, posY - (j + 1)].SetActive(false); }

                currentTile = new Vector3(posX, posY + j + 1);
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX, posY + j + 1].SetActive(false); }

                currentTile = new Vector3(posX - (i + 1), posY + j + 1);
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX - (i + 1), posY + j + 1].SetActive(false); }

                currentTile = new Vector3(posX - (i + 1), posY);
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX - (i + 1), posY].SetActive(false); }

                currentTile = new Vector3(posX - (i + 1), posY - (j + 1));
                if (!IsTileOutOfBounds(currentTile)) { fogObjects[posX - (i + 1), posY - (j + 1)].SetActive(false); }
            }
        }
    }

    public void MovePlayer(Vector3 distance)
    {
        if (gamePaused)
        {
            return;
        }

        Vector3 tileHit = player.transform.position + distance;
        TryToMoveCharacter(distance, player);

        if (IsTileSolid(tileHit))
        {
            if (IsTileWater(tileHit))
            {
                playerObject.GetComponentInChildren<Animator>().SetBool("movingUp", distance.y > 0);
                playerObject.GetComponentInChildren<Animator>().SetBool("movingDown", distance.y < 0);
                playerObject.GetComponentInChildren<Animator>().SetBool("movingLeft", distance.x < 0);
                playerObject.GetComponentInChildren<Animator>().SetBool("movingRight", distance.x > 0);
                player.water += 200;
                WriteText("You take a hearty gulp, hoping that it's not poisoned.");
            }
            else if (IsTileCamp(tileHit) && artifacts >= req_artifacts)
            {
                EndLevel();
                return;
            }
            else if (IsTileOutOfBounds(tileHit))
            {
                return;
            }
            SetTileDepleted(tileHit);
            return;
        }

        ToggleFog(player.transform.position, cntVisionRadius);
    }

    void Awake()
    {
        numSectionTypes = 210;
        sectionSize = 5;
        section = new int[numSectionTypes, sectionSize, sectionSize];
        LoadSections(folderName + "Section");
        LoadRandomSectionLayout(folderName + "RandomSectionTypes");
        LoadRandomLevelLayout(folderName + "RandomLevelTypes");
        if (leveltoload != 0)
        {
            LoadLevel(leveltoload);
            if (leveltoload < 0)
            {
                LoadLevel(leveltoload);
                GenerateLevel(true);
            }
            else
            {
                LoadLevel(leveltoload);
                GenerateLevel(false);
            }



        }
        InvokeRepeating("tickEnimies", 1.0f, 1.0f);
        characterList = GameObject.FindObjectsOfType(typeof(Character)) as Character[];
        //print(characterList.Length);
    }




    public override void BaseUpdate(float dt)
    {
        if (playerObject)
        {
            playerObject.GetComponentInChildren<Animator>().SetBool("movingUp", false);
            playerObject.GetComponentInChildren<Animator>().SetBool("movingDown", false);
            playerObject.GetComponentInChildren<Animator>().SetBool("movingLeft", false);
            playerObject.GetComponentInChildren<Animator>().SetBool("movingRight", false);
        }

        if (PauseMenu != null && Input.GetKeyUp(menuKey) && (!GameOverMenu.activeSelf) && (!ShopMenu.activeSelf))
        {
            UpdateMenu();
        }
        //print(time);
        if (time > 10 && !gamePaused)
        {
            time -= dt;
            if (time <= 30 && cntVisionRadius > 0)
            {
                GenerateFog(nightFogObjectType);
                if (nightMessage)
                {
                    WriteText("Night has fallen. Spoooooooky...");
                    nightMessage = false;
                }
                cntVisionRadius = 0;
                if (flashlightLvl > 0)
                {
                    flashlightLvl -= dt;
                    if (flashlightMessage)
                    {
                        WriteText("Your flashlight has " + flashlightLvl + " seconds of battery left.");
                        flashlightMessage = false;
                    }
                    if (flashlightLvl >= 30)
                    {
                        cntVisionRadius += 3;
                    }
                    else if (flashlightLvl >= 20)
                    {
                        cntVisionRadius += 2;
                    }
                    else
                    {
                        cntVisionRadius += 1;
                    }
                }
                ToggleFog(player.transform.position, cntVisionRadius);
            }
            else if (time <= 40 && cntVisionRadius > Mathf.FloorToInt(baseVisionRadius / 2.0f))
            {
                WriteText("It's getting darker.");
                cntVisionRadius = Mathf.FloorToInt(baseVisionRadius / 2.0f);
                ToggleFog(player.transform.position, cntVisionRadius);
            }
            else if (time <= 50 && cntVisionRadius == baseVisionRadius)
            {
                WriteText("It's getting darker.");
                cntVisionRadius = baseVisionRadius - 1;
                ToggleFog(player.transform.position, cntVisionRadius);
            }
        }
    }

    public void UpdateMenu()
    {
        if (PauseMenu.activeSelf)
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

    public void RestartLevel()
    {
        GameOverMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
        time = maxTime;
        player.health = 100;

        LoadLevel(leveltoload);
        GenerateLevel(false);
    }

    public void NextLevel()
    {
        ShopMenu.SetActive(false);
        ActiveUI(true);
        Time.timeScale = 1.0f;
        gamePaused = false;
        time = maxTime;

        persHealth = player.health;
        persWater = player.water;
        print(persWater);
        persAmmo = player.ammo;
        persMoney = player.money;
        persDamage = player.damage;
        persMaxHealth = player.maxHealth;
        persMaxWater = player.maxWater;

        if (leveltoload > 0)
        {
            ++leveltoload;
        }
        else
        {
            --leveltoload;
        }

        LoadLevel(leveltoload);
        GenerateLevel(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        ReloadPlayerAtts();
    }

    public void EndLevel()
    {
        if (!ShopMenu.activeSelf)
        {
            ShopMenu.SetActive(true);
            ShopMenu.GetComponent<ShopManager>().PrepareShop(player);
            //WriteText("\n\n\n\n");
            ActiveUI(false);
            Time.timeScale = 0;
            gamePaused = true;

            if (helplessPeople > 0)
            {
                WriteText("You feel guilty for some reason...");
                WriteText("...Did you forget to save anyone?");
                guilt += helplessPeople * 10;
            }
            helplessPeople = 0;
        }
    }


    public void GameOver(Player lastPlayer)
    {
        if (!GameOverMenu.activeSelf)
        {
            GameOverMenu.SetActive(true);
            //Time.timeScale = 0;
            gamePaused = true;
            GameOverMenu.GetComponent<GameOverManager>().GameOver(lastPlayer, guilt, artifacts, flashlightLvl, time);
        }
    }

    public void ExitToMainMenu()
    {
        //print("ExitToMainMenu");
        Time.timeScale = 1.0f;
        gamePaused = false;
        Application.LoadLevel("MainMenu");
    }

    public void ReloadPlayerAtts()
    {
        player.health = persHealth;
        player.water = persWater;
        player.ammo = persAmmo;
        player.money = persMoney;
        player.damage = persDamage;
        player.maxHealth = persMaxHealth;
        player.maxWater = persMaxWater;
    }

    void LoadSections(string fileNameBase)
    {
        for (int i = 0; i < numSectionTypes; ++i)
        {
            LoadSection(fileNameBase + i, i);
        }
    }

    void LoadSection(string sectionFileName, int sectionNum)
    {
        TextAsset assetFile = (TextAsset)Resources.Load(folderName + "Section" + sectionNum, typeof(TextAsset));
        StringReader input = new StringReader(assetFile.text);
        for (int i = 0; i < sectionSize; ++i)
        {
            for (int j = 0; j < sectionSize; ++j)
            {
                int posX = j;
                int posY = sectionSize - i - 1;
                section[sectionNum, posX, posY] = ReadNextNumber(input);
            }
        }
    }

    void LoadRandomSectionLayout(string fileName)
    {
        TextAsset inputFile = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
        StringReader input = new StringReader(inputFile.text);
        int numberRandomSectionTypes = ReadNextNumber(input) + 1;
        randomSectionLayout = new List<int>[numberRandomSectionTypes];
        randomSectionLayoutFrequency = new List<int>[numberRandomSectionTypes];
        int next = ReadNextNumber(input);
        for (int i = 1; i < numberRandomSectionTypes; ++i)
        {

            int sectionID = -next;
            randomSectionLayout[sectionID] = new List<int>();
            randomSectionLayoutFrequency[sectionID] = new List<int>();
            next = ReadNextNumber(input);
            while (next > 0)
            {
                randomSectionLayout[sectionID].Add(next);
                next = ReadNextNumber(input);
                randomSectionLayoutFrequency[sectionID].Add(next);
                next = ReadNextNumber(input);
            }
        }
    }

    void LoadRandomLevelLayout(string fileName)
    {
        TextAsset inputFile = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
        StringReader input = new StringReader(inputFile.text);
        int numberRandomLevels = ReadNextNumber(input) + 1;
        randomLevelLayout = new List<int>[numberRandomLevels];
        int next = ReadNextNumber(input);
        for (int i = 1; i < numberRandomLevels; ++i)
        {
            int levelID = -next;
            randomLevelLayout[levelID] = new List<int>();
            next = ReadNextNumber(input);
            while (next > 0)
            {
                randomLevelLayout[levelID].Add(next);
                next = ReadNextNumber(input);
            }
        }
    }

    void LoadLevel(int levelNum)
    {
        int loadingLevel = levelNum;
        if (loadingLevel < 0)
        {
            List<int> levelPool = randomLevelLayout[-loadingLevel];
            loadingLevel = levelPool[Random.Range(0, levelPool.Count)];
        }

        TextAsset inputFile = (TextAsset)Resources.Load(folderName + "Level" + loadingLevel, typeof(TextAsset));
        StringReader input = new StringReader(inputFile.text);
        levelWidth = ReadNextNumber(input);
        levelHeight = ReadNextNumber(input);
        level = new int[levelWidth, levelHeight];
        for (uint i = 0; i < levelHeight; ++i)
        {
            for (uint j = 0; j < levelWidth; ++j)
            {
                int nextSectionNum = ReadNextNumber(input);
                if (nextSectionNum < 0)
                {
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

        if (tileObjects != null)
        {
            for (int i = 0; i < tileObjects.GetLength(0); ++i)
            {
                for (int j = 0; j < tileObjects.GetLength(1); ++j)
                {
                    if (tileObjects[i, j] != null)
                    {
                        GameObject.DestroyImmediate(tileObjects[i, j]);
                    }

                    if (tilePickups[i, j] != null)
                    {
                        GameObject.DestroyImmediate(tilePickups[i, j]);
                    }

                    if (fogObjects[i, j] != null)
                    {
                        GameObject.DestroyImmediate(fogObjects[i, j]);
                    }
                }
            }
        }

        Cleaning();

        tileObjects = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
        tilePickups = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
        tileCharacters = new Character[levelWidth * sectionSize, levelHeight * sectionSize];
        tileTypes = new int[levelWidth * sectionSize, levelHeight * sectionSize];
        fogObjects = new GameObject[levelWidth * sectionSize, levelHeight * sectionSize];
    }

    public int SelectRandomSection(int sectionType)
    {
        int totalFrequency = 0;
        for (int i = 0; i < randomSectionLayoutFrequency[sectionType].Count; ++i)
        {
            totalFrequency += randomSectionLayoutFrequency[sectionType][i];
        }

        int sectionFreq = Random.Range(0, totalFrequency);
        int currentFrequency = 0;
        for (int i = 0; i < randomSectionLayoutFrequency[sectionType].Count; ++i)
        {
            currentFrequency += randomSectionLayoutFrequency[sectionType][i];
            if (currentFrequency > sectionFreq)
            {
                return randomSectionLayout[sectionType][i];
            }
        }

        return randomSectionLayout[sectionType][0];
    }

    int ReadNextNumber(StringReader input)
    {
        string result = "";
        while (input.Peek() >= 0)
        {
            char c = (char)input.Read();
            if (c == '\r')
            {
                input.Read();
                break;
            }
            else if (c == '\n' || c == ' ' || c == '\t')
            {
                break;
            }

            result += c;
        }

        if (result == "")
        {
            return -9999;
        }
        else
        {
            return int.Parse(result);
        }
    }

    // ============================================= Level Generation =============================================//
    void GenerateLevel(bool rotateRandomly)
    {
        artifacts = 0;
        for (int i = 0; i < levelWidth; ++i)
        {
            for (int j = 0; j < levelHeight; ++j)
            {
                GenerateSection(i * sectionSize, j * sectionSize, level[i, j], rotateRandomly);
            }
        }
        GenerateFog(dayFogObjectType);
        time = maxTime;
        flashlightLvl = maxFlashlightLvl;
        flashlightMessage = true;
        nightMessage = true;
    }

    void GenerateFog(GameObject fogObjectType)
    {
        for (int i = 0; i < levelWidth * sectionSize; ++i)
        {
            for (int j = 0; j < levelHeight * sectionSize; ++j)
            {
                if (fogObjects[i, j] != null)
                {
                    GameObject.Destroy(fogObjects[i, j]);
                }
                Vector3 currentTile = new Vector3(i, j);
                fogObjects[i, j] = (GameObject)GameObject.Instantiate(fogObjectType, new Vector3(i, j, -2.5f), Quaternion.identity);
            }
        }
    }

    // Some awesome dude on Stack Overflow wrote this cool rotate function.
    static int[,] RotateMatrix(int[,] matrix, int n)
    {
        int[,] ret = new int[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                ret[i, j] = matrix[n - j - 1, i];
            }
        }

        return ret;
    }

    void GenerateSection(int tilePositionX, int tilePositionY, int sectionNum, bool rotateRandomly)
    {
        int[,] sectionTiles = new int[sectionSize, sectionSize];
        for (int i = 0; i < sectionSize; ++i)
        {
            for (int j = 0; j < sectionSize; ++j)
            {
                sectionTiles[i, j] = section[sectionNum, i, j];
            }
        }

        if (rotateRandomly)
        {
            int numRotations = Random.Range(0, 4);
            for (int i = 0; i < numRotations; ++i)
            {
                sectionTiles = RotateMatrix(sectionTiles, sectionSize);
            }
        }

        for (int i = 0; i < sectionSize; ++i)
        {
            for (int j = 0; j < sectionSize; ++j)
            {
                int posX = tilePositionX + i;
                int posY = tilePositionY + j;
                SetTile(posX, posY, section[sectionNum, i, j]);
            }
        }
    }

    void SetTile(int posX, int posY, int tileID)
    {
        if (tileObjects[posX, posY] != null)
        {
            GameObject.Destroy(tileObjects[posX, posY]);
        }

        if (tilePickups[posX, posY] != null)
        {
            GameObject.Destroy(tilePickups[posX, posY]);
        }

        tileObjects[posX, posY] = (GameObject)GameObject.Instantiate(tileType[tileID], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z), Quaternion.identity);
        tileTypes[posX, posY] = tileID;

        if (spawnObject[tileID] != null)
        {
            GameObject newObject = null;
            if (spawnObject[tileID].name == "Player")
            {
                newObject = (GameObject)GameObject.Instantiate(spawnObject[tileID], new Vector3(startPosition.x + posX * tileSpacing, startPosition.y + posY * tileSpacing, startPosition.z - 1), Quaternion.identity);
                newObject.name = "Player";
            }
            else
            {
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
        artifacts += 1;
    }

    public virtual void OnPickPerson(int intensity)
    {
        guilt -= intensity;
        if (guilt <= 0)
        {
            guilt = 0;
        }
        helplessPeople--;
    }

    public bool addToEnemies(GameObject enemy)
    {
        if (GetCharacter(GetTileByPosition(enemy.transform.position)) == null)
        {
            enemies.Add(enemy);
            //Debug.Log("added to enemies");  
            SetCharacter(GetTileByPosition(enemy.transform.position), enemy.GetComponent<Enemy>());
            return true;
        }
        else
        {
            //print("sorry, you don't get to play");
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

    public void ActiveUI(bool activate)
    {
        UITextBox.SetActive(activate);
        UITextPanel.SetActive(activate);
        UIHealthSlider.SetActive(activate);
        UIWaterSlider.SetActive(activate);
    }

    public void Cleaning()
    {
        foreach (GameObject stuff in cleanUp)
        {
            if (stuff != null)
            {
                GameObject.DestroyImmediate(stuff);
            }

        }
    }

}
