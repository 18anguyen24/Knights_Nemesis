using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;


public class ObjectCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hit detected");
        rb.velocity = new Vector2(0.0f, 0f);
    }
}