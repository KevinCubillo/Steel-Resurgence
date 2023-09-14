using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTurretData(TurretUpgradeMenuMessage data) {
        foreach (var button in buttons)
        {
            button.SendMessage("SetTurretData", data);
        }
    }

}


struct TurretUpgradeMenuMessage {
    public TurretSlot turretData;
    public int slotIndex;
}