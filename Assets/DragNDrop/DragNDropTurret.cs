using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private void Awake()
    {
        TurretObjectData.SetVisualObject(visualObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        TurretObjectData.SetVisualObject(visualObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Selector.SendMessage("setHeldObject", TurretObjectData.copy());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SlotController.SendMessage("OnDrop");
        Selector.SendMessage("removeHeldObject");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
