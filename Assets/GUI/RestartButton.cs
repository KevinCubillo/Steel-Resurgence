using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public Button yourButton;
    // Start is called before the first frame update

    //[SerializeField]
    string currentLevelName;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Restart);
        currentLevelName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Restart()
    {
            SceneManager.LoadScene(currentLevelName);
    }
}
