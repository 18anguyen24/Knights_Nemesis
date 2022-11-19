using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image TitleScreen;
    public Image Controls;
    public Canvas orig;
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
            Controls.enabled = !Controls.enabled;
            onTitle = !onTitle;
            if(orig.enabled)
            {
                orig.enabled = !orig.enabled;
            }
        }
        if (Controls.enabled == true && Input.GetKeyDown(KeyCode.K))
        {
            player.GetComponent<PlayerActions>().enabled = !player.GetComponent<PlayerActions>().enabled;
            orig.enabled = !orig.enabled;
            title.enabled = !title.enabled;
        }
    }
}
