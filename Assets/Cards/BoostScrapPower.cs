using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScrapPower : CardPower
{
    [SerializeField]
    float multiplier;
    

    // Start is called before the first frame update
    void Start()
    {
        GlobalValues.ScrapMultiplier *= multiplier;
    }

    public override void PowerFinished()
    {
        GlobalValues.ScrapMultiplier *= 1.0f / multiplier;
    }

}
