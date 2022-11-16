using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        UnitController uc = other.GetComponent<UnitController>();
        if (uc != null)
        {
            uc.Damage(damage);
            //Destroy(gameObject);
        }

        /*
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.Damage(damage);
        }*/
    }
}