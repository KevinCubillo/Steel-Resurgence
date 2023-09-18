using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text DayLabel;

    [SerializeField]
    TMP_Text LifesLabel;

    [SerializeField]
    TMP_Text ScrapLabel;

    [SerializeField]
    TMP_Text CardName;

    [SerializeField]
    TMP_Text CooldownText;

    [SerializeField]
    TMP_Text CardDesc;

    [SerializeField]
    GameObject CardImage;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDay(int day) {
        DayLabel.text = "" + day;
    }
    public void setLifes(int lifes)
    {
        LifesLabel.text = "" + lifes;
    }
    public void setScrap(int scrap)
    {
        ScrapLabel.text = "" + scrap;
    }

    public void setCooldown(int totalSeconds)
    {
        int seconds = totalSeconds % 60;
        int minutes = (totalSeconds-seconds) / 60;
        CooldownText.text = minutes + ":" + seconds;
    }

    public void setPower(GameObject power) {
        CardPower p = power.GetComponent<CardPower>();
        CardImage.GetComponent<Button>().image.sprite = p.image;
        CardName.text = p.DisplayName;
        CardDesc.text = p.Description;
        CardImage.GetComponent<CardButton>().power = power;
    }
}
