using UnityEngine;
using System.Collections;

public class SimpleEnemyMovement : EnemyBase {
    
    
    public override void BaseStart()
    {
        base.BaseStart();
        prevPlayerPostion = playerObject.transform.position;
        xAxis = new Vector3(1, 0, 0);
        yAxis = new Vector3(0, 1, 0);
        speed = 1;
    }

    public override void BaseUpdate(float dt)
    {
        //TODO delete when level manager is finished
        base.BaseUpdate(dt);
 
        if (prevPlayerPostion != playerObject.transform.position)
        {
            tickEnemy();
            prevPlayerPostion = playerObject.transform.position;
        }

    }

    public override void tickEnemy()
    {
        base.tickEnemy();
        moveEnemy();
    }

    public override void moveEnemy()
    {
        base.tickEnemy();
 
        //Debug.Log(enemy.transform.position);
        playerDistance = transform.position - playerObject.transform.position;
        
        if (playerDistance.x > 0)
        {
            Debug.Log(playerDistance);
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

    public Vector3 getPlayerDistance()
    {
        return playerDistance;
    }
}
