using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool PlayerTurn = true;
    public int turnCount = 1;
    public static int SpeedFactor = 10;

    //This gets called once at start, but there shouldn't be much to do here?
    void Start()
    {

    }

    //This gets called every frame
    void Update()
    {

    }

    //Debug.Log("Currently player turn");
    //WaitWhile(PlayerTurn == true);

    //if the player moves or attacks, set PlayerTurn to false

    //turnCount++;

    //Debug.Log("Currently enemy turn");
    //WaitUntil(PlayerTurn == false);

    //let all enemies do their action, then set PlayerTurn to true
    //presumably this would be a for loop that goes through each enemy

    //turnCount++

}