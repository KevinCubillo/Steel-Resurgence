using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    float cooldownTimer;

    [SerializeField]
    NumResource Lives;

    [SerializeField]
    NumResource Scrap;

    [SerializeField]
    NumResource CardCooldown;

    [SerializeField]
    NumResource Days;

    [SerializeField]
    GameObject displayManager;

    //[SerializeField]
    GameObject powerGenerator;

    [SerializeField]
    GameObject GameOverUI;

    private void Awake()
    {
        Lives.reset();
        Scrap.reset();
        CardCooldown.reset();
        Days.reset();

        displayManager.SendMessage("setLifes", Lives.value());
        displayManager.SendMessage("setScrap", Scrap.value());
        displayManager.SendMessage("setDay", Days.value());
        displayManager.SendMessage("setCooldown", CardCooldown.value());

        powerGenerator = GameObject.Find("PowerGenerator");

        
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= 1 && CardCooldown.value() > 0)
            {
                cooldownTimer = 0;
                CardCooldown.sub(1);
                displayManager.SendMessage("setCooldown", CardCooldown.value(), SendMessageOptions.DontRequireReceiver);
                if (CardCooldown.value() == 0)
                {
                    //Generate Card
                    powerGenerator.SendMessage("GenerateCard");
                }
            }
        }
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
                displayManager.SendMessage("setCooldown", CardCooldown.value());
                break;
            case "Day":
                Days.add(message.value);
                displayManager.SendMessage("setDay", Days.value());
                break;
        }
    }

    void consumeResource(ResourceMessage message) {
        switch (message.name) {
            case "Lifes":
                Lives.sub(message.value);
                displayManager.SendMessage("setLifes", Lives.value());
                if (Lives.value() == 0 && !GameOverUI.activeInHierarchy) {
                    GameOverUI.SetActive(true);
                    GameObject.Find("UI").SetActive(false);
                }
                break;
            case "Scrap":
                Scrap.sub(message.value);
                displayManager.SendMessage("setScrap", Scrap.value());
                break;
            case "Cooldown":
                CardCooldown.sub(message.value);
                displayManager.SendMessage("setCooldown", CardCooldown.value());
                break;
            case "Day":
                Days.sub(message.value);
                displayManager.SendMessage("setDay", Days.value());
                break;
        }
    }

    void resetResource(ResourceMessage message)
    {
        switch (message.name)
        {
            case "Lifes":
                Lives.reset();
                displayManager.SendMessage("setLifes", Lives.value());
                break;
            case "Scrap":
                Scrap.reset();
                displayManager.SendMessage("setScrap", Scrap.value());
                break;
            case "Cooldown":
                CardCooldown.reset();
                displayManager.SendMessage("setCooldown", CardCooldown.value());
                break;
            case "Day":
                Days.reset();
                displayManager.SendMessage("setDay", Days.value());
                break;
        }
    }

    void CanBuy(buyMessage message) {
        message.source.SendMessage(message.callbackMessage, message.cost <= Scrap.value());
    }

    void GetResourceValue(GetResourceMessage message)
    {
        switch (message.name)
        {
            case "Lifes":
                message.source.SendMessage(message.callbackMessage, Lives.value());
                break;
            case "Scrap":
                message.source.SendMessage(message.callbackMessage, Scrap.value());
                break;
            case "Cooldown":
                message.source.SendMessage(message.callbackMessage, CardCooldown.value());
                break;
            case "Day":
                message.source.SendMessage(message.callbackMessage, Days.value());
                break;
        }
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

struct GetResourceMessage
{
    public string name;
    public GameObject source;
    public string callbackMessage;
}