using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage;
    public bool PlayerAttack = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        UnitController uc = other.GetComponent<UnitController>();
        if (uc != null)
        {
            if (PlayerAttack == true) {
                int damageDealt = Mathf.RoundToInt(damage * (float)(1 + .05 * GameState.PlayerLevel));
                uc.Damage(damageDealt);
                //uc.Damage(damage * Mathf.RoundToInt((float)(1 + .05 * GameState.PlayerLevel)));
                Debug.Log("damage dealt: " + damageDealt);
            } else {
                uc.Damage(damage);
                Debug.Log("damage dealt: " + damage);
            }
            
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