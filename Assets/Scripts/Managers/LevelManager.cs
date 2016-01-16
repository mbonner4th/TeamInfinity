using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int parts;
    public int guilt;

    public int[,] tileIDs;
    public Vector4[] tiles;

	void Update()
    {
	
	}

    public virtual void OnPickPart(int intensity)
    {
        parts += intensity;
    }

    public virtual void OnPickPerson(int intensity)
    {
        guilt -= intensity;
    }
}
