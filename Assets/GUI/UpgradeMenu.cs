using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    List<GameObject> buttons;

    [SerializeField]
    GameObject VisualRange;

    // Start is called before the first frame update
    void Start()
    {
        VisualRange = Instantiate(VisualRange, transform);
        VisualRange.transform.position = Vector3.left * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (VisualRange != null)
            VisualRange.transform.position = transform.position;
    }

    private void SetTurretData(TurretUpgradeMenuMessage data) {
        if (data.turretData != null) { 
            float scale = data.turretData.getCurrentTurret().GetComponent<Turret>().Range;
            VisualRange.transform.localScale = new Vector3(scale, scale, 1);
        }

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