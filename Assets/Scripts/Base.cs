using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
	void Start()
    {
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
