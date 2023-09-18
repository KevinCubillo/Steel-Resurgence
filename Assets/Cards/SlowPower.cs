using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPower : CardPower
{

    [SerializeField]
    float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        GlobalValues.EnemySpeedMultiplier *= multiplier;
    }

    public override void PowerFinished()
    {
        GlobalValues.EnemySpeedMultiplier *= 1.0f / multiplier;
    }
}