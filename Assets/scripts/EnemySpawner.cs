using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float enemyInterval = 3.5f;

    //Specify width and height of the spawner, starting from the origin at the bottom left corner. Can just place a spawner per room, probably

    public float width = 5f;
    public float height = 7f;

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Mathf.Round(Random.Range(0, width)), Mathf.Round(Random.Range(0, height)), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}