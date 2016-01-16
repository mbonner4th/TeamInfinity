using UnityEngine;
using System.Collections;

public class SimpleEnemyMovement : Base {

    

    public Vector3 xAxis;
    public Vector3 yAxis;

    public uint speed;

    private Vector3 prevPlayerPostion;
    private float x;
    private float y;
    private Vector3 playerDistance;
    
    
    public override void BaseStart()
    {
        xAxis = new Vector3(1, 0, 0);
        yAxis = new Vector3(0, 1, 0);

        prevPlayerPostion = playerObject.transform.position;
        x = prevPlayerPostion.x;
        y = prevPlayerPostion.y;
    }

    public override void BaseUpdate(float dt)
    {
        Debug.Log(prevPlayerPostion.x);
        if (prevPlayerPostion != playerObject.transform.position)
        {
            tickEnemy();
            prevPlayerPostion = playerObject.transform.position;
        }
    }

    /*
     * @function tickEnemy
     * 
     * takes nothing. Signals the enemy to do its behavior once every
     * time the function is called 
     */
    public void tickEnemy()
    {
        moveEnemy();
    }

    private void moveEnemy()
    {
        //Debug.Log(enemy.transform.position);
        playerDistance = transform.position - playerObject.transform.position;
        if (playerDistance.x > 0)
        {
            transform.Translate(-speed* xAxis);
        }
        else if (playerDistance.x < 0)
        {
            transform.Translate(speed * xAxis);

        }
        else if (playerDistance.y > 0)
        {
            transform.Translate(-speed * yAxis);
        }
        else if (playerDistance.y < 0)
        {
            transform.Translate(speed * yAxis);
        }     

    }
}
