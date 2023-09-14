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
    Image CardImage;


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
}
