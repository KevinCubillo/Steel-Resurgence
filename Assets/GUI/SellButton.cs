using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellButton : MonoBehaviour
{
    public Button yourButton;
    private int turretSlot;

    [SerializeField]
    GameObject TurretSlotManager;

    [SerializeField]
    TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(SellTurret);
    }

    // Update is called once per frame
    void Update()
    {
        //if (turretSlot >= 0)
        //{
        //    RectTransform tr = yourButton.GetComponent<RectTransform>();
        //    tr.localPosition = transform.position;
        //}
    }

    void SellTurret() {
        TurretSlotManager.SendMessage("SellTurret", turretSlot);
    }

    void SetSlotIndex(int index) {
        turretSlot = index;
        RectTransform tr = yourButton.GetComponent<RectTransform>();
        tr.localPosition = turretSlot >= 0? transform.position : Vector3.left * 100;
    }

    void SetTurretData(TurretUpgradeMenuMessage data) {
        turretSlot = data.slotIndex;
        if(data.turretData != null)
            text.text = "" + data.turretData.currentSellValue();
        RectTransform tr = yourButton.GetComponent<RectTransform>();
        tr.localPosition = turretSlot >= 0 ? transform.position : Vector3.left * 100;
    }
}