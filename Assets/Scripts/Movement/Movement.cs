using UnityEngine;
using System.Collections;

public class Movement : Base
{
    public KeyCode forward;
    public KeyCode backward;
    public string forwardAnimationCondition;
    public string backwardAnimationCondition;
    public Vector3 axis;
    public float speed;

    public override void BaseStart()
    {

    }

    public override void BaseUpdate(float dt)
    {
		if (level.gamePaused) {
			// don't do anything if the game is paused
			return;
		}

		bool movingForward = false;
        bool movingBackward = false;
        if (Input.GetKeyDown(forward))
        {
            if (!level.IsTileSolid(transform.position + speed * axis))
            {
                level.SetTileDepleted(transform.position + speed * axis);
                transform.Translate(speed * axis);   
            }
            else if (level.IsTileWater(transform.position + speed * axis))
            {
                level.SetTileDepleted(transform.position + speed * axis);
                movingForward = true;
                gameObject.GetComponent<Player>().water += 200;
                WriteText("You take a hearty gulp, hoping that it's not poisoned.");
            }
            else if (level.IsTileCamp(transform.position + speed * axis) && level.artifacts == level.req_artifacts)
            {
                level.EndLevel();
            }
        }
        else if (Input.GetKeyDown(backward))
        {
            if (!level.IsTileSolid(transform.position - speed * axis))
            {
                level.SetTileDepleted(transform.position - speed * axis);
                transform.Translate(-speed * axis);
            }
            else if (level.IsTileWater(transform.position - speed * axis))
            {
                level.SetTileDepleted(transform.position - speed * axis);
                movingBackward = true;
                gameObject.GetComponent<Player>().water += 200;
                WriteText("You take a hearty gulp, hoping that it's not poisoned.");
            }
        }

        playerObject.GetComponentInChildren<Animator>().SetBool(forwardAnimationCondition, movingForward);
        playerObject.GetComponentInChildren<Animator>().SetBool(backwardAnimationCondition, movingBackward);
    }
}
