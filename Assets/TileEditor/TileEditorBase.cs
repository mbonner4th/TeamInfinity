using UnityEngine;
using System.Collections;

public class TileEditorBase : MonoBehaviour
{
    public TileManager manager;

	// Use this for initialization
	public virtual void Start()
    {
        manager = GameObject.Find("TileManager").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
    public virtual void Update()
    {
	
	}
}
