using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject launchPosition;
    public GameObject target;
    public GameObject player;

    public float speed = 10f;

    public Vector3 movePosition;

    private float launchPositionX;
    private float targetX;
    private float nextX;
    private float dist;
    private float baseY;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        launchPosition = GameObject.FindGameObjectWithTag("Launcher");
        target = GameObject.FindGameObjectWithTag("Receiver");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        launchPositionX = launchPosition.transform.position.x;
        targetX = target.transform.position.x;
        dist = targetX - launchPositionX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        //baseY = Mathf.Lerp(launchPosition.transform.position.y, target.transform.position.y, (nextX - launchPositionX) / dist);
        //height = 2 * (nextX - launchPositionX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX,/* baseY + height*/ 0, 0 /*transform.position.z*/);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;


        if (movePosition == target.transform.position)
        {
            Destroy(gameObject);
        }
    }

    public static Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }
}
