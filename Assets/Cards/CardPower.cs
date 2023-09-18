using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardPower : MonoBehaviour
{
    [SerializeField]
    public float duration;

    public string DisplayName;
    public string Description;
    public Sprite image;

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            PowerFinished();
            Destroy(gameObject);
        }
    }

    abstract public void PowerFinished();
}
