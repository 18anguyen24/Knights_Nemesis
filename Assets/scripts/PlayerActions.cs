using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerActions : UnitController
{


    //public variables
    public bool infiniteTurns = false;
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask WhatStopsMovement;
    public GameObject attackArea1;
    public GameObject attackArea2;

    public Image deathScreen;

    public float turnDelay;



    public EnemySpawner spawner;


    private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private float Speed;

    public static PlayerActions player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerActions.player = this;

        //randomSpawn();


        movePoint.parent = null;



        activeAttack = attackArea1;

    }

    private void randomSpawn()
    {
        bool spawned = false;

        while (spawned == false)
        {
            Vector3 spawnPoint = new Vector3(Mathf.Round(Random.Range(-15, 15)), Mathf.Round(Random.Range(-20, 0)), 0);
            spawnPoint += transform.position;


            if (!Physics2D.OverlapCircle(spawnPoint, .2f, WhatStopsMovement))
            {
                transform.position = spawnPoint;
                spawned = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //playerHealth.Heal(1);
        //handles the movement of the player to the movepoint
        Speed = moveSpeed;
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
            GameState.PlayerTurn = true;
            GameState.EnemyCount = 0;
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
        {

        }
        else if (GameState.PlayerTurn)
        { //list of actions, makes sure its player turn

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
                    }
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowVertical = 0;
                    }

                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0), .2f, WhatStopsMovement))
                    {
                        AllowDiagonal = 0;
                    }
                    float MotionX = Input.GetAxisRaw("Horizontal") * AllowHorizontal;
                    float MotionY = Input.GetAxisRaw("Vertical") * AllowVertical;

                    if (Mathf.Abs(MotionX) == Mathf.Abs(MotionY) && AllowDiagonal == 0)
                    {
                        MotionX = 0;
                        MotionY = 0;
                    }

                    movePoint.position += new Vector3(MotionX, MotionY, 0);
                    GameState.PlayerTurn = false;

                    StartCoroutine(enemyLoop());

                    spawner.newEnemy();

                    Heal(1);
                }
            }

            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Player is attempting an attack");
                Attack();
                GameState.PlayerTurn = false;

                StartCoroutine(enemyLoop());
            }

        }



    }
    private void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }

    public override void OnDeath()
    {
        deathScreen.enabled = !deathScreen.enabled;
        Destroy(gameObject);
        Destroy(movePoint);
        Debug.Log("Player Died");
    }


    IEnumerator enemyLoop()
    {
        yield return new WaitForSeconds(turnDelay / Speed);
        for (int i = 0; i < GameState.EnemyCount; i++)
        {
            GameObject EnemyPoint = GameObject.FindWithTag("Moved");
            EnemyPoint.gameObject.tag = "Unmoved";
            if (Vector3.Distance(transform.position, EnemyPoint.transform.position) < 6)
            {
                yield return new WaitForSeconds(turnDelay / Speed);
            }

        }
        Debug.Log("Number of Enemies: " + GameState.EnemyCount);
        GameState.PlayerTurn = true;
        GameState.PlayerTurn = true;
    }

}