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

    [SerializeField]
    GameObject rangeViusal;

    private void Awake()
    {
        camerac = localCamera.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rangeViusal = Instantiate(rangeViusal, transform);
        rangeViusal.transform.position = Vector3.left * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawnObject != null)
        {
            Vector3 pos = Input.mousePosition;
            pos = camerac.ScreenToWorldPoint(pos);
            pos.z = 10;
            drawnObject.transform.position = pos;
            rangeViusal.transform.position = pos;

            if (heldObject == null) {
                removeHeldObject();
            }
        }

        if(heldObject == null)
        {
            rangeViusal.transform.position = Vector3.left * 100;
        }
    }

    void setHeldObject(TurretSlot o) 
    {
        heldObject = o;
        if (drawnObject != null)
            Destroy(drawnObject);
        if (o.GetVisualObject() != null)
            drawnObject = Instantiate(o.GetVisualObject());

        float scale = o.getCurrentTurret().GetComponent<Turret>().Range;
        rangeViusal.transform.localScale= new Vector3(scale, scale, 1);
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
