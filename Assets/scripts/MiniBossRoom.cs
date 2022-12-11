using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossRoom : MonoBehaviour
{
    public GameObject Wall;
    public GameObject MiniBoss;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Locks the player into the room, then destroys itself so the player cant be locked in later
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Wall.SetActive(true);
            MiniBoss.SetActive(true);
            Destroy(this);
        }
    }
}
