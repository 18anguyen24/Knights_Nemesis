using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    
    //Cumulative chance, each enemy type should be increasingly more likely
    public GameObject enemyType1;
    public int type1SpawnRate;
    public GameObject enemyType2;
    public int type2SpawnRate;
    public GameObject enemyType3;
    public int type3SpawnRate;
    public GameObject enemyType4;
    public int type4SpawnRate;
    public GameObject enemyType5;
    //private int type5SpawnRate = 100;

    public LayerMask WhatStopsSpawning;

    public int maxEnemies;
    public int chanceToSpawn;

    

    //Specify width and height of the spawner, starting from the origin at the bottom left corner. 

    public int width = 5;
    public int height = 7;

    void Start()
    {
        type2SpawnRate += type1SpawnRate;
        type3SpawnRate += type2SpawnRate;
        type4SpawnRate += type3SpawnRate;
        //5th isnt needed as its the default
    }

    void Update()
    {
        
    }


    public void newEnemy() {
        
        //decides on a theoretical point for a new enemy to be spawned
        Vector3 enemyDrop = new Vector3(Mathf.Round(Random.Range(0, width)), Mathf.Round(Random.Range(0, height)), 0);
        enemyDrop += transform.position;

        //Multiple checks before spawning an enemy
            //Checks if the space is valid
            //Checks the randomizer to see if an enemy might be spawned
            //Makes sure the enemy is a couple tiles from the player
            //Checks that there is space for another enemy
        if(!Physics2D.OverlapCircle(enemyDrop, .2f, WhatStopsSpawning) && Random.Range(0, 100) < chanceToSpawn && Vector3.Distance(enemyDrop, PlayerActions.player.transform.position) > 3 && GameState.Enemies.Count < maxEnemies)
        {
            //If an enemy is going to be spawned a second randomizer is used to determine which enemy (currently each level spawns a max of 5 enemy types)
            int enemyType = (Random.Range(0, 100));
            
            if (enemyType < type1SpawnRate) {
                GameObject newEnemy = Instantiate(enemyType1, enemyDrop, Quaternion.identity);
            } else if (enemyType < type2SpawnRate)
            {
                GameObject newEnemy = Instantiate(enemyType2, enemyDrop, Quaternion.identity);
            }
            else if (enemyType < type3SpawnRate)
            {
                GameObject newEnemy = Instantiate(enemyType3, enemyDrop, Quaternion.identity);
            }
            else if (enemyType < type4SpawnRate)
            {
                GameObject newEnemy = Instantiate(enemyType4, enemyDrop, Quaternion.identity);
            }
            else 
            {
                GameObject newEnemy = Instantiate(enemyType5, enemyDrop, Quaternion.identity);
            }
            
        }
    }

    //Punishment for killing the NPC, the max enemies will be increased, as will the spawnrate
    public void DEATH() {
       
        maxEnemies *= 2;
        chanceToSpawn += 15;
    }
   
}