using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelData : MonoBehaviour
{
    int currentDay;

    [SerializeField]
    int daysUntilEnd;

    private void Awake()
    {
        currentDay = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void advanceDay() {
        currentDay++;

        //change display

        if (currentDay > daysUntilEnd) { 
            
            //change map, create selectable upgrade
        }
    }
}
