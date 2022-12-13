using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBossUI : MonoBehaviour
{
    public GameObject player;
    public GameObject bossUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.activeSelf)
        {
            bossUI.SetActive(false);
        }
    }
}
