using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    //public EnemyAction directional;

    private Transform target;
    private Vector3 fakePoint;
    private float Heading;
    private float newHeading;
    private float changeHeading;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerActions.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null){
            target = PlayerActions.player.transform;
        }
        float XDistance = target.position.x - transform.position.x;
        float YDistance = target.position.y - transform.position.y;

        float MoveVertical = 1;
        float MoveHorizontal = 1;

        float XDirection = 0;
        float YDirection = 0;

        if (XDistance != 0)
            XDirection = Mathf.Abs(XDistance) / XDistance;
       
        if (YDistance != 0)
            YDirection = Mathf.Abs(YDistance) / YDistance;

        if (Mathf.Abs(XDistance) > 1.5 * Mathf.Abs(YDistance))
        {
            MoveVertical = 0;
        }
        if (Mathf.Abs(XDistance) * 1.5 < Mathf.Abs(YDistance))
        {
            MoveHorizontal = 0;
        }

        float targetX = XDirection * MoveHorizontal;
        float targetY = YDirection * MoveVertical;


        fakePoint = transform.position + new Vector3(targetX, targetY, 0);
        newHeading = Mathf.Atan2(fakePoint.y - transform.position.y, fakePoint.x - transform.position.x);
        
        newHeading = Mathf.Rad2Deg * newHeading;
        changeHeading = newHeading - Heading;
        transform.RotateAround(transform.position, Vector3.forward, changeHeading);
        Heading = newHeading;
        //}
    }
}
