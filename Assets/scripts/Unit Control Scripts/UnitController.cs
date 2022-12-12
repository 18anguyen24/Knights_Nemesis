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
    public bool attacking = false;
    protected float timeToAttack = 0.25f;
    protected float timer = 0f;

    protected int Speed = 1;

    public GameObject damageText;

    [SerializeField]
    public int health = 100;
    public int MAX_HEALTH;

    [SerializeField]
    public float XPDropped;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Takes in the number of tiles and tries to move to the tile at that location
    public virtual void Move(float X, float Y) {
        float AllowVertical = 1;
        float AllowHorizontal = 1;
        float AllowDiagonal = 1;

        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, Y, 0), .2f, WhatStopsMovement))
        {       
            AllowDiagonal = 0;
        }
        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, 0f, 0), .2f, WhatStopsMovement))
        {
           AllowHorizontal = 0;
        }
        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0f, Y, 0), .2f, WhatStopsMovement))
        {
            AllowVertical = 0;
        }

        
        float MotionX = X * AllowHorizontal;
        float MotionY = Y * AllowVertical;

        //Allows movement to slide along a wall, but if trying to move diagonal into a corner it wont pick a single direction
        if (Mathf.Abs(MotionX) == Mathf.Abs(MotionY) && AllowDiagonal == 0)
        {
            MotionX = 0;
            MotionY = 0;
        }

        movePoint.transform.position += new Vector3(MotionX, MotionY, 0);
        
    }


    public virtual void Dash2(GameObject unit)
    {

        float XDistance = unit.transform.position.x - transform.position.x;
        float YDistance = unit.transform.position.y - transform.position.y;

        if (XDistance != 0)
            XDistance = Mathf.Abs(XDistance) / XDistance;
       
        if (YDistance != 0)
            YDistance = Mathf.Abs(YDistance) / YDistance;

        Vector3 goalPoint = movePoint.transform.position + new Vector3(2 * XDistance, 2 * YDistance, 0);
        movePoint.transform.position = goalPoint;

        Debug.Log("TESTING DASH TURN");
    }

    protected void Attack()
    {
        attacking = true;
        activeAttack.SetActive(attacking);
    }


    public virtual void OnDeath()
    {

    }

    //Handles the damaging and healing for all units
    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        DamagePopup indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        indicator.SetDamageText(amount);

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
