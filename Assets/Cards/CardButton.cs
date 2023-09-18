using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{

    public Button yourButton;
    public GameObject power;
    public Image fillImage;

    float fillRate = 0;
    float currFill = 0;

    [SerializeField]
    GameObject PowerGenerator;

    [SerializeField]
    GameObject resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(ActivatePower);
    }

    // Update is called once per frame
    void Update()
    {
        if (currFill > 0 && power == null) {
            currFill -= fillRate * Time.deltaTime;
            fillImage.fillAmount = Mathf.Max(currFill, 0);
        }
        if (currFill <= 0 && power != null) {
            fillImage.fillAmount = 1;
            currFill = 1;
        }
    }

    void ActivatePower() {
        if (power == null)
            return;
        GameObject newPower = Instantiate(power, PowerGenerator.transform);
        power = null;

        currFill = 1;
        if (newPower.GetComponent<SlowPower>() != null)
            fillRate = 1.0f / newPower.GetComponent<SlowPower>().duration;
        else if (newPower.GetComponent<BoostScrapPower>() != null)
            fillRate = 1.0f / newPower.GetComponent<BoostScrapPower>().duration;
        else if (newPower.GetComponent<WeakenEnemyPower>() != null)
            fillRate = 1.0f / newPower.GetComponent<WeakenEnemyPower>().duration;
        else
            fillRate = 1f / 20f;
        fillImage.fillAmount = 1;

        ResourceMessage rm = new ResourceMessage();
        rm.name = "Cooldown";
        rm.value = 0;
        resourceManager.SendMessage("resetResource", rm);
    }
}
