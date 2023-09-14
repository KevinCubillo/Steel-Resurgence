using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;

    [SerializeField]
    GameObject ActiveUI;

    [SerializeField]
    GameObject InactiveUI;

    [SerializeField]
    float timeScale;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = pauseButton.GetComponent<Button>();
        btn.onClick.AddListener(Pause);
    }

    // Update is called once per frame
    private void Pause()
    {
        ActiveUI.SetActive(false);
        InactiveUI.SetActive(true);
        Time.timeScale = timeScale;
    }
}
