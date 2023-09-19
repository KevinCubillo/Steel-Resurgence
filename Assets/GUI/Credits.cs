using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject activeUI;

    [SerializeField]
    GameObject CreditsUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            CreditsUI.SetActive(true);
            activeUI.SetActive(false);
        }
    }
}
