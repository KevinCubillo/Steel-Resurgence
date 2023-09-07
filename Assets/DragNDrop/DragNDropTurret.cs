using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragNDropTurret : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    GameObject TurretPrefab;

    [SerializeField]
    GameObject Selector;

    [SerializeField]
    GameObject SlotController;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Selector.SendMessage("setHeldObject", TurretPrefab);
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
