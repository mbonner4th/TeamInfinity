using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
    public LevelManager level;
    public GameObject playerObject;
    public Player player;

	void Start()
    {
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerObject = GameObject.Find("Player");
        player = GameObject.Find("LevelManager").GetComponent<Player>();
        BaseStart();
	}

    public virtual void BaseStart()
    {

    }
	
	void Update()
    {
        float dt = Time.deltaTime;
        BaseUpdate(dt);
	}

    public virtual void BaseUpdate(float dt)
    {

    }
}
