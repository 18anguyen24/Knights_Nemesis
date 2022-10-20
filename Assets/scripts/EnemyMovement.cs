using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask WhatStopsMovement;
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameState.PlayerTurn)
        {
            MoveEnemy();
            GameState.PlayerTurn = true;
        }

        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Speed * Time.deltaTime);

    }

    private void MoveEnemy()
    {
        float MoveVertical = 1;
        float MoveHorizontal = 1;

        float XDirection = 0;
        float YDirection = 0;


        float XDistance = Target.position.x - transform.position.x;
        if(XDistance !=0)
            XDirection = Mathf.Abs(XDistance) / XDistance;
        float YDistance = Target.position.y - transform.position.y;
        if (YDistance != 0)
            YDirection = Mathf.Abs(YDistance) / YDistance;

        if (Mathf.Abs(XDistance) > 2 * Mathf.Abs(YDistance))
        {
            MoveVertical = 0;
        }
        if (Mathf.Abs(XDistance) * 2 < Mathf.Abs(YDistance))
        {
            MoveHorizontal = 0;
        }

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {

            //Checks to see if motion is allowed first to prevent going through corners
            float AllowVertical = 1;
            float AllowHorizontal = 1;

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
                AllowHorizontal = 0;
                AllowVertical = 0;
            }


            movePoint.position += new Vector3(MoveHorizontal * XDirection * AllowHorizontal, MoveVertical * YDirection * AllowVertical, 0);

        }


    }

}
