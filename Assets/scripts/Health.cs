using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    public GameObject player;

    public HealthBar healthbar;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            healthbar.SetMaxHealth(MAX_HEALTH);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            //Heal(10);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;
        if (gameObject.tag == "Player")
        {
            healthbar.SetHealth(this.health);
        }

        if (this.health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = (health + amount) > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }


    private void Die()
    {
        Debug.Log("I am Dead!");
        Destroy(gameObject);
        if(gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}