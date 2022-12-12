using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DylanEnemyAction : UnitController, NPCInterface
{
    //public variables
    /*
    public float moveSpeed = 5f;
    public GameObject movePoint;

    public LayerMask WhatStopsMovement;
    */

    public GameObject attackArea1;
    

    public Transform Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;


    /*private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    */

    [SerializeField] AudioSource audioSource1;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        health = health + GameState.FloorNumber * 2;
        MAX_HEALTH = health;
        XPDropped = XPDropped * (1 + .15f * GameState.FloorNumber);

        GameState.Enemies.Add(this);

        GameObject PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = PlayerMovePoint.transform;
        
        movePoint.transform.parent = null;
     
        targetX = 0;
        targetY = 0;

        activeAttack = attackArea1;

        source = Instantiate(audioSource1);
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
        if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)    //Checks if player is one tile away
        {
            Attack();   //Attacks
            source.Play();
        }
        else
        {
            MoveEnemy();
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

        //if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)//should be unnecessary with multiple enemies
        //{
            Move(XDirection, YDirection);

            /*
            //Checks to see if motion is allowed first to prevent going through corners
            float AllowVertical = 1;
            float AllowHorizontal = 1;
            float AllowDiagonal = 1;

            if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(XDirection * MoveHorizontal, 0f, 0), .2f, WhatStopsMovement))
            {
                AllowHorizontal = 0;
            }
            if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0f, YDirection * MoveVertical, 0), .2f, WhatStopsMovement))
            {
                AllowVertical = 0;
            }
            if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(XDirection * MoveHorizontal, YDirection * MoveVertical, 0), .2f, WhatStopsMovement))
            {
                AllowDiagonal = 0;
            }

            float MotionX = XDirection * AllowHorizontal;
            float MotionY = YDirection * AllowVertical;

            if (Mathf.Abs(MotionX) == Mathf.Abs(MotionY) && AllowDiagonal == 0)
            {
                MotionX = 0;
                MotionY = 0;
            }

            movePoint.transform.position += new Vector3(MoveHorizontal * MotionX, MoveVertical * MotionY, 0);
            */
        //}


    }

    /*
    private void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }*/

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
