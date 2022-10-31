using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //public variables
    public bool infiniteTurns = true;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask WhatStopsMovement;
    public GameObject attackArea1;
    public GameObject attackArea2;
    public float changeHeading;

    //private variables
    private Vector3 fakePoint;
    public float Heading;
    public float newHeading;
   

    
    private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        Heading = 0.0f;

        activeAttack = attackArea1;
        //attackArea = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //handles the movement of the player to the movepoint
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
        


        if (infiniteTurns == true)
        {
            GameState.PlayerTurn = true;
        }

        //this is a tedious way to do it but imma try it
        if (Input.GetKey("1"))
        {
            activeAttack = attackArea1;
        }
        else if (Input.GetKey("2"))
        {
            activeAttack = attackArea2;
        }

        if (Input.GetMouseButton(1))
        { //right click pauses actions
            //fakePoint = transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            //newHeading = Mathf.Atan2(fakePoint.y - transform.position.y, fakePoint.x - transform.position.x);
            //Debug.Log("The heading is " + newHeading);
            //if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
              //  newHeading = Mathf.Rad2Deg * newHeading;
                //changeHeading = newHeading - Heading;
                //transform.RotateAround(transform.position, Vector3.forward, changeHeading);
                //Heading = newHeading;
            //}

        }
        else if (GameState.PlayerTurn){ //list of actions, makes sure its player turn
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            { //handles player movement and movement takes precedent over other actions
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
                        //newHeading = Mathf.Atan2(movePoint.position.y - transform.position.y, movePoint.position.x - transform.position.x);
                        //Debug.Log("The heading is " + newHeading);

                        //newHeading = Mathf.Rad2Deg * newHeading;
                        //changeHeading = newHeading - Heading;
                        //transform.RotateAround(transform.position, Vector3.forward, changeHeading);
                        //Heading = newHeading;

                        GameState.PlayerTurn = false;
                        //}
                        
                    }

                }
            }

            else if (Input.GetKeyDown(KeyCode.Space)) 
            {
                Debug.Log("Player is attempting an attack");
                Attack();
                GameState.PlayerTurn = false;
            }
            
        
        }



    }
    private void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }
}
