using UnityEngine;
using System.Collections;

public class EnemyBase : Base {

    public Vector3 xAxis;
    public Vector3 yAxis;

    public uint speed;

    public Vector3 prevPlayerPostion;
    public Vector3 playerDistance;
    public float viewDistance;



	// Use this for initialization
    public override void BaseStart()
    {
        base.BaseStart();
        xAxis = new Vector3(1, 0, 0);
        yAxis = new Vector3(0, 1, 0);

        if (playerObject != null)
        {
            prevPlayerPostion = playerObject.transform.position;
        }
    }

    public override void BaseUpdate(float dt)
    {

    }

    public virtual bool seePlayer()
    {
        return true;
    }

    /*
     * @function tickEnemy
     * 
     * takes nothing. Signals the enemy to do its behavior once every
     * time the function is called 
     */

    public virtual void tickEnemy()
    {

    }

    public virtual void moveEnemy()
    {

    }

    public virtual void hurtPlayer()
    {

    }
	
}
