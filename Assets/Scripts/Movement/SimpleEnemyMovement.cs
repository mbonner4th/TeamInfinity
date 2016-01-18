﻿using UnityEngine;
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
        if (viewDistance == 0)
        {
            viewDistance = 10;
        }
        if (speed == 0)
        {
            speed = 1;
        }
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
        base.moveEnemy();
        //TryToMoveCharacter(Vector3 distance, Character characterToMove);
       
       // //Debug.Log(enemy.transform.position);
        playerDistance = transform.position - playerObject.transform.position;
        


        if (Mathf.Abs(playerDistance.x) >= 2)
        {
            print("far side");
            //print(Mathf.Abs(playerDistance.x));
            if (playerDistance.x > 0 && !level.IsTileSolid(transform.position + (-speed * xAxis)))
            {
                //move left      
                print("move left");
                level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(-speed * xAxis);
            }
            else if (playerDistance.x < 0 && !level.IsTileSolid(transform.position + (speed * xAxis)))
            {
                //move right
                print("move right");
                level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(speed * xAxis);

            }
        }
        else if (Mathf.Abs(playerDistance.y) >= 2)
        {
            print("far up");
            //print(Mathf.Abs(playerDistance.y));
            if (playerDistance.y > 0 && !level.IsTileSolid(transform.position + (-speed * yAxis)))
            {
                //move down
                print("move down");
                level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(-speed * yAxis);
            }
            else if (playerDistance.y < 0 && !level.IsTileSolid(transform.position + (speed * yAxis)))
            {
                //move up
                print("move up");
                level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(speed * yAxis);
            }
        }
        //check to see if you're stuck on the environment
        else
        {
            WriteText("you got stung");
            hurtPlayer();
        }

    }

    public override void hurtPlayer()
    {
        player.health -= gameObject.GetComponent<Enemy>().damage;
        sound.PlaySound(3);
        
    }

    public Vector3 getPlayerDistance()
    {
        return playerDistance;
    }
}
