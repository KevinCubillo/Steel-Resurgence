using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    [SerializeField]
    NumResource Lives;

    [SerializeField]
    NumResource Scrap;

    [SerializeField]
    NumResource CardCooldown;

    [SerializeField]
    GameObject displayManager;

    private void Awake()
    {
        Lives.reset();
        Scrap.reset();
        CardCooldown.reset();

        displayManager.SendMessage("setLifes", Lives.value());
        displayManager.SendMessage("setScrap", Scrap.value());
    }

    [SerializeField]
    int bonusEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void giveResource(ResourceMessage message)
    {
        switch (message.name)
        {
            case "Lifes":
                Lives.add(message.value);
                displayManager.SendMessage("setLifes", Lives.value());
                break;
            case "Scrap":
                Scrap.add(message.value);
                displayManager.SendMessage("setScrap", Scrap.value());
                break;
            case "Cooldown":
                CardCooldown.add(message.value);
                break;
        }
    }

    void consumeResource(ResourceMessage message) {
        switch (message.name) {
            case "Lifes":
                Lives.sub(message.value);
                displayManager.SendMessage("setLifes", Lives.value());
                break;
            case "Scrap":
                Scrap.sub(message.value);
                displayManager.SendMessage("setScrap", Scrap.value());
                break;
            case "Cooldown":
                CardCooldown.sub(message.value);
                break;
        }
    }

    void CanBuy(buyMessage message) {
        message.source.SendMessage(message.callbackMessage, message.cost <= Scrap.value());
    }
}


[System.Serializable]
public class NumResource
{
    [SerializeField]
    int minValue;

    [SerializeField]
    int maxValue;

    [SerializeField]
    int initialValue;

    int currentValue;

    NumResource(int minVa, int maxVa)
    {
        minValue = minVa;
        maxValue = maxVa;
        initialValue = 0;
        currentValue = 0;
    }

    NumResource(int minVa, int maxVa, int initialVal)
    {
        minValue = minVa;
        maxValue = maxVa;
        initialValue = initialVal;
        currentValue = initialVal;
    }

    public bool addIfPossible(int amount)
    {
        bool available = currentValue + amount <= maxValue;
        currentValue += available ? amount : 0;
        return available;
    }

    public bool subIfPossible(int amount)
    {
        bool available = currentValue - amount >= minValue;
        currentValue -= available ? amount : 0;
        return available;
    }

    public int add(int amount)
    {
        currentValue += amount;
        currentValue = currentValue > maxValue ? maxValue : currentValue;
        return currentValue;
    }

    public int sub(int amount)
    {
        currentValue -= amount;
        currentValue = currentValue < minValue ? minValue : currentValue;
        return currentValue;
    }

    public int set(int val)
    {
        currentValue = val;
        currentValue = currentValue > maxValue ? maxValue : currentValue;
        currentValue = currentValue < minValue ? minValue : currentValue;
        return val;
    }

    public int value()
    {
        return currentValue;
    }
    public int min()
    {
        return minValue;
    }
    public int max()
    {
        return maxValue;
    }
    public int initial()
    {
        return initialValue;
    }

    public int reset()
    {
        currentValue = initialValue;
        return initialValue;
    }
}

struct ResourceMessage {
    public string name;
    public int value;
}

struct buyMessage {
    public int cost;
    public GameObject source;
    public string callbackMessage;
}