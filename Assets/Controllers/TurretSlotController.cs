using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSlotController : MonoBehaviour
{

    bool _gameStarted;
    bool upgrading;
    Camera camerac;
    int lastSelectedSlot;
    int tbutton_index;
    List<TurretSlot> slotContents;
    TurretSlot emptySlot;
    TurretSlot tempSlot;

    [SerializeField]
    GameObject emptyObject;

    [SerializeField]
    List<Vector3> slotsPositions;

    [SerializeField]
    float slotRadius;

    [SerializeField]
    GameObject camera;

    [SerializeField]
    GameObject Selector;

    [SerializeField]
    GameObject UpgradeUI;

    [SerializeField]
    float UIRadius;

    [SerializeField]
    GameObject ResourceController;

    [SerializeField]
    GameObject SlotImage;


    private void Awake()
    {
        camerac = camera.GetComponent<Camera>();
        //emptyObject = Instantiate<GameObject>(emptyObject);
        emptySlot = new TurretSlot();
        slotContents = new List<TurretSlot>();
        GameObject slotImage;
        for (int i = 0; i < slotsPositions.Count; i++) {
            slotContents.Add(emptySlot);
            slotImage = Instantiate(SlotImage,transform);
            slotImage.transform.position = slotsPositions[i];
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
        bool clicked = Input.GetButtonDown("Fire1");
        if (clicked) {
            Vector3 pos = Input.mousePosition;
            pos = camerac.ScreenToWorldPoint(pos);
            pos.z = 0;
            UpgradeUI.transform.position = Vector3.left * 100;
            float dist;
            bool wasUpgrading = upgrading;
            upgrading = false;
            bool selected = false;
            for (int i = 0; i < slotsPositions.Count; i++) {
                dist = Vector3.Distance(pos, slotsPositions[i]);
                if (dist < slotRadius || (wasUpgrading && lastSelectedSlot == i&& dist < UIRadius)) {
                    //print("Clicked slot at: "+ slotsPositions[i]);
                    lastSelectedSlot = i;
                    if (slotContents[i] != emptySlot)
                    {
                        //Open Upgrade/Sell UI
                        UpgradeUI.transform.position = slotsPositions[i];
                        UpgradeUI.SendMessage("SetTurretData", new TurretUpgradeMenuMessage() { slotIndex = i, turretData = slotContents[i] });
                        upgrading = true;
                        selected = true;
                        break;
                    }
                }
            }
            if (!selected) {
                UpgradeUI.SendMessage("SetTurretData", new TurretUpgradeMenuMessage() { slotIndex = -1});
            }
        }
    }


    
    //Buy
    private void setHeldObject(TurretSlot turretPrefab) {
        tempSlot = turretPrefab;

        buyMessage m = new buyMessage();
        m.cost = tempSlot.initialCost();
        m.source = gameObject;
        m.callbackMessage = "CanBuy";
        ResourceController.SendMessage("CanBuy", m);
    }

    private void CanBuy(bool available) {
        if (!available)
            return;

        ResourceMessage m = new ResourceMessage();
        m.name = "Scrap";
        m.value = tempSlot.initialCost();
        ResourceController.SendMessage("consumeResource", m);

        GameObject newTurret = Instantiate(tempSlot.getCurrentTurret());
        newTurret.transform.position = slotsPositions[lastSelectedSlot];
        slotContents[lastSelectedSlot] = tempSlot;
        tempSlot.setTurret(newTurret);
        Selector.SendMessage("removeHeldObject");
        tempSlot = null;
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(slotsPositions[i] + currPosition, UIRadius);
        }
    }

    public void OnDrop()
    {
        Vector3 pos = Input.mousePosition;
        pos = camerac.ScreenToWorldPoint(pos);
        pos.z = 0;
        upgrading = false;
        for (int i = 0; i < slotsPositions.Count; i++)
        {
            if (Vector3.Distance(pos, slotsPositions[i]) < slotRadius)
            {
                //print("Clicked slot at: "+ slotsPositions[i]);
                lastSelectedSlot = i;
                if (slotContents[i] == emptySlot)
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

    //Upgrade
    private void UpgradeTurret(UpgradeButtonData slot)
    {
        if (slot.turretSlot < 0)
            return;
        if (slotContents[slot.turretSlot] == emptySlot)
            return;

        tempSlot = slotContents[slot.turretSlot];
        lastSelectedSlot = slot.turretSlot;
        tbutton_index = slot.buttonIndex;

        buyMessage m = new buyMessage();
        m.cost = slotContents[slot.turretSlot].NextUpgradeCost();
        m.source = gameObject;
        m.callbackMessage = "CanUpgrade";
        ResourceController.SendMessage("CanBuy", m);
    }

    private void CanUpgrade(bool available) {
        if (!available || tempSlot.NextUpgradeCost() == -1)
        {
            upgrading = true;
            tempSlot = null;
            return;
        }

        ResourceMessage m = new ResourceMessage();
        m.name = "Scrap";
        m.value = tempSlot.NextUpgradeCost();
        ResourceController.SendMessage("consumeResource", m);

        Destroy(tempSlot.getTurret());
        tempSlot.NextLevel(tbutton_index);

        GameObject newTurret = Instantiate(tempSlot.getCurrentTurret());
        newTurret.transform.position = slotsPositions[lastSelectedSlot];
        tempSlot.setTurret(newTurret);
        UpgradeUI.SendMessage("SetTurretData", new TurretUpgradeMenuMessage() { slotIndex = lastSelectedSlot, turretData = tempSlot });
        upgrading = true;
        tempSlot = null;
    }

    //Sell
    private void SellTurret(int slot) {
        if (slot < 0)
            return;
        if (slotContents[slot] == emptySlot)
            return;

        ResourceMessage m = new ResourceMessage();
        m.name = "Scrap";
        m.value = slotContents[slot].currentSellValue();
        ResourceController.SendMessage("giveResource", m);

        Destroy(slotContents[slot].getTurret());
        slotContents[slot].setTurret(null);
        slotContents[slot] = emptySlot;
        UpgradeUI.SendMessage("SetTurretData", new TurretUpgradeMenuMessage() { slotIndex = -1 });
        UpgradeUI.transform.position = Vector3.left * 100;
        upgrading = false;
    }
}


[System.Serializable]
class TurretSlot {
    int level = 0;
    TurretUpgrade currentTurret;
    GameObject worldTurret;
    GameObject visualObject;

    public List<TurretUpgrade> turretList;

    public void setCurrentTurret(GameObject turret) {
        currentTurret.turret = turret;
    }

    public GameObject getCurrentTurret() {
        if (currentTurret.turret == null && turretList != null)
            currentTurret = turretList[level];
        return currentTurret.turret;
    }

    public void setTurret(GameObject turret)
    {
        worldTurret = turret;
    }

    public GameObject getTurret()
    {
        return worldTurret;
    }

    //If there are different upgrade paths, here goes the logic for them
    public GameObject NextLevel(int buttonIndex) {
        level = level + (level+1 < turretList.Count? 1 : 0);
        currentTurret = turretList[level];
        return currentTurret.turret;
    }

    public int NextUpgradeCost() {
        return (level + 1 < turretList.Count ? turretList[level + 1].cost : -1);
    }

    public int initialCost() {
        return turretList[0].cost;
    }

    public int currentSellValue() {
        return turretList[level].sellValue;
    }

    public TurretSlot copy() {
        TurretSlot nt = new TurretSlot();
        nt.turretList = new List<TurretUpgrade>(turretList.ToArray());
        nt.setCurrentTurret(nt.turretList[0].turret);
        nt.SetVisualObject(visualObject);
        return nt;
    }

    public void SetVisualObject(GameObject v) {
        visualObject = v;
    }

    public GameObject GetVisualObject() {
        return visualObject;
    }

}

struct UpgradeButtonData {
    public int turretSlot;
    public int buttonIndex;
}

[System.Serializable]
struct TurretUpgrade {
    public GameObject turret;
    public int cost;
    public int sellValue;
}