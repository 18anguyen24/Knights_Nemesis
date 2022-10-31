using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public PlayerActions action;
    private Vector3 fakePoint;
    public float Heading;
    public float newHeading;
    public float changeHeading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fakePoint = transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        newHeading = Mathf.Atan2(fakePoint.y - transform.position.y, fakePoint.x - transform.position.x);
        Debug.Log("The heading is " + newHeading);
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            newHeading = Mathf.Rad2Deg * newHeading;
            changeHeading = newHeading - Heading;
            transform.RotateAround(transform.position, Vector3.forward, changeHeading);
            Heading = newHeading;
        }
    }
}
