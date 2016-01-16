using UnityEngine;
using System.Collections;

public class SpawnTiles : MonoBehaviour
{
    public GameObject[] tileType;

    public int[,,] section;
    public Vector4[] edgeMatch;
    public float tileSpacing;

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

    void Update()
    {

    }
}
