using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakenEnemyPower : CardPower
{

    [SerializeField]
    float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        GlobalValues.EnemyReceivedDamageMultiplier *= multiplier;
    }

    public override void PowerFinished()
    {
        GlobalValues.EnemyReceivedDamageMultiplier *= 1.0f / multiplier;
    }
}
