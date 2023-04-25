using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI hpItems;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance == null) return;
        hpItems.text = PlayerManager.instance.GetInventory().hpItems.Count.ToString();   
    }
}
