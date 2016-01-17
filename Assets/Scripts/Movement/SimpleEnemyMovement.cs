﻿using UnityEngine;
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
            //tickEnemy();

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


        if (Mathf.Abs(playerDistance.x) >= 1 && Mathf.Abs(playerDistance.y) >= 1)
        {
            if (playerDistance.x > 0)
            {
                Debug.Log(playerDistance);
                transform.Translate(-speed * xAxis);
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
        else
        {
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
