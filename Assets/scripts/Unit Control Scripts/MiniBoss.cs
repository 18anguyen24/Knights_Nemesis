using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : UnitController, NPCInterface
{
    //public variables
    /*
    public float moveSpeed = 5f;
    public GameObject movePoint;

    public LayerMask WhatStopsMovement;
    */

    public GameObject attackArea1;
    public GameObject attackArea2;


    public Transform Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;

    private int turns;
    public int turnsToAttack;

    public GameObject Wall;

    public PlayerActions Player;
    public GameObject TrashEnemies;

    /*private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    */


    // Start is called before the first frame update
    void Start()
    {
        MAX_HEALTH = health;
        GameState.Enemies.Add(this);

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
        XDistance = Target.position.x - transform.position.x;
        YDistance = Target.position.y - transform.position.y;


        Speed = 1;

        if (Input.GetMouseButton(0))
        {
            Speed = GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * moveSpeed * Time.deltaTime);


        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack / Speed)
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
        turns++;
        if (turns >= turnsToAttack)
        {
            turns = 0;

            if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)    //Checks if player is one tile away
            {
                int hits = (int)Mathf.Round(Random.Range(1, 3));
                activeAttack = attackArea1; //Sets to attack 1
                StartCoroutine(MultiHit(hits));
            }
            
            else if (health < MAX_HEALTH / 4)
            {
                Heal(40);
            }

            else if ((Mathf.Abs(XDistance) == Mathf.Abs(YDistance) && Mathf.Abs(XDistance) == 2) || ((XDistance == 0) && Mathf.Abs(YDistance) == 2) || ((YDistance == 0) && Mathf.Abs(XDistance) == 2)) //Checks if the player is two diagonal tiles away
            {
                activeAttack = attackArea2; //Sets to attack 2
                Attack();   //Attacks
            }
            else
            {
                MoveEnemy();  //Moves
            }


        }
        else {
            Heal(5);
        }

    }

    IEnumerator MultiHit(int hits)
    {
        timeToAttack = timeToAttack / hits;
        for (int i = 0; i < hits; i++)
        {
            yield return new WaitForSeconds(timeToAttack);
            Attack();   //Attacks
        }
        //Attack();   //Attacks
        timeToAttack = timeToAttack * hits;
        
    }


        private void MoveEnemy()    //Currently finds the direction the player is in, then calls move to move in that direction 
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

            
        }


    }

    

    public override void OnDeath()
    {
        GameState.Enemies.Remove(this);
        Destroy(gameObject);
        Destroy(movePoint);
        GameState.PlayerXP += XPDropped;

        GameState.UnlockAttack3 = true;
        Wall.SetActive(false);
        if (Player != null) {
            Player.Attack3UI.SetActive(true);
        }
        for (int e = 0; e < 15; e++)
        {
            Vector3 enemyDrop = new Vector3(2 * Mathf.Round(Random.Range(-1, 2)), 2 * Mathf.Round(Random.Range(-1, 2)), 0);
            enemyDrop += Player.transform.position;
            if (!Physics2D.OverlapCircle(enemyDrop, .2f, WhatStopsMovement))
            {
                GameObject newEnemy = Instantiate(TrashEnemies, enemyDrop, Quaternion.identity);
            }
        }
    }

    public Vector3 NPCLocation()
    {
        return transform.position;
    }
}
