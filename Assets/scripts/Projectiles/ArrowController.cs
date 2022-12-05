using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrowRef;
    public Transform ArrowLauncher;


    // Start is called before the first frame update
    void Start()
    {
        Coroutine startSpawn = StartCoroutine(ArrowRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ArrowRoutine()
    {
        while(true)
        {
            Instantiate(arrowRef, ArrowLauncher.position, ArrowLauncher.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}
