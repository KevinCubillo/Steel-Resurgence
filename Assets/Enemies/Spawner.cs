using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

[Header("Settings")]
[SerializeField] private int enemyCount = 10;
[SerializeField] private GameObject testGo;

[Header("Fixed Delay")]
[SerializeField] private float delayBtwSpawns;

private float spawnTimer;
private int enemySpawned;
private EnemyPooler pooler;

    void Start(){
        pooler = GetComponent<EnemyPooler>();    
    }

    void Update(){
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0){
            spawnTimer = delayBtwSpawns;
            if (enemySpawned < enemyCount){
                enemySpawned++;
                SpawnEnemy(); 
            }   
        } 
    }

    private void SpawnEnemy(){
        GameObject enemy = pooler.GetEnemyFromPool();
        enemy.SetActive(true);
       
    }
}