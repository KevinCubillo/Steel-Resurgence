using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public Button yourButton;
    private int turretSlot;

    [SerializeField]
    GameObject TurretSlotManager;

    [SerializeField]
    int buttonIndex;

    [SerializeField]
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(UpgradeTurret);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeTurret()
    {
        UpgradeButtonData data = new UpgradeButtonData();
        data.turretSlot = turretSlot;
        data.buttonIndex = buttonIndex;
        TurretSlotManager.SendMessage("UpgradeTurret", data);
    }

    void SetSlotIndex(int index)
    {
        turretSlot = index;
        RectTransform tr = yourButton.GetComponent<RectTransform>();
        tr.localPosition = turretSlot >= 0 ? transform.position : Vector3.left * 100;
    }

    void SetTurretData(TurretUpgradeMenuMessage data)
    {
        turretSlot = data.slotIndex;
        if (data.turretData != null) { 
            int cost = data.turretData.NextUpgradeCost();
            if (cost == -1)
                text.text = "MAX";
            else
                text.text = "" + data.turretData.NextUpgradeCost();
        }
        RectTransform tr = yourButton.GetComponent<RectTransform>();
        tr.localPosition = turretSlot >= 0 ? transform.position : Vector3.left * 100;
    }
}
