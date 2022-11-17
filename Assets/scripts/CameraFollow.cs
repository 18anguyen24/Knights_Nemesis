using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(0, 0, -5);
    }
}