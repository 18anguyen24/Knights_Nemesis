using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    //Bringing in more previous info to simplify Player and different enemy Scripts
    public float moveSpeed = 5f;
    public GameObject movePoint;
    public LayerMask WhatStopsMovement;
    
    protected GameObject activeAttack;
    protected bool attacking = false;
    protected float timeToAttack = 0.25f;
    protected float timer = 0f;

    protected float Speed;




    [SerializeField]
    public int health = 100;
    public int MAX_HEALTH = 100;

    //contain variable for stats (attack, defense, xp etc...)



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move(float X, float Y) {
        float AllowVertical = 1;
        float AllowHorizontal = 1;
        float AllowDiagonal = 1;


        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, 0f, 0), .2f, WhatStopsMovement))
        {
            AllowHorizontal = 0;
        }
        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0f, Y, 0), .2f, WhatStopsMovement))
        {
            AllowVertical = 0;
        }

        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, Y, 0), .2f, WhatStopsMovement))
        {
            AllowDiagonal = 0;
        }
        float MotionX = X * AllowHorizontal;
        float MotionY = Y * AllowVertical;

        if (Mathf.Abs(MotionX) == Mathf.Abs(MotionY) && AllowDiagonal == 0)
        {
            MotionX = 0;
            MotionY = 0;
        }

        movePoint.transform.position += new Vector3(MotionX, MotionY, 0);
        
    }

    protected void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }


    public virtual void OnDeath()
    {

    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }
}
