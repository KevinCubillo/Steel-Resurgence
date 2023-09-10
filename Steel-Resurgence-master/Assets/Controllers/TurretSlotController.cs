using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSlotController : MonoBehaviour
{

    bool _gameStarted;
    Camera camerac;
    int lastSelectedSlot;

    [SerializeField]
    GameObject emptyObject;

    List<GameObject> slotContents;

    [SerializeField]
    List<Vector3> slotsPositions;

    [SerializeField]
    float slotRadius;

    [SerializeField]
    GameObject camera;

    [SerializeField]
    GameObject Selector;


    private void Awake()
    {
        camerac = camera.GetComponent<Camera>();
        //emptyObject = Instantiate<GameObject>(emptyObject);
        slotContents = new List<GameObject>();
        for (int i = 0; i < slotsPositions.Count; i++) {
            slotContents.Add(emptyObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool clicked = Input.GetButtonUp("Fire1");
        if (clicked) {
            Vector3 pos = Input.mousePosition;
            pos = camerac.ScreenToWorldPoint(pos);
            pos.z = 0;
            for (int i = 0; i < slotsPositions.Count; i++) {
                if (Vector3.Distance(pos, slotsPositions[i]) < slotRadius) {
                    //print("Clicked slot at: "+ slotsPositions[i]);
                    lastSelectedSlot = i;
                    if (slotContents[i] != emptyObject)
                    {
                        //Open Upgrade/Sell UI
                    }
                    break;
                }
            }
        }
    }


    private void setHeldObject(GameObject turretPrefab) {
        GameObject newTurret = Instantiate(turretPrefab);
        newTurret.transform.position = slotsPositions[lastSelectedSlot];
        slotContents[lastSelectedSlot] = newTurret;
        Selector.SendMessage("removeHeldObject");
    }


    private void OnDrawGizmos()
    {
        Vector3 currPosition = transform.position;
        if (!_gameStarted && transform.hasChanged)
        {
            currPosition = transform.position;
        }

        for (int i = 0; i < slotsPositions.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(slotsPositions[i] + currPosition, slotRadius);
        }
    }

    public void OnDrop()
    {
        Vector3 pos = Input.mousePosition;
        pos = camerac.ScreenToWorldPoint(pos);
        pos.z = 0;
        for (int i = 0; i < slotsPositions.Count; i++)
        {
            if (Vector3.Distance(pos, slotsPositions[i]) < slotRadius)
            {
                //print("Clicked slot at: "+ slotsPositions[i]);
                lastSelectedSlot = i;
                if (slotContents[i] == emptyObject)
                {
                    Selector.SendMessage("getHeldObject", gameObject);
                }
                else
                {
                    Selector.SendMessage("removeHeldObject");
                }
                break;
            }
        }
    }
}
