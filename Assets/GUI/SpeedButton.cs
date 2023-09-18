using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    public Button button;

    [SerializeField]
    [Range(0,10)]
    float targetSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(SetGameSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGameSpeed() {
        Time.timeScale = targetSpeed;
    }
}
