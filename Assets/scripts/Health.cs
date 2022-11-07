using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;

    private int MAX_HEALTH = 100;

    public GameObject player;

    public HealthBar healthbar;

    public Image ded;

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
        if(gameObject.tag == "Player")
        {
            ded.enabled = !ded.enabled;
            Destroy(gameObject);
            //SceneManager.LoadScene(0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}