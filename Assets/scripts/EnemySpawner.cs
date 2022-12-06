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

    //public bool DEATH = false;

    //Specify width and height of the spawner, starting from the origin at the bottom left corner. 

    public int width = 5;
    public int height = 7;

    void Start()
    {
        type2SpawnRate += type1SpawnRate;
        type3SpawnRate += type2SpawnRate;
        type4SpawnRate += type3SpawnRate;
    }

    void Update()
    {
        
    }


    public void newEnemy() {
        
        Vector3 enemyDrop = new Vector3(Mathf.Round(Random.Range(0, width)), Mathf.Round(Random.Range(0, height)), 0);
        enemyDrop += transform.position;


        if(!Physics2D.OverlapCircle(enemyDrop, .2f, WhatStopsSpawning) && Random.Range(0, 100) < chanceToSpawn && GameState.Enemies.Count < maxEnemies)
        {
            int enemyType = (Random.Range(0, 99));
            
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
            //Debug.Log("Spawning new enemy");

            //GameState.EnemyCount++;
        }
    }

    public void DEATH() {
        Debug.Log("Du Duh Du Dun");

        //For now just have it increase max and spawn rate
        maxEnemies *= 2;
        chanceToSpawn += 15;
    }
    /*
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        
        //yield return new WaitForSeconds(interval);
        
        //GameObject newEnemy = Instantiate(enemy, transform.position + new Vector3(Mathf.Round(Random.Range(0, width)), Mathf.Round(Random.Range(0, height)), 0), Quaternion.identity);
        //GameState.EnemyCount++;
        //StartCoroutine(spawnEnemy(interval, enemy));
        
    }*/
}