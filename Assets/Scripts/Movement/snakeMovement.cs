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
        if (Mathf.Sqrt(distance) >= viewDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public override void moveEnemy()
    {
        base.moveEnemy();
        Vector3 playerDistance = transform.position - playerObject.transform.position;
        //gameObject.GetComponent<Animator>().SetBool("attackingPlayerLeft", false);

        if (Mathf.Abs(playerDistance.x) > Mathf.Abs(playerDistance.y) && Mathf.Abs(playerDistance.x) > 1)
        {
            switch (movementTacker % 5)
            {

                case 0:
                    //forward
                    if (playerDistance.x > 0)
                    {
                        //transform.Translate(-speed * xAxis);
                        level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * xAxis);
                        level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());

                    }
                    movementTacker++;
                    break;
                case 1:
                case 4:
                    //up
                    if (playerDistance.x > 0)
                    {
                        //transform.Translate(-speed * xAxis);
                        level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * xAxis);
                        level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                    }
                    //transform.Translate(speed * yAxis);
                    level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                    movementTacker++;
                    break;
                case 2:
                case 3:
                    //down
                    if (playerDistance.x > 0)
                    {
                        //transform.Translate(-speed * xAxis);
                        level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * xAxis);
                        level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                    }
                    //transform.Translate(-speed * yAxis);
                    level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                    movementTacker++;
                    break;

            }
        }
        else if (Mathf.Abs(playerDistance.y) > Mathf.Abs(playerDistance.x) && Mathf.Abs(playerDistance.y) > 1)
        {
            //up/down squiggle
            //Debug.Log("up/down squiggler");
            switch (movementTacker % 5)
            {

                case 0:
                    //forward
                    if (playerDistance.y > 0)
                    {
                        //transform.Translate(-speed * yAxis);
                        level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * yAxis);
                        level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());

                    }
                    movementTacker++;
                    break;
                case 1:
                case 4:
                    //up
                    if (playerDistance.y > 0)
                    {
                        //transform.Translate(-speed * yAxis);
                        level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * yAxis);
                        level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                    }
                    //transform.Translate(speed * xAxis);
                    level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                    movementTacker++;
                    break;
                case 2:
                case 3:
                    //down
                    if (playerDistance.y > 0)
                    {
                        //transform.Translate(-speed * yAxis);
                        level.TryToMoveCharacter(transform.position + (-speed * yAxis), gameObject.GetComponent<Enemy>());
                    }
                    else
                    {
                        //transform.Translate(speed * yAxis);
                        level.TryToMoveCharacter(transform.position + (speed * yAxis), gameObject.GetComponent<Enemy>());
                    }
                    //transform.Translate(-speed * xAxis);
                    level.TryToMoveCharacter(transform.position + (-speed * xAxis), gameObject.GetComponent<Enemy>());
                    movementTacker++;

                    break;

            }
        }
        else
        {
            //hurtPlayer();
            if (playerDistance.x > 0)
            {
                level.TryToMoveCharacter((-speed * xAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(-speed * xAxis);
            }
            else if (playerDistance.x < 0)
            {
                level.TryToMoveCharacter((speed * xAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(speed * xAxis);

            }
            if (playerDistance.y > 0)
            {
                level.TryToMoveCharacter((-speed * yAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(-speed * yAxis);
            }
            else if (playerDistance.y < 0)
            {
                level.TryToMoveCharacter((speed * yAxis), gameObject.GetComponent<Enemy>());
                //transform.Translate(speed * yAxis);
            }

        }
        if ((Mathf.Abs(playerDistance.x) + Mathf.Abs(playerDistance.y)) <= 2)
        {
            WriteText("A snake bit you!");
            hurtPlayer();

            print((playerDistance.y > 0) + ", " + (playerDistance.x > 0));
            //gameObject.GetComponent<Animator>().SetBool("attackingPlayerUp", playerDistance.y > 0);
            //gameObject.GetComponent<Animator>().SetBool("attackingPlayerDown", playerDistance.y < 0);
            //gameObject.GetComponent<Animator>().SetBool("attackingPlayerLeft", true);
            //gameObject.GetComponent<Animator>().SetBool("attackingPlayerRight", playerDistance.x > 0);
            gameObject.GetComponent<Animator>().Play("AttackingPlayerLeft");
        }
    }

    public override void hurtPlayer()
    {

        if (player != null)
        {
            //Debug.Log(player.health);
            player.health -= gameObject.GetComponent<Enemy>().damage;
        }
        else
        {
            print("no player :(");
            int explosions = player.health;
        }
    }
}
