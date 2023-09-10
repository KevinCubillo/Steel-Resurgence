using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeldObject : MonoBehaviour
{

    GameObject heldObject;

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

    void setHeldObject(GameObject o) {
        heldObject = o;
        drawnObject = Instantiate(o);
    }

    void removeHeldObject()
    {
        heldObject = null;
        Destroy(drawnObject);
        drawnObject = null;
    }

    void getHeldObject(GameObject callBackObject) {
        if (heldObject != null) { 
            callBackObject.SendMessage("setHeldObject", heldObject);
        }
    }
}
