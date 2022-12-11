using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    
    private Vector3 fakePoint;
    private float Heading;
    private float newHeading;
    private float changeHeading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Allows the players pointer to move regardless of whether its the players turn or not
    void Update()
    {
        fakePoint = transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        newHeading = Mathf.Atan2(fakePoint.y - transform.position.y, fakePoint.x - transform.position.x);
        
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            newHeading = Mathf.Rad2Deg * newHeading;
            changeHeading = newHeading - Heading;
            transform.RotateAround(transform.position, Vector3.forward, changeHeading);
            Heading = newHeading;
        }
    }
}
