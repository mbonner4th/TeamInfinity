using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : Base
{
    public GameObject enemyToSpawn;
    public GameObject epicEnemyToSpawn;
    public float baseSpawnTime = 100.0f;
    public float spawnVariation;
    public float spawnTime;
    public float chanceOfEpic;
    public float timeSinceLastSpawn;
    public int numSpawnedEnemies;
    public int maxSpawnedEnemies;

    // Use this for initialization
    void BaseStart()
    {
        print(level.spawnRateMod);
        if (level.spawnRateMod != 0.0f) {
            Spawn();
            maxSpawnedEnemies = Mathf.CeilToInt(level.spawnRateMod * maxSpawnedEnemies);
        }
        timeSinceLastSpawn = 0.0f;
    }

    void UpdateSpawnedEnemies()
    {
        numSpawnedEnemies = transform.childCount;
    }

    public void Spawn()
    {
        if (level.GetCharacter(level.GetTileByPosition(transform.position)) != null) {
            return;
        }

        spawnTime = baseSpawnTime + Random.Range(-spawnVariation, spawnVariation);
        GameObject newEnemy;// = (GameObject) GameObject.Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        if (epicEnemyToSpawn != null)
        {
            float spawnChance = Random.Range(0, 100.0f);
            if (spawnChance > (100 - chanceOfEpic))
            {
                newEnemy = (GameObject)GameObject.Instantiate(epicEnemyToSpawn, transform.position, Quaternion.identity);
            }
            else
            {
                newEnemy = (GameObject)GameObject.Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }
            
        }
        else
        {
            newEnemy = (GameObject)GameObject.Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
       // level.addToEnemies(newEnemy);
        newEnemy.transform.parent = transform;
    }

    // Update is called once per frame
    public override void BaseUpdate(float dt)
    {
        UpdateSpawnedEnemies();
        timeSinceLastSpawn += dt * level.spawnRateMod;

        if (numSpawnedEnemies == maxSpawnedEnemies)
        {
            timeSinceLastSpawn = 0;
        }

        if (timeSinceLastSpawn >= spawnTime)
        {
            timeSinceLastSpawn -= spawnTime;
            Spawn();
        }
    }

    public virtual void OnDestroy()
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; ++i) {
            children.Add(transform.GetChild(i).gameObject);
        }

        transform.DetachChildren();
        for (int i = 0; i < children.Count; ++i) {
            level.cleanUp.Add(children[i]);
            children[i].GetComponent<Enemy>().enabled = true;
            children[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
