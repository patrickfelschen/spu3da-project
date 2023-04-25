using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureTemplates : MonoBehaviour
{
    public GameObject[] tableTemplates;
    public GameObject[] barrelTemplates;
    public GameObject[] shelfTemplates;
    public GameObject[] torchTemplates;
    public GameObject[] carpetTemplates;
    public GameObject[] ovenTemplates;
    public GameObject[] chestTemplates;

    public List<GameObject> activeFurniture = new();
}
