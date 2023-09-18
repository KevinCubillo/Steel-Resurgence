using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> powers;

    [SerializeField]
    GameObject ResourceDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateCard() {
        GameObject power = powers[Mathf.FloorToInt(Random.value * powers.Count)];
        ResourceDisplay.SendMessage("setPower", power);
    }
}
