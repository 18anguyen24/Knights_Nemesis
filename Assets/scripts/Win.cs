using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public Image WinScreen;
    public Slider HealthBar;
    public Image PlayerIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Win"))
        {
            WinScreen.enabled = !WinScreen.enabled;
            //Debug.Log("WIN");
            Destroy(gameObject);
        }
    }
}
