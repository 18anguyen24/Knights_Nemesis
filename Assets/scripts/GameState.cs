using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    
    public static bool PlayerTurn = true;
    
    public static int SpeedFactor = 5;

    public static List<NPCInterface> Enemies = new List<NPCInterface>();
    public static List<NPCInterface> NPCs = new List<NPCInterface>();

    //Handles player stats to be maintained between levels
    public static int PlayerHP = 0;
    public static float PlayerXP = 0;
    public static int PlayerLevel = 10;
    public static float XPtoLevel = 50;
    public static bool UnlockAttack3 = true;

    public static int FloorNumber = 0;

    

    //This gets called once at start, but there shouldn't be much to do here?
    void Start()
    {

    }

    //Clears these variables inbetween levels or while restarting so the player can continue
    public static void clear() {
        Enemies.Clear();
        NPCs.Clear();
        PlayerTurn = true;
    }

   
}