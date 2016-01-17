using UnityEngine;
using System.Collections;

public class SimpleEnemyMovement : EnemyBase {

    private Vector4 possiblePaths;
    
    public override void BaseStart()
    {
        base.BaseStart();
        if (playerObject != null)
        {
            prevPlayerPostion = playerObject.transform.position;
        }
        xAxis = new Vector3(1, 0, 0);
        yAxis = new Vector3(0, 1, 0);
        possiblePaths = new Vector4(0, 0, 0, 0);
        //^Use this
        speed = 1;
    }

    public override void BaseUpdate(float dt)
    {
        //TODO delete when level manager is finished
        base.BaseUpdate(dt);
            //tickEnemy();

    }

    public override void tickEnemy()
    {
        base.tickEnemy();
        checkPossiblePath();
        moveEnemy();
    }

    private void checkPossiblePath()
    {
        possiblePaths = new Vector4(
            !level.IsTileSolid(transform.position + (-speed * xAxis)) ? 1 : 0,
            !level.IsTileSolid(transform.position + (speed * xAxis)) ? 1 : 0,
            !level.IsTileSolid(transform.position + (-speed * yAxis)) ? 1 : 0,
            !level.IsTileSolid(transform.position + (speed * yAxis)) ? 1 : 0
            );
    }

    public override void moveEnemy()
    {
        base.tickEnemy();
       
        //Debug.Log(enemy.transform.position);
        playerDistance = transform.position - playerObject.transform.position;
      
       // Debug.Log(playerDistance);
        print("move left " + "\nmove right "+"\nmove up "+"\nmove down ");

        if (Mathf.Abs(playerDistance.x) >= 2)
        {
            //print(Mathf.Abs(playerDistance.x));
            if (playerDistance.x > 0 && !level.IsTileSolid(transform.position  + (-speed * xAxis)))
            { 
                //move left      
                print("move left");
                transform.Translate(-speed * xAxis);
            }
            else if (playerDistance.x < 0 && !level.IsTileSolid(transform.position + (speed * xAxis)))
            {
                //move right
                print("move right");
                transform.Translate(speed * xAxis);

            }
        }
        else if (Mathf.Abs(playerDistance.y) >= 2)
        {
            //print(Mathf.Abs(playerDistance.y));
            if (playerDistance.y > 0 && !level.IsTileSolid(transform.position + (-speed * yAxis)))
            {
                //move down
                print("move down");
                transform.Translate(-speed * yAxis);
            }
            else if (playerDistance.y < 0 && !level.IsTileSolid(transform.position + (speed * yAxis)))
            {
                //move up
                print("move up");
                transform.Translate(speed * yAxis);
            }
        }
        //check to see if you're stuck on the environment
        else if ((level.IsTileSolid(transform.position + (-speed * yAxis)) && !level.IsTileSolid(transform.position + (speed * yAxis))) ||
            (level.IsTileSolid(transform.position + (-speed * xAxis)) && level.IsTileSolid(transform.position  + (speed * xAxis))))
        {

        }
        else
        {
            print("get hurt");
            hurtPlayer();
        }

    }

    public override void hurtPlayer()
    {
        player.health -= gameObject.GetComponent<Enemy>().damage;
        
    }

    public Vector3 getPlayerDistance()
    {
        return playerDistance;
    }
}
