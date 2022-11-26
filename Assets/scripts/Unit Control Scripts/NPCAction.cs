using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAction : UnitController, NPCInterface
{
    //This NPC won't have any attacks, but others might?
    //public GameObject attackArea1;
    //public GameObject attackArea2;

    public EnemySpawner spawner;

    public Vector3 Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;

    private int steps = 0;
    private int stepsneeded = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        MAX_HEALTH = health;
        GameState.NPCs.Add(this);

        //Only initializing target as player
        //GameObject PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = new Vector3(0,.5f,0);
        

        movePoint.transform.parent = null;

        targetX = 0;
        targetY = 0;

        //activeAttack = attackArea1;


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


        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                activeAttack.SetActive(attacking);
            }

        }

    }
    private void randomTarget()
    {
        Target = transform.position + new Vector3(Mathf.Round(Random.Range(-5, 5)), Mathf.Round(Random.Range(-5, 5)), 0);
        

    }

    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void NPCTurn()
    {
        MoveNPC();

    }




    private void MoveNPC()    //Currently finds the direction the player is in, then calls move to move in that direction 
                                //Move is in UnitController, and will check for collision
    {
        //float MoveVertical = 1;
        //float MoveHorizontal = 1;


        float XDirection = 0;
        float YDirection = 0;

        //Debug.Log("The x and Y distaces are " + XDistance + "," + YDistance);

        //float XDistance = Target.position.x - transform.position.x;
        if (XDistance != 0)
            XDirection = Mathf.Abs(XDistance) / XDistance;
        //float YDistance = Target.position.y - transform.position.y;
        if (YDistance != 0)
            YDirection = Mathf.Abs(YDistance) / YDistance;

        if (Mathf.Abs(XDistance) > 3 * Mathf.Abs(YDistance))
        {
            //MoveVertical = 0;
        }
        if (Mathf.Abs(XDistance) * 3 < Mathf.Abs(YDistance))
        {
            //MoveHorizontal = 0;
        }

        //targetX = XDirection * MoveHorizontal;
        //targetY = YDirection * MoveVertical;

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)//should be unnecessary with multiple enemies
        {
            
            Move(XDirection, YDirection);
            steps++;
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
        spawner.DEATH();
    }

    public Vector3 NPCLocation()
    {
        return transform.position;
    }
}
