using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayerButton : MonoBehaviour
{

    public Button yourButton;
    GameObject GameController;

    [SerializeField]
    GameObject customRoundNumber;

    private void Awake()
    {
        GameController = GameObject.Find("GameController");
    }

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Debug.Log("You have clicked the button!");
        SceneManager.GetActiveScene();
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        int level = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("TestMapUI");
    }
}
