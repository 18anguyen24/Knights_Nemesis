using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : UnitController, NPCInterface
{
    //This NPC won't have any attacks, but others might?
    //public GameObject attackArea1;
    //public GameObject attackArea2;

    //Hooked to spawner to handle penalty for players
    public EnemySpawner spawner;

    public Vector3 Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;

    private int steps = 0;
    private int stepsneeded = 0;

    
    
    void Start()
    {
        health = health + GameState.FloorNumber * 5;
        MAX_HEALTH = health;
        XPDropped = XPDropped * (1 + .15f * GameState.FloorNumber);

        GameState.NPCs.Add(this);

        Target = new Vector3(0,.5f,0);
        
        movePoint.transform.parent = null;

        targetX = 0;
        targetY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        XDistance = Target.x - transform.position.x;
        YDistance = Target.y - transform.position.y;

        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * Time.deltaTime);
    }

    //Gives the NPC a random target within a 5 tile radius to walk towards
    private void randomTarget()
    {
        Target = transform.position + new Vector3(Mathf.Round(Random.Range(-5, 5)), Mathf.Round(Random.Range(-5, 5)), 0);
        

    }

    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void NPCTurn()
    {
        MoveNPC();
        //Only option for this NPC is to move
    }




    private void MoveNPC()   
    {
        
        float XDirection = 0;
        float YDirection = 0;

        
        if (XDistance != 0)
            XDirection = Mathf.Abs(XDistance) / XDistance;
        
        if (YDistance != 0)
            YDirection = Mathf.Abs(YDistance) / YDistance;
        /*
        if (Mathf.Abs(XDistance) > 3 * Mathf.Abs(YDistance))
        {
            //MoveVertical = 0;
        }
        if (Mathf.Abs(XDistance) * 3 < Mathf.Abs(YDistance))
        {
            //MoveHorizontal = 0;
        }

        targetX = XDirection * MoveHorizontal;
        targetY = YDirection * MoveVertical;
        */

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)//should be unnecessary with multiple enemies
        {
            Move(XDirection, YDirection);
            steps++;
            //Walks a random number of steps before setting a new target
            if (steps >= stepsneeded) {
                steps = 0;
                stepsneeded = Random.Range(1, 4);
                randomTarget();
            }
            
        }


    }

    public override void OnDeath()
    {
        GameState.NPCs.Remove(this);
        Destroy(gameObject);
        Destroy(movePoint);
        spawner.DEATH();        //Penalizes player for their sins
        GameState.PlayerXP += XPDropped;
    }

    public Vector3 NPCLocation()
    {
        return transform.position;
    }
}
