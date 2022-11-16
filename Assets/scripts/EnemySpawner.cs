using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;

    public LayerMask WhatStopsMovement;

    public int maxEnemies;
    public int chanceToSpawn;

    //Specify width and height of the spawner, starting from the origin at the bottom left corner. Can just place a spawner per room, probably

    public int width = 5;
    public int height = 7;

    void Start()
    {
        //newEnemy();
        //newEnemy();
        //newEnemy();
    }



    public void newEnemy() {
        
        Vector3 enemyDrop = new Vector3(Mathf.Round(Random.Range(0, width)), Mathf.Round(Random.Range(0, height)), 0);
        enemyDrop += transform.position;


        if(!Physics2D.OverlapCircle(enemyDrop, .2f, WhatStopsMovement) && Random.Range(0, 100) < chanceToSpawn && GameState.EnemyCount < maxEnemies)
        {
            //Debug.Log("Spawning new enemy");
            GameObject newEnemy = Instantiate(enemyPrefab, enemyDrop, Quaternion.identity);
            //GameState.EnemyCount++;
        }
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