using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only Enemy I plan to comment, this enemy has the basic mechanics that the rest of the enemies were built from
public class BasicEnemyAction : UnitController, NPCInterface
{
    //Add the attacks
    public GameObject attackArea1;
    public GameObject attackArea2;
    
    //The target the enemy will move towards and attack
    public Transform Target;

    //Acessed by other scripts
    public float targetX;
    public float targetY;

    private float XDistance;
    private float YDistance;

    void Start()
    {
        //Similar startup as player, scaling to the appropriate power
        health = health + GameState.FloorNumber * 5;
        MAX_HEALTH = health;
        XPDropped = XPDropped * (1 + .15f * GameState.FloorNumber);

        //Added to the list of enemies that will be used
        GameState.Enemies.Add(this);

        //Targets the players movepoint
        GameObject PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = PlayerMovePoint.transform;
        
        movePoint.transform.parent = null;
     
        targetX = 0;
        targetY = 0;

        activeAttack = attackArea1;
    }

    // Update is called once per frame
    void Update()
    {
        //Finds where the target is relative
        XDistance = Target.position.x - transform.position.x;
        YDistance = Target.position.y - transform.position.y;

        //Applies speedup to all animations
        Speed = 1;
        if (Input.GetMouseButton(0))
        {
            Speed = GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * moveSpeed* Time.deltaTime);
        

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack/Speed)
            {
                timer = 0;
                attacking = false;
                activeAttack.SetActive(attacking);
            }

        }
        
    }


    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void NPCTurn()
    {
        if ((Mathf.Abs(XDistance) == Mathf.Abs(YDistance) && Mathf.Abs(XDistance) == 2) || ((XDistance == 0) && Mathf.Abs(YDistance) == 2) || ((YDistance == 0) && Mathf.Abs(XDistance) == 2)) //Checks if the player is tiles away, either diagonal or straight
        {
            activeAttack = attackArea2; //Sets to attack 2
            Attack();   //Attacks
        }
        else if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)    //Checks if player is one tile away
        {
            activeAttack = attackArea1; //Sets to attack 1
            Attack();   //Attacks
        }
        else
        {
            MoveEnemy();  //Moves
        }

    }




    private void MoveEnemy()    //Currently finds the direction the player is in, then calls move to move in that direction 
                                //Move is in UnitController, and will check for collision
    {
        
        float XDirection = 0;
        float YDirection = 0;

        //Creates a normalized X and Y direction to be used
        if (XDistance != 0)
            XDirection = Mathf.Abs(XDistance) / XDistance;
        
        if (YDistance != 0)
            YDirection = Mathf.Abs(YDistance) / YDistance;

        /* Currently not in use, the enemy will move diagonal if not exactly inline with the player
        if (Mathf.Abs(XDistance) > 3 * Mathf.Abs(YDistance))
        {
            MoveVertical = 0;
        }
        if (Mathf.Abs(XDistance) * 3 < Mathf.Abs(YDistance))
        {
            MoveHorizontal = 0;
        }*/

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)//should be unnecessary with multiple enemies
        {
            //Only moves one unit at a time, so the directions are passed, and allow the universal move to handle the logic further
            Move(XDirection, YDirection);
        }


    }

    public override void OnDeath()
    {
        GameState.Enemies.Remove(this);
        Destroy(gameObject);
        Destroy(movePoint);
        GameState.PlayerXP += XPDropped;
        
    }

    public Vector3 NPCLocation()
    {
        return transform.position;
    }
}
