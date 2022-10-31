using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public float Heading;
    public float newHeading;
    float changeHeading;

    public LayerMask WhatStopsMovement;

    public bool infiniteTurns = true;

    private Vector3 fakePoint;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        Heading = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, Speed * Time.deltaTime);

        if (infiniteTurns == true) {
            GameState.PlayerTurn = true;
        }

        if (GameState.PlayerTurn)
        {


            if (!Input.GetMouseButton(1))
            {

                if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
                {
                    Debug.Log("ready for next movement");
                    //Checks to see if motion is allowed first to prevent going through corners
                    float AllowVertical = 1;
                    float AllowHorizontal = 1;

                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0), .2f, WhatStopsMovement))
                    {
                        AllowHorizontal = 0;
                    }
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowVertical = 0;
                    }
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowHorizontal = 0;
                        AllowVertical = 0;
                    }


                    

                    if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                    {
                        //if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0), .2f, WhatStopsMovement))
                        //{
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * AllowHorizontal, Input.GetAxisRaw("Vertical") * AllowVertical, 0);
                        newHeading = Mathf.Atan2(movePoint.position.y - transform.position.y, movePoint.position.x - transform.position.x);
                        Debug.Log("The heading is " + newHeading);

                        newHeading = Mathf.Rad2Deg * newHeading;
                        changeHeading = newHeading - Heading;
                        transform.RotateAround(transform.position, Vector3.forward, changeHeading);
                        Heading = newHeading;

                        GameState.PlayerTurn = false;
                        //}
                    }

                }
            }
            else {//player is holding right click to pause motion
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    //if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0), .2f, WhatStopsMovement))
                    //{
                    fakePoint = transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                    newHeading = Mathf.Atan2(fakePoint.y - transform.position.y, fakePoint.x - transform.position.x);
                    Debug.Log("The heading is " + newHeading);

                    newHeading = Mathf.Rad2Deg * newHeading;
                    changeHeading = newHeading - Heading;
                    transform.RotateAround(transform.position, Vector3.forward, changeHeading);
                    Heading = newHeading;

                    GameState.PlayerTurn = false;
                    //}
                }


            }

        }
        
    }
}
