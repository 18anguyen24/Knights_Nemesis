using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorCount : MonoBehaviour
{
    public TextMeshProUGUI FloorIndicator;
    
    //duh
    void Start()
    {
        FloorIndicator.text = "Floor:\n" + GameState.FloorNumber;
    }

    
    void Update()
    {
        
    }
}
