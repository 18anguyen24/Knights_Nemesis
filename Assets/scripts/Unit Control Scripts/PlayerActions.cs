using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerActions : UnitController
{


    //public variables
    public bool infiniteTurns = false;
    /*
    public float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask WhatStopsMovement;
    */
    public GameObject attackArea1;
    public GameObject attackArea2;
    public GameObject attackArea3;

    public GameObject UnlockAttack3;
    public GameObject Attack3UI;

    public Image deathScreen;
    public Image Attack1Selected;
    public Image Attack2Selected;
    public Image Attack3Selected;
    

    public float turnDelay;

    public EnemySpawner spawner;

    //for animations
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //for audio
    public AudioSource source;

    public static PlayerActions player;

    public int StepsToHeal;
    private int steps;
    // Start is called before the first frame update
    void Start()
    {
        MAX_HEALTH = MAX_HEALTH + (GameState.PlayerLevel * 5);
        PlayerActions.player = this;

        //randomSpawn();

        movePoint.transform.parent = null;

        activeAttack = attackArea1;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        //Debug.Log(GameState.PlayerXP + "/" + GameState.XPtoLevel);
        if (GameState.PlayerXP >= GameState.XPtoLevel) {
            //Debug.Log("Max health is now: " + MAX_HEALTH);
            GameState.PlayerLevel++;
            MAX_HEALTH += 5;
            GameState.PlayerXP = GameState.PlayerXP - GameState.XPtoLevel;
            GameState.XPtoLevel *= 1.5f;
            Debug.Log("LEVEL UP");
        }
        Speed = 1;
        if (Input.GetMouseButton(0))
        {
            Speed = GameState.SpeedFactor;
        }
        //Debug.Log("Speed " + Speed);
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

        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameState.clear();
            SceneManager.LoadScene(0);
            
        }*/



        if (infiniteTurns == true)
        {
            GameState.PlayerTurn = true;
        }

        //this is a tedious way to do it but imma try it
        if (attacking == false) {
            if (Input.GetKey("1"))
            {
                activeAttack = attackArea1;
            }
            else if (Input.GetKey("2"))
            {
                activeAttack = attackArea2;
            }
            else if (UnlockAttack3.activeSelf && Input.GetKey("3"))
            {
                activeAttack = attackArea3;
            }
        }
        if (activeAttack == attackArea1)
        {
            Attack1Selected.enabled = true;
            Attack2Selected.enabled = false;
            Attack3Selected.enabled = false;
        }
        else if (activeAttack == attackArea2)
        {
            Attack1Selected.enabled = false;
            Attack2Selected.enabled = true;
            Attack3Selected.enabled = false;
        }
        else if (activeAttack == attackArea3)
        {
            Attack1Selected.enabled = false;
            Attack2Selected.enabled = false;
            Attack3Selected.enabled = true;
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Player is attempting an attack");
                Attack();
                GameState.PlayerTurn = false;

                StartCoroutine(enemyLoop());
            }
        }
        else if (GameState.PlayerTurn)
        { //list of actions, makes sure its player turn
            

            //handles player movement and movement takes precedent over other actions
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)  //makes sure the player has actually moved to sprite, should be irrelevant soon
                {
                   
                    Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                    //run animation
                    animator.SetBool("isMovingRight", true);
                  
                    GameState.PlayerTurn = false;

                    StartCoroutine(enemyLoop());

                    spawner.newEnemy();

                    steps++;
                    if (steps >= StepsToHeal)
                    {
                        steps = 0;
                        Heal(1);
                    }

                    
                }

            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Player is attempting an attack");
                Attack();
                source.Play();
                GameState.PlayerTurn = false;

                StartCoroutine(enemyLoop());
            }
            else
            {
                animator.SetBool("isMovingRight", false);
            }

            if(Input.GetAxisRaw("Horizontal") < 0)
            {
                animator.SetBool("isFacingBack", false);
                animator.SetBool("isFacingFront", false);
                animator.SetBool("isMovingRight", true);
                spriteRenderer.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetBool("isFacingBack", false);
                animator.SetBool("isFacingFront", false);
                animator.SetBool("isMovingRight", true);
                spriteRenderer.flipX = false;
            }
            else if(Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetBool("isFacingBack", true);
                animator.SetBool("isFacingFront", false);
                animator.SetBool("isMovingRight", false);
            }
            else if(Input.GetAxisRaw("Vertical") < 0)
            {
                animator.SetBool("isFacingBack", false);
                animator.SetBool("isFacingFront", true);
                animator.SetBool("isMovingRight", false);
            }

        }



    }
    /*
    private void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }*/

    public override void OnDeath()
    {
        deathScreen.enabled = !deathScreen.enabled;
        Destroy(gameObject);
        Destroy(movePoint);
        Debug.Log("Player Died");
    }


    IEnumerator enemyLoop()
    {
        yield return new WaitForSeconds(turnDelay/Speed);
        //Debug.Log("Time Delay: " + turnDelay/Speed);
        for (int i = 0; i < GameState.NPCs.Count; i++)
        {
            GameState.NPCs[i].NPCTurn();
            if (Vector3.Distance(PlayerActions.player.transform.position, GameState.NPCs[i].NPCLocation()) < 5)
            {
                yield return new WaitForSeconds(turnDelay / Speed);
            }

        }
        for (int i = 0; i < GameState.Enemies.Count; i++)
        {
            GameState.Enemies[i].NPCTurn();
            if (Vector3.Distance(PlayerActions.player.transform.position, GameState.Enemies[i].NPCLocation()) < 5)
            {
                yield return new WaitForSeconds(turnDelay/Speed);
            }
            
        }
        //Debug.Log("Number of Enemies: " + GameState.Enemies.Count);
        GameState.PlayerTurn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack3"))
        {
            UnlockAttack3.SetActive(true);
            Attack3UI.SetActive(true);
        }
    }
}