using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool PlayerTurn = true;
    //public static bool PlayerDidAction = false;
    //public int turnCount = 1;
    public static int SpeedFactor = 10;
    public static int EnemyCount;

    

    //This gets called once at start, but there shouldn't be much to do here?
    void Start()
    {

    }

    //This gets called every frame
    public static void PlayerMoved()
    {
        //Debug.Log("Enemy turn");

            for (int i = 0; i < EnemyCount; i++) {
                GameObject EnemyPoint = GameObject.FindWithTag("Moved");
                EnemyPoint.gameObject.tag = "Unmoved";

            }
        //EnemySpawner.newEnemy();
        //Debug.Log("Player turn");
        PlayerTurn = true;
    }
    
    //if the player moves or attacks, set PlayerTurn to false

    

    

    //let all enemies do their action, then set PlayerTurn to true
    //presumably this would be a for loop that goes through each enemy

    //turnCount++

}