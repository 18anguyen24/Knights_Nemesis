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

    public static List<EnemyInterface> Enemies = new List<EnemyInterface>();

    //This gets called once at start, but there shouldn't be much to do here?
    void Start()
    {

    }

    public static void clear() {
        PlayerTurn = true;
        EnemyCount = 0;
        Enemies.Clear();
    }

   
}