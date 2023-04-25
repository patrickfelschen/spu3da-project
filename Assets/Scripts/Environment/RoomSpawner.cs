using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomSpawner : MonoBehaviour
{
    /*
    * 1 = bottom
    * 2 = top
    * 3 = left
    * 4 = right
    */
    public int openingDir;

    private RoomTemplates templates; 
    private int rnd;
    private bool spawned = false;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5f);
    }

    void Spawn() {
        if(!spawned)
        {
            if(openingDir == 1) {
                // spawn room with bottom door
                rnd = Random.Range(0, templates.bottomRooms.Length);
                SpawnRoom(templates.bottomRooms[rnd], transform.position, templates.bottomRooms[rnd].transform.rotation);      
            }
            else if(openingDir == 2) {
                // spawn room with top door
                rnd = Random.Range(0, templates.topRooms.Length);
                SpawnRoom(templates.topRooms[rnd], transform.position, templates.topRooms[rnd].transform.rotation);
            }
            else if(openingDir == 3) {
                // spawn room with left door
                rnd = Random.Range(0, templates.leftRooms.Length);
                SpawnRoom(templates.leftRooms[rnd], transform.position, templates.leftRooms[rnd].transform.rotation);
            }
            else if(openingDir == 4) {
                // spawn room with right door
                rnd = Random.Range(0, templates.rightRooms.Length);
                SpawnRoom(templates.rightRooms[rnd], transform.position, templates.rightRooms[rnd].transform.rotation);
            }
            spawned = true;
        }
    }

    private void SpawnRoom(GameObject room, Vector3 pos, Quaternion rot) 
    {
        GameObject go = Instantiate(room, pos, rot);
        if(go.CompareTag("Corridor"))
        {
            templates.activeCorridors.Add(go);
        }
        else if(go.CompareTag("ClosedRoom"))
        {
            templates.activeClosedRooms.Add(go);
        }
        else
        {
            templates.activeRooms.Add(go);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomSpawner>().spawned == false && spawned == false) 
            { 
                SpawnRoom(templates.closedRoom, transform.position, templates.closedRoom.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;       
        }
    }
}
