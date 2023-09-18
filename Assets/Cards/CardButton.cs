using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{

    public Button yourButton;
    public GameObject power;

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
        
    }

    void ActivatePower() {
        if (power == null)
            return;
        GameObject newPower = Instantiate(power, PowerGenerator.transform);
        power = null;

        ResourceMessage rm = new ResourceMessage();
        rm.name = "Cooldown";
        rm.value = 0;
        resourceManager.SendMessage("resetResource", rm);
    }
}
