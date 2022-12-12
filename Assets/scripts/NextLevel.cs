using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string current;
    public string nextLevel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Allows the player to restart at any point in a level
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameState.clear();
            SceneManager.LoadScene(current);

        }
    }

    //Lets the player move to the next level
    //Levels are manually set into the goals
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameState.FloorNumber++;
            GameState.clear();
            SceneManager.LoadScene(nextLevel);
        }
    }
}
