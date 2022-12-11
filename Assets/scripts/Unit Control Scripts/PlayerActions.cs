using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PlayerActions : UnitController
{


    //public variables
    public bool infiniteTurns = false;  //testing purposes

    public int RandSpawnX;  //Allows the player to be spawned within a certain area of each map
    public int RandSpawnY;
    
    //Gives the player 3 attacks
    public GameObject attackArea1;
    public GameObject attackArea2;
    public GameObject attackArea3;

    //Handles the UI attached to the player
    public GameObject Attack3UI;

    public Image deathScreen;
    public Image Attack1Selected;
    public Image Attack2Selected;
    public Image Attack3Selected;

    public TextMeshProUGUI PlayerLevel;

    //Sets the time between each units movement
    public float turnDelay;

    public EnemySpawner spawner;

    //for animations
    Animator animator;
    SpriteRenderer spriteRenderer;

    //for audio
    public AudioSource source;

    public static PlayerActions player;

    //Allows controlable healing
    public int StepsToHeal;
    private int steps;
    

    void Start()
    {
        //handles the  transitions between levels
        if (GameState.PlayerHP != 0) {
            health = GameState.PlayerHP;
            Debug.Log("Players Health: " + health);
        }

        //Increases the players health to the proper amount
        MAX_HEALTH = MAX_HEALTH + (GameState.PlayerLevel * 5);

        //Allows access from other scripts
        PlayerActions.player = this;
        
        //Spawns the player somewhere random
        randomSpawn();

        movePoint.transform.parent = null;

        activeAttack = attackArea1;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Second check to make sure the player always gets to start
        GameState.PlayerTurn = true;

        //Shows the 3d attack if unlocked
        if (GameState.UnlockAttack3 == true) {
            Attack3UI.SetActive(true);
        }
        
        //Displays player level
        PlayerLevel.text = "Lvl: " + (GameState.PlayerLevel);
    }

    //Finds a valid spawnpoint within the specifiec area and spawns the player there
    private void randomSpawn()
    {
        bool spawned = false;

        while (spawned == false)
        {
            Vector3 spawnPoint = new Vector3(Mathf.Round(Random.Range(0, RandSpawnX)), Mathf.Round(Random.Range(0, RandSpawnY)), 0);
            spawnPoint += transform.position;


            if (!Physics2D.OverlapCircle(spawnPoint, .2f, WhatStopsMovement))
            {
                transform.position = spawnPoint;
                spawned = true;
            }
        }
    }

    
    void Update()
    {
        //Allows the player to level up whenever during the game
        if (GameState.PlayerXP >= GameState.XPtoLevel) {
            GameState.PlayerLevel++;
            MAX_HEALTH += 5;
            GameState.PlayerXP = GameState.PlayerXP - GameState.XPtoLevel;
            GameState.XPtoLevel *= 1.3f;
            PlayerLevel.text = "Lvl: " + (GameState.PlayerLevel);
        }

        //Allows for speedup of gameplay at anypoint
        Speed = 1;
        if (Input.GetMouseButton(0))
        {
            Speed = GameState.SpeedFactor;
        }

        //Moves the player to the designated movepoint
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * moveSpeed * Time.deltaTime);

        //Gives a timer to display the attack for a certain length of time
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

        //Debugging purposes
        if (infiniteTurns == true)
        {
            GameState.PlayerTurn = true;
        }

        //Sets the players attacks using the keys
        if (attacking == false) {
            if (Input.GetKey("1"))
            {
                activeAttack = attackArea1;
            }
            else if (Input.GetKey("2"))
            {
                activeAttack = attackArea2;
            }
            else if (GameState.UnlockAttack3 == true && Input.GetKey("3"))
            {
                activeAttack = attackArea3;
            }
        }

        //Handles the UI for displaying the proper attack
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

        //Allows the player to rotate and/or attack without moving
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Attack();
                source.Play();
                GameState.PlayerTurn = false;

                StartCoroutine(enemyLoop());
            }
        }
        else if (GameState.PlayerTurn)
        { //list of actions, makes sure its player turn
            

            //handles player movement and movement takes precedent over other actions
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                GameState.PlayerHP = health;    //Saves the players HP at every step

                if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)  //makes sure the player has actually moved to its destination before initiating another move
                {
                   
                    Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                    //run animation
                    animator.SetBool("isMovingRight", true);
                  
                    GameState.PlayerTurn = false;

                    StartCoroutine(enemyLoop());

                    spawner.newEnemy();

                    //Heals every certain number of steps
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
                Attack();
                source.Play();
                GameState.PlayerTurn = false;

                StartCoroutine(enemyLoop());
            }
            //Animation work
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
    

    public override void OnDeath()
    {
        deathScreen.enabled = !deathScreen.enabled;
        GameState.PlayerHP = MAX_HEALTH / 2;    //Saves half their max HP to be restarted with
        GameState.PlayerXP = 0; //Removes their excess XP, no level penalty

        gameObject.SetActive(false);
    }

    //Called after every player turn
    IEnumerator enemyLoop()
    {
        yield return new WaitForSeconds(turnDelay/Speed);   //Has a set time after the player
        //Debug.Log("Time Delay: " + turnDelay/Speed);
        for (int i = 0; i < GameState.NPCs.Count; i++)
        {
            GameState.NPCs[i].NPCTurn();    //NPCs move first, and if nearby will also have a time inbetween each movement
            if (Vector3.Distance(PlayerActions.player.transform.position, GameState.NPCs[i].NPCLocation()) < 5)
            {
                yield return new WaitForSeconds(turnDelay / Speed);
            }

        }
        Debug.Log(GameState.Enemies.Count);
        for (int i = 0; i < GameState.Enemies.Count; i++)
        {
            GameState.Enemies[i].NPCTurn();     //enemies move next, and if nearby will also have a time inbetween each movement
            if (Vector3.Distance(PlayerActions.player.transform.position, GameState.Enemies[i].NPCLocation()) < 5)
            {
                yield return new WaitForSeconds(turnDelay/Speed);
            }
            
        }
        
        //After all moves have been made, the player will be allowed to make their next move
        GameState.PlayerTurn = true;
    }

    //Frame work for a chest/dropped items mechanic, not in use
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack3"))
        {
            GameState.UnlockAttack3 = true;
            Attack3UI.SetActive(true);
        }
    }
}