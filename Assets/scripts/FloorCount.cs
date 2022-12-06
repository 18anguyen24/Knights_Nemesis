using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorCount : MonoBehaviour
{
    public TextMeshProUGUI FloorIndicator;
    // Start is called before the first frame update
    void Start()
    {
        FloorIndicator.text = "Floor:\n" + GameState.FloorNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
