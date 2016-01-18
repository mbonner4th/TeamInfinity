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
        print(viewDistance);
        if (viewDistance == 0.0f)
        {
            viewDistance = 100.0f;
        }
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

    public override bool seePlayer()
    {
        base.seePlayer();
        float totalX = playerObject.transform.position.x - transform.position.x;
        float totalY = playerObject.transform.position.y - transform.position.y;
        float distance = Mathf.Pow(totalX, 2) + Mathf.Pow(totalY, 2);
       //print("distance: " + Mathf.Sqrt(distance) + " can see: " + viewDistance);
        if (Mathf.Sqrt(distance) <= viewDistance)
        {
            //print("I see you");
            return true;
        }
        else
        {
            print("we ain't found shit");
            return false;
        }

    }

    public override void moveEnemy()
    {
        base.moveEnemy();
        Vector3 playerDistance = transform.position - playerObject.transform.position;

        if (seePlayer())
        {
            if (Mathf.Abs(playerDistance.x) > Mathf.Abs(playerDistance.y) && Mathf.Abs(playerDistance.x) > 1)
            {
                print("move left/right");
                switch (movementTacker % 5)
                {
                    case 0:
                        if (playerDistance.x > 0)
                        {
                            level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        movementTacker++;
                        break;
                    case 1:
                    case 4:
                        //up
                        if (playerDistance.x > 0)
                        {
                            level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                        movementTacker++;
                        break;
                    case 2:
                    case 3:
                        //down
                        if (playerDistance.x > 0)
                        {
                            level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                        }
                        level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                        movementTacker++;
                        break;

                }
            }
            else if (Mathf.Abs(playerDistance.y) > Mathf.Abs(playerDistance.x) && Mathf.Abs(playerDistance.y) > 1)
            {
                //up/down squiggle
                Debug.Log("up/down squiggler");
                switch (movementTacker % 5)
                {

                    case 0:
                        //forward
                        if (playerDistance.y > 0)
                        {
                            level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());

                        }
                        movementTacker++;
                        break;
                    case 1:
                    case 4:
                        //up
                        if (playerDistance.y > 0)
                        {
                            level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                        }
                        level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                        movementTacker++;
                        break;
                    case 2:
                    case 3:
                        //down
                        if (playerDistance.y > 0)
                        {
                            level.TryToMoveCharacter(transform.position + (-speed * yAxis), gameObject.GetComponent<Enemy>());
                        }
                        else
                        {
                            level.TryToMoveCharacter(transform.position + (speed * yAxis), gameObject.GetComponent<Enemy>());
                        }
                        level.TryToMoveCharacter(transform.position + (-speed * xAxis), gameObject.GetComponent<Enemy>());
                        movementTacker++;
                        break;

                }
            }
            else
            {
             hurtPlayer();
            }
        }
    }

    public override void hurtPlayer()
    {

        if (player != null)
        {
            WriteText("A Snake bit you for " + gameObject.GetComponent<Enemy>().damage+" damage" );
            player.health -= gameObject.GetComponent<Enemy>().damage;
            sound.PlaySound(2);
        }
        else
        {
            print("no player :(");
            int explosions = player.health;
        }
    }
}
