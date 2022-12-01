using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool PlayerTurn = true;
    //public static bool PlayerDidAction = false;
    //public int turnCount = 1;
    public static int SpeedFactor = 5;
    public static int EnemyCount;

    public static List<NPCInterface> Enemies = new List<NPCInterface>();
    public static List<NPCInterface> NPCs = new List<NPCInterface>();

    public static float PlayerXP = 0;
    public static int PlayerLevel = 0;
    public static float XPtoLevel = 50;

    //This gets called once at start, but there shouldn't be much to do here?
    void Start()
    {

    }

    public static void clear() {
        PlayerTurn = true;
        EnemyCount = 0;
        Enemies.Clear();
        NPCs.Clear();
    }

   
}