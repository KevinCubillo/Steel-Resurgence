using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPooler : MonoBehaviour{
    [SerializeField] private Path path;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> pool;
    private GameObject poolContainer;

    public GameObject WaveController;
    private bool finished;

    private void Awake(){
        pool = new List<GameObject>();
        poolContainer = new GameObject($"Pool - {enemyPrefab.name}");
        CreatePooler();
    }

    private void Update()
    {
        if (!finished && poolContainer.transform.childCount == 0) {
            WaveController.SendMessage("WaveFinished", gameObject, SendMessageOptions.DontRequireReceiver);
            finished = true;
        }
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
        enemy.GetComponent<Enemy>().SetPath(path);
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
        Destroy(enemy);
    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject enemy, float delay){
        yield return new WaitForSeconds(delay);
        enemy.SetActive(false);
    }

}
