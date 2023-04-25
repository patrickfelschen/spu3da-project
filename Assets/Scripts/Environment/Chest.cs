using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Chest : Interactable
{
    public TextMeshPro textMesh;
    public HPItem loot;

    // Start is called before the first frame update
    void Start()
    {
        textMesh.enabled = false;   
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = textMesh.transform.rotation;
        rot.y = Quaternion.LookRotation(textMesh.transform.position - Camera.main.transform.position).y;
        textMesh.transform.rotation = rot;
    }

    public override void Interact()
    {

        textMesh.enabled = true;
    }

    public void GetLoot()
    {
        PlayerManager.instance.GetInventory().AddHpItem(loot);
        AudioManager.instance.Play("Pickup");
        Destroy(this.gameObject);
    }
}
