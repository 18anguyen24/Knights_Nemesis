using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    //public variables
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask WhatStopsMovement;
    public Transform Target;

    public GameObject attackArea1;
    //public GameObject attackArea2;


    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;
    
    

    private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = PlayerMovePoint.transform;
        //Target = PlayerActions.player.transform;
        movePoint.parent = null;
        //Heading = 0.0f;
        targetX = 0;
        targetY = 0;

        activeAttack = attackArea1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player Location" + PlayerActions.player.transform);

        XDistance = Target.position.x - transform.position.x;
        YDistance = Target.position.y - transform.position.y;

        if (movePoint.tag == "Unmoved")
        {

            EnemyTurn();
           
            movePoint.tag = "Moved";
        }

        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Speed * Time.deltaTime);

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

    public void EnemyTurn() {
        if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)
        {
            //Debug.Log("attacking");
            Attack();
            //GameState.PlayerTurn = false;
        }
        else
        {
            //Debug.Log("moving");
            MoveEnemy();
        }
        movePoint.tag = "Moved";
    }


    private void MoveEnemy()
    {
        float MoveVertical = 1;
        float MoveHorizontal = 1;


        float XDirection = 0;
        float YDirection = 0;

        //Debug.Log("The x and Y distaces are " + XDistance + "," + YDistance);

        //float XDistance = Target.position.x - transform.position.x;
        if (XDistance !=0)
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

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)//should be unnecessary with multiple enemies
        {

            //Checks to see if motion is allowed first to prevent going through corners
            float AllowVertical = 1;
            float AllowHorizontal = 1;
            float AllowDiagonal = 1;

            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(XDirection * MoveHorizontal, 0f, 0), .2f, WhatStopsMovement))
            {
                AllowHorizontal = 0;
            }
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, YDirection * MoveVertical, 0), .2f, WhatStopsMovement))
            {
                AllowVertical = 0;
            }
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(XDirection * MoveHorizontal, YDirection * MoveVertical, 0), .2f, WhatStopsMovement))
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

            movePoint.position += new Vector3(MoveHorizontal * MotionX, MoveVertical * MotionY, 0);

        }


    }

   
    private void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }



}
