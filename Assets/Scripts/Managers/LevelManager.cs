using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int parts;
    public int guilt;
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
