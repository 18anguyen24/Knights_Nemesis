using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : UnitController, NPCInterface
{
    //public variables
    /*
    public float moveSpeed = 5f;
    public GameObject movePoint;

    public LayerMask WhatStopsMovement;
    */

    public GameObject attackArea1;
    public GameObject attackArea2;
    public GameObject attackArea3;

    public Transform Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;


    //Animation
    SpriteRenderer sr;
    Animator animator;

    public GameObject player;
    public GameObject PlayerMovePoint;


    /*private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    */


    // Start is called before the first frame update
    void Start()
    {
        GameState.Enemies.Add(this);

        PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = PlayerMovePoint.transform;

        movePoint.transform.parent = null;

        targetX = 0;
        targetY = 0;

        activeAttack = attackArea1;

        player = GameObject.FindGameObjectWithTag("Player");

        //setting up animation
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        XDistance = Target.position.x - transform.position.x;
        YDistance = Target.position.y - transform.position.y;

        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * Time.deltaTime);
        

        if (attacking)
        {
            timer += Time.deltaTime;
            
            if(activeAttack == attackArea3) //spear animation
            {
                if (timer >= 0.75f)
                {
                    timer = 0;
                    attacking = false;
                    activeAttack.SetActive(attacking);
                }
            }
            else //claymore animation
            {
                if (timer >= 0.5f)
                {
                    timer = 0;
                    attacking = false;
                    activeAttack.SetActive(attacking);
                }
            }

        }
        
        
    }


    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void NPCTurn()
    {
        if (Mathf.Abs(XDistance) == Mathf.Abs(YDistance) && Mathf.Abs(XDistance) == 2) //Checks if the player is two diagonal tiles away
        {
            activeAttack = attackArea2; //Sets to attack 2
            Attack();   //Attacks
        }
        else if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)    //Checks if player is one tile away
        {
            int rnd = Random.Range(0, 10);
            if(rnd >= 5)
            {
                activeAttack = attackArea3; //Sets to attack 1
                Dash2(player);
                Attack();
            }
            else
            {
                activeAttack = attackArea1; //Sets to attack 1
                Attack();
            }

        }
        else
        {
            MoveEnemy();  //Moves
        }

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
        {
            XDirection = Mathf.Abs(XDistance) / XDistance;
            if(XDirection < 0)
            {
                sr.flipX = true;
            }
            else if (XDirection > 0)
            {
                sr.flipX = false;
            }
        }    
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
        
    }

    public Vector3 NPCLocation()
    {
        return transform.position;
    }
}
