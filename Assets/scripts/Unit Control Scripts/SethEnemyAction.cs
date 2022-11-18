using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SethEnemyAction : UnitController, EnemyInterface
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

    /*private GameObject activeAttack;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
    */


    // Start is called before the first frame update
    void Start()
    {
        MAX_HEALTH = health;
        GameState.Enemies.Add(this);

        GameObject PlayerMovePoint = GameObject.FindWithTag("PlayerLocation");
        Target = PlayerMovePoint.transform;
        
        movePoint.transform.parent = null;
     
        targetX = 0;
        targetY = 0;

        activeAttack = attackArea1;
    }

    // Update is called once per frame
    void Update()
    {
        XDistance = Target.position.x - transform.position.x;
        YDistance = Target.position.y - transform.position.y;

        if (movePoint.transform.tag == "Unmoved")
        {

            EnemyTurn();

            movePoint.transform.tag = "Moved";

        }

        
        float Speed = moveSpeed;

        if (Input.GetMouseButton(0))
        {
            Speed = Speed * GameState.SpeedFactor;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, Speed * Time.deltaTime);
        

        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                activeAttack.SetActive(attacking);
                if (activeAttack == attackArea2) {
                    OnDeath();
                }
            }

        }
        
    }

    public void GetEnemyLocation() { 
    
    }

    //Implement different Enemies controls here: this is the basic code for the current Enemy
    public void EnemyTurn()
    {
        if (Mathf.Abs(XDistance) < 2 && Mathf.Abs(YDistance) < 2 && health < (MAX_HEALTH/3)) 
        {
            if (primed == false)
            {
                Debug.Log("Priming: " + health);
                primed = true;
            }
            else {
                activeAttack = attackArea2; //Sets to attack 2
                Attack();   //Attacks
                
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
            Move(XDirection, YDirection);

            
        }


    }

    public override void OnDeath()
    {
        GameState.Enemies.Remove(this);
        Destroy(gameObject);
        Destroy(movePoint);
        

    }

    public Vector3 EnemyLocation()
    {
        return transform.position;
    }
}
