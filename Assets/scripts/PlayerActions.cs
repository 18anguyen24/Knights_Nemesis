using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    

    //public variables
    public bool infiniteTurns = false;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask WhatStopsMovement;
    public GameObject attackArea1;
    public GameObject attackArea2;
    public float changeHeading;
    public float Heading;
    public float newHeading;
    //this does nothing yet, but the goal is to make sure the player isn't doing actions when the game isn't ready
    public static bool canMove;

    //private variables
    private Vector3 fakePoint;
    
   

    
    private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private Health playerHealth;

    public static PlayerActions player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerActions.player = this;
        movePoint.parent = null;
        Heading = 0.0f;

        playerHealth = this.GetComponent<Health>();
        //playerHealth.returnHP();

        activeAttack = attackArea1;
        //attackArea = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //playerHealth.Heal(1);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
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

            //handles player movement and movement takes precedent over other actions
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            { 
                if (Vector3.Distance(transform.position, movePoint.position) <= .05f)  //makes sure the player has actually moved to sprite, should be irrelevant soon
                {
                    
                    //Checks to see if motion is allowed first to prevent going through corners
                    float AllowVertical = 1;
                    float AllowHorizontal = 1;
                    float AllowDiagonal = 1;

                    
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0), .2f, WhatStopsMovement))
                    {
                        AllowHorizontal = 0;
                    } else {
                        //AllowHorizontal = 1;
                    }
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowVertical = 0;
                    }
                    else
                    {
                        //AllowVertical = 1;
                    }
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowDiagonal = 0;


                    }
                    float MotionX = Input.GetAxisRaw("Horizontal") * AllowHorizontal;
                    float MotionY = Input.GetAxisRaw("Vertical") * AllowVertical;

                    if (Mathf.Abs(MotionX) == Mathf.Abs(MotionY) && AllowDiagonal == 0) {
                        MotionX = 0;
                        MotionY = 0;
                    }

                    //if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                    //{

                    //if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0), .2f, WhatStopsMovement))
                    //{
                    movePoint.position += new Vector3(MotionX, MotionY, 0);                     
                    GameState.PlayerTurn = false;
                    playerHealth.Heal(1);

                    //}

                    //}

                }
            }

            else if (Input.GetKeyDown(KeyCode.Space)) 
            {
                //Debug.Log("Player is attempting an attack");
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
