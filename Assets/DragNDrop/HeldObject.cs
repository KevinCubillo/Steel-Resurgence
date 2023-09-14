using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeldObject : MonoBehaviour
{

    TurretSlot heldObject;

    GameObject drawnObject;
    Camera camerac;

    [SerializeField]
    GameObject localCamera;

    private void Awake()
    {
        camerac = localCamera.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drawnObject != null) {
            Vector3 pos = Input.mousePosition;
            pos = camerac.ScreenToWorldPoint(pos);
            pos.z = 10;
            drawnObject.transform.position = pos;
        }
    }

    void setHeldObject(TurretSlot o) 
    {
        heldObject = o;
        if (drawnObject != null)
            Destroy(drawnObject);
        if (o.GetVisualObject() != null)
            drawnObject = Instantiate(o.GetVisualObject());
    }

    void removeHeldObject()
    {
        heldObject = null;
        if(drawnObject != null)
            Destroy(drawnObject);
        drawnObject = null;
    }

    void getHeldObject(GameObject callBackObject) {
        if (heldObject != null) { 
            callBackObject.SendMessage("setHeldObject", heldObject);
        }
    }
}
