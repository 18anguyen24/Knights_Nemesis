using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Coroutine beginLife = StartCoroutine(LifeCycle());
        damage = 5;
    }
    // Update is called once per frame

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnitController uc = collision.GetComponent<UnitController>();
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Encounter");
            uc.Damage(damage);
            Destroy(gameObject);
        }

        
    }
    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
