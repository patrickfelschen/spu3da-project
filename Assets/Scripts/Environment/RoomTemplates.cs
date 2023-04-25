using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject spawnRoom;
    public GameObject closedRoom;

    public List<GameObject> activeRooms = new();
    public List<GameObject> activeCorridors = new();
    public List<GameObject> activeClosedRooms = new();
}
