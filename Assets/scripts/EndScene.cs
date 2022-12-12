using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class EndScene : MonoBehaviour
{
    public TextMeshProUGUI text;  //Add reference to UI Text here via the inspector
    private float timeToAppear = 2f;
    private float timeWhenAppear;
    public TextMeshProUGUI text2;
    //Call to enable the text, which also sets the timer
    void Start()
    {
        text.enabled = true;
        text2.enabled = false;
        timeWhenAppear = Time.time + timeToAppear;
    }
    
    //We check every frame if the timer has expired and the text should disappear
    void Update()
    {
        if (text.enabled && (Time.time >= timeWhenAppear))
        {
            text2.enabled = true;
        }
    }
}