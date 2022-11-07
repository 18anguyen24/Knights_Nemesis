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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TitleScreen.enabled = !TitleScreen.enabled;
            Controls.enabled = !Controls.enabled;
        }
        if (Controls.enabled == true && Input.GetKeyDown(KeyCode.K))
        //if (Input.GetKeyDown(KeyCode.K))
        {
            orig.enabled = !orig.enabled;
            title.enabled = !title.enabled;
        }
    }
}
