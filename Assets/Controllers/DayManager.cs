using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    int currentWave = 0;
    bool finished;

    [SerializeField]
    public GameObject[] waves;

    [SerializeField]
    GameObject ResourceController;


    [SerializeField]
    string MapToLoad;

    // Start is called before the first frame update
    void Start()
    {
        waves[0].SetActive(true);
    }

    private void WavesFinished() {
        ResourceMessage message = new ResourceMessage();
        message.name = "Day";
        message.value = 1;
        ResourceController.SendMessage("giveResource", message);


        //Check if lifes > 0 to award upgrade
        GetResourceMessage lifesLeft = new GetResourceMessage();
        lifesLeft.callbackMessage = "LifesLeft";
        lifesLeft.name = "Lifes";
        lifesLeft.source = gameObject;
        ResourceController.SendMessage("GetResourceValue", lifesLeft);

        //Check if it's final day, if so, change map
        /*GetResourceMessage daysPassed = new GetResourceMessage();
        daysPassed.callbackMessage = "DaysPassed";
        daysPassed.name = "Day";
        daysPassed.source = gameObject;
        ResourceController.SendMessage("GetResourceValue", daysPassed);*/

        waves[currentWave].SetActive(false);
        Destroy(waves[currentWave]);

        currentWave++;

        if (waves.Length == currentWave)
        {
            SceneManager.LoadScene(MapToLoad);
            return;
        }
        else {
            waves[currentWave].SetActive(true);
        }

        //Reset lifes for the new day
        ResourceMessage message2 = new ResourceMessage();
        message2.name = "Lifes";
        message2.value = 0;
        ResourceController.SendMessage("resetResource", message2);
    }

    private void DaysPassed(int days) {
        if (days > waves.Length) {
            finished = true;
            SceneManager.LoadScene(MapToLoad);
        }
    }

    private void LifesLeft(int lifes) {
        if (lifes > 0) { 
            //Open upgrade menu
        }
    }
}
