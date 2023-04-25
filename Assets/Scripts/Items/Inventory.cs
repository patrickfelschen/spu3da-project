using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Stack<HPItem> hpItems = new Stack<HPItem>();

    public void AddHpItem(HPItem item)
    {
        hpItems.Push(item);
    }

    public ConsumableItem GetHpItem()
    {
        return hpItems.Pop();
    }

    public bool IsEmpty()
    {
        return hpItems.Count == 0;
    }

}
