using UnityEngine;
using System.Collections;

public class snakeMovement : EnemyBase
{
    private int movementTacker;

    public override void BaseStart()
    {
        base.BaseStart();
        movementTacker = 0;
        xAxis = new Vector3(1, 0, 0);
        yAxis = new Vector3(0, 1, 0);
        speed = 1;
    }

    public override void BaseUpdate(float dt)
    {
        //TODO delete when level manager is finished
        base.BaseUpdate(dt);

       
       

    }

    public override void tickEnemy()
    {
        base.tickEnemy();
        moveEnemy();
    }

    public override void moveEnemy()
    {
        base.moveEnemy();
        Vector3 playerDistance = transform.position - playerObject.transform.position;
       

        if (Mathf.Abs(playerDistance.x) >= 1 && Mathf.Abs(playerDistance.y) >= 1)
        {

            if (Mathf.Abs(playerDistance.x) > Mathf.Abs(playerDistance.y) && Mathf.Abs(playerDistance.x) > 2)
            {
                //side squiggle
                Debug.Log("Side squliggle");
                switch (movementTacker % 5)
                {

                    case 0:
                        //forward
                        if (playerDistance.x > 0)
                        {
                            transform.Translate(-speed * xAxis);
                        }
                        else
                        {
                            transform.Translate(speed * xAxis);

                        }
                        movementTacker++;
                        break;
                    case 1:
                    case 4:
                        //up
                        if (playerDistance.x > 0)
                        {
                            transform.Translate(-speed * xAxis);
                        }
                        else
                        {
                            transform.Translate(speed * xAxis);
                        }
                        transform.Translate(speed * yAxis);
                        movementTacker++;
                        break;
                    case 2:
                    case 3:
                        //down
                        if (playerDistance.x > 0)
                        {
                            transform.Translate(-speed * xAxis);
                        }
                        else
                        {
                            transform.Translate(speed * xAxis);
                        }
                        transform.Translate(-speed * yAxis);
                        movementTacker++;

                        break;

                }
            }
            else if (Mathf.Abs(playerDistance.y) > Mathf.Abs(playerDistance.x) && Mathf.Abs(playerDistance.y) > 2)
            {
                //up/down squiggle
                Debug.Log("up/down squiggler");
                switch (movementTacker % 5)
                {

                    case 0:
                        //forward
                        if (playerDistance.y > 0)
                        {
                            transform.Translate(-speed * yAxis);
                        }
                        else
                        {
                            transform.Translate(speed * yAxis);

                        }
                        movementTacker++;
                        break;
                    case 1:
                    case 4:
                        //up
                        if (playerDistance.y > 0)
                        {
                            transform.Translate(-speed * yAxis);
                        }
                        else
                        {
                            transform.Translate(speed * yAxis);
                        }
                        transform.Translate(speed * xAxis);
                        movementTacker++;
                        break;
                    case 2:
                    case 3:
                        //down
                        if (playerDistance.y > 0)
                        {
                            transform.Translate(-speed * yAxis);
                        }
                        else
                        {
                            transform.Translate(speed * yAxis);
                        }
                        transform.Translate(-speed * xAxis);
                        movementTacker++;

                        break;

                }
            }
            else
            {
                if (playerDistance.x > 0)
                {
                    transform.Translate(-speed * xAxis);
                }
                else if (playerDistance.x < 0)
                {
                    transform.Translate(speed * xAxis);

                }
                if (playerDistance.y > 0)
                {
                    transform.Translate(-speed * yAxis);
                }
                else if (playerDistance.y < 0)
                {
                    transform.Translate(speed * yAxis);
                }

            }
        }
        else
        {
            hurtPlayer();
        }
    }

    public override void hurtPlayer()
    {
        if (player != null)
        {
            Debug.Log(player.health);
            //player.health -= gameObject.GetComponent<Enemy>().damage;
        }
        else
        {
            print("no player :(");
            int explosions = player.health;
        }
    }
}
