using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragNDropTurret : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    GameObject visualObject;

    [SerializeField]
    TurretSlot TurretObjectData;

    [SerializeField]
    GameObject Selector;

    [SerializeField]
    GameObject SlotController;

    [SerializeField]
    TMP_Text TurretCost;

    [SerializeField]
    TMP_Text TurretDPS;

    [SerializeField]
    TMP_Text TurretRof;

    [SerializeField]
    TMP_Text TurretType;


    private void Awake()
    {
        TurretObjectData.SetVisualObject(visualObject);
        GameObject TurretObject = TurretObjectData.getCurrentTurret();
        Turret Data = TurretObject.GetComponent<Turret>();

        TurretCost.text = "" + TurretObjectData.initialCost();
        TurretDPS.text = "" + Data.DPS;
        TurretRof.text = "" + Data.FireRate + "/s";
        TurretType.text = "" + Data.DamageType;
    }

    // Start is called before the first frame update
    void Start()
    {
        TurretObjectData.SetVisualObject(visualObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SlotController.SendMessage("OnDrop");
        Selector.SendMessage("removeHeldObject");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Selector.SendMessage("setHeldObject", TurretObjectData.copy());
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
