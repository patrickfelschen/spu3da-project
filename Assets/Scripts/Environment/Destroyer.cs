using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("ClosedRoom"))
        {
            //GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>().activeRooms.Remove(other.gameObject);
            Destroy(other.gameObject);
            Debug.Log("Destroyer: Deleted " + other.gameObject);

        }
    }
}
