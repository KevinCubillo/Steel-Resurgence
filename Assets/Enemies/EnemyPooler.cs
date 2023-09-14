using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPooler : MonoBehaviour{ 
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> pool;
    private GameObject poolContainer;

    private void Awake(){
        pool = new List<GameObject>();
        poolContainer = new GameObject($"Pool - {enemyPrefab.name}");
        CreatePooler();
    }
    private void CreatePooler(){
       for (int i = 0; i < poolSize; i++){
        pool.Add(CreateEnemy());     
       } 
    }

    private GameObject CreateEnemy(){
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.SetParent(poolContainer.transform);
        enemy.SetActive(false);
        return enemy;
    }

    public GameObject GetEnemyFromPool(){
        for (int i = 0; i < pool.Count; i++){
            if (pool[i] != null && !pool[i].activeInHierarchy){
                return pool[i];
            }
        }
        return CreateEnemy();   
    }

    public static void ReturnToPool(GameObject enemy){
        enemy.SetActive(false);
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject enemy, float delay){
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);
    }
}
