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

    protected int Speed = 1;

    public GameObject damageText;




    [SerializeField]
    public int health = 100;
    public int MAX_HEALTH;

    [SerializeField]
    public float XPDropped;

    //contain variable for stats (attack, defense, xp etc...)



    // Start is called before the first frame update
    void Start()
    {
        //MAX_HEALTH = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move(float X, float Y) {
        float AllowVertical = 1;
        float AllowHorizontal = 1;
        float AllowDiagonal = 1;

        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, Y, 0), .2f, WhatStopsMovement))
        {
            //Debug.Log("Blocked from moving diagonal");
            AllowDiagonal = 0;
        }
        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(X, 0f, 0), .2f, WhatStopsMovement))
        {
            //Debug.Log("Blocked from moving horizontal");
            AllowHorizontal = 0;
        }
        if (Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0f, Y, 0), .2f, WhatStopsMovement))
        {
            //Debug.Log("Blocked from moving vertical");
            AllowVertical = 0;
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

    public virtual void Dash(GameObject objectDashing, GameObject objectDashedThrough)
    {
        movePoint.transform.position = Vector2.MoveTowards(objectDashing.transform.position, 
                    objectDashedThrough.transform.position, Time.deltaTime * moveSpeed * 10);
    }

    public virtual void Dash2(GameObject unit)
    {
        Debug.Log(unit.transform.position - unit.transform.forward * 5);
        Physics2D.IgnoreCollision(unit.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Vector3 save = unit.transform.position;
        movePoint.transform.position = unit.transform.position - movePoint.transform.position + save - new Vector3(0, 1.5f);
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
