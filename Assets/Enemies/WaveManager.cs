using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int finishedPoolers = 0;
    private int currentSpawnerIndex = 0;
    GameObject ResourceController;
    GameObject DayController;

    [SerializeField]
    public GameObject[] Spawners;

    [SerializeField]
    [Min(1)]
    [Header("How many spawners are active at a time.")]
    int concurrentPaths;

    [SerializeField]
    int delayBetweenWaves;

    private void Awake()
    {
        //Set the enemy pooler of this waves as this object
        EnemyPooler pooler;
        for (int i = 0; i < Spawners.Length; i++) {
            pooler = Spawners[i].GetComponent<EnemyPooler>();
            pooler.WaveController = gameObject;
        }

        ResourceController = GameObject.Find("ResourceController");
        DayController = GameObject.Find("DaysController");
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = currentSpawnerIndex; i < currentSpawnerIndex + concurrentPaths; i++)
            Spawners[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaveFinished(GameObject pooler) {
        finishedPoolers++;
        pooler.SetActive(false);
        if (finishedPoolers >= concurrentPaths) {
            finishedPoolers = 0;

            //Stop current spaawners
            for (int i = currentSpawnerIndex; i < currentSpawnerIndex + concurrentPaths; i++)
                Spawners[i].SetActive(false);


            currentSpawnerIndex += concurrentPaths;

            

            //Start next spawners/wave
            if (currentSpawnerIndex < Spawners.Length)
            {
                yield return new WaitForSeconds(delayBetweenWaves);
                for (int i = currentSpawnerIndex; i < currentSpawnerIndex + concurrentPaths; i++)
                    Spawners[i].SetActive(true);
            }
            else {
                DayController.SendMessage("WavesFinished");
            }
        }
    }
}
