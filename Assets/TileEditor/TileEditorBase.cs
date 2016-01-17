using UnityEngine;
using System.Collections;

public class TileEditorBase : MonoBehaviour
{
    public TileManager manager;
    public LevelManager level;

	// Use this for initialization
	public virtual void Start()
    {
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
    public virtual void Update()
    {
	
	}
}
