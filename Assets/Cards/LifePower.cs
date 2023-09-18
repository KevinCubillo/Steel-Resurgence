using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePower : CardPower
{
    [SerializeField]
    int amount;
    // Start is called before the first frame update
    void Start()
    {
        GameObject resources = GameObject.Find("ResourceController");
        if (resources != null)
        {
            ResourceMessage message = new ResourceMessage();
            message.name = "Lifes";
            message.value = amount;
            resources.SendMessage("giveResource", message);
        }
    }

    public override void PowerFinished()
    {
       
    }
}
