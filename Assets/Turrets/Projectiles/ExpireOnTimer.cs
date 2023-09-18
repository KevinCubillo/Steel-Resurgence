using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpireOnTimer : MonoBehaviour
{
    [SerializeField]
    float time;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time) {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
}
