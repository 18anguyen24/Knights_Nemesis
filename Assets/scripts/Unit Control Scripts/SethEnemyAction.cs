using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethEnemyAction : UnitController, NPCInterface
{
    
    public GameObject attackArea1;
    public GameObject attackArea2;

    public Transform Target;

    public float targetX;
    public float targetY;

    //private Variables
    private float XDistance;
    private float YDistance;

    private bool primed = false;
    private bool truant = true;

    
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [SerializeField] AudioSource audioSource1;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        health = health + GameState.FloorNumber * 7;
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
            animator.SetBool("attacking", true);
            timer += Time.deltaTime;

            if (timer >= timeToAttack/Speed)
            {
                timer = 0;
                attacking = false;
                activeAttack.SetActive(attacking);
                if (activeAttack == attackArea2) {
                    OnDeath();
                    XPDropped *= 2;
                }
                animator.SetBool("attacking", false);
            }

        }
        
    }

    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void NPCTurn()
    {
        if (Mathf.Abs(XDistance) < 2 && Mathf.Abs(YDistance) < 2 && health < (MAX_HEALTH/3)) 
        {
            if (primed == false)
            {
                Debug.Log("Priming: " + health);
                primed = true;
                animator.SetBool("primed", true);
                //animator.SetTrigger("up");
                source.Play();
            }
            else {
                activeAttack = attackArea2; //Sets to attack 2
                Attack();   //Attacks
                source.Play();
                //OnDeath();
            }
        }
        else if (Mathf.Abs(XDistance) <= 1 && Mathf.Abs(YDistance) <= 1)    //Checks if player is one tile away
        {
            activeAttack = attackArea1; //Sets to attack 1
            Attack();   //Attacks
            truant = true;
        }
        else
        {
            
            if (truant == false)
            {
                truant = true;
            }
            else
            {
                MoveEnemy();  //Moves
                truant = false;
            }
            
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

        if (Vector3.Distance(transform.position, movePoint.transform.position) <= .05f)//should be unnecessary with multiple enemies
        {
            

            animator.ResetTrigger("Down");
            animator.ResetTrigger("Side");
            animator.ResetTrigger("Up");

            if (YDistance < 0 - Mathf.Abs(XDistance))
            {
               
                animator.SetTrigger("Down");
                
            }
            else if (Mathf.Abs(XDistance) >= 0 + .75* Mathf.Abs(YDistance))
            {
                
                animator.SetTrigger("Side");
               
                if (XDirection < 0) {
                    spriteRenderer.flipX = true;
                }
            }
            else {
               
                animator.SetTrigger("Up");
                
            }
            Move(XDirection, YDirection);

            if (XDistance >= 0)
            {
                spriteRenderer.flipX = false;
            }
        }


    }

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
