using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image TitleScreen;
    public GameObject orig;
    public Canvas title;
    public GameObject player;
    public bool onTitle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onTitle && Input.GetKeyDown(KeyCode.Space))
        {
            TitleScreen.enabled = !TitleScreen.enabled;
            onTitle = !onTitle;
            player.GetComponent<PlayerActions>().enabled = !player.GetComponent<PlayerActions>().enabled;
            orig.SetActive(true);
        }
    }
}
