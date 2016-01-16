using UnityEngine;
using System.Collections;

public class SpawnTiles : MonoBehaviour
{
    public GameObject[] tileType;
    public Vector4[] edgeMatch;
    public float tileSize;

    public Vector3 startPosition;
    public Vector2 bounds;

    public Vector3[] specialTiles;

    public GameObject[,] tiles;
    

    void Start()
    {
        tiles = new GameObject[(int) bounds.x, (int) bounds.y];
        bool[,] specials = new bool[(int)bounds.x, (int)bounds.y];

        for (uint i = 0; i < bounds.x; ++i) {
            for (uint j = 0; j < bounds.y; ++j) {
                specials[i, j] = false;
            }
        }

        for (uint i = 0; i < specialTiles.Length; ++i) {
            tiles[(int) specialTiles[i].x, (int)specialTiles[i].y] =
                (GameObject) GameObject.Instantiate(tileType[(int)specialTiles[i].z],
                new Vector3(startPosition.x + specialTiles[i].x * tileSize, startPosition.y + specialTiles[i].y * tileSize, 0), Quaternion.identity);
            specials[(int)specialTiles[i].x, (int)specialTiles[i].y] = true;
        }

        for (uint i = 0; i < bounds.x; ++i) {
            for (uint j = 0; j < bounds.y; ++j) {
                print(i);
                print(j);
                print(specials[i, j]);
                print(new Vector3(startPosition.x + i * tileSize, startPosition.y + j * tileSize, 0));
                if (!specials[i, j]) {
                    tiles[i, j] = (GameObject) GameObject.Instantiate(tileType[0], new Vector3(startPosition.x + i * tileSize, startPosition.y + j * tileSize, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {

    }
}
