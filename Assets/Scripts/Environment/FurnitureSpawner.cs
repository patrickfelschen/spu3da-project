using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    /*
     * 1 = Table
     * 2 = Barrel
     * 3 = Shelf
     * 4 = Torch
     * 5 = Carpet
     * 6 = Oven
     * 7 = Chest
     */
    public int furnitureType;
    public float chance = 50.0f;

    private FurnitureTemplates furnitureTemplates;
    private bool spawned = false;

    void Start()
    {
        furnitureTemplates = GameObject.FindGameObjectWithTag("Templates").GetComponent<FurnitureTemplates>();

        if (Random.Range(0, 100f) <= chance)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if(furnitureType == 1)
        {
            // Spawn random table
            int rndIndex = Random.Range(0, furnitureTemplates.tableTemplates.Length);
            SpawnFurniture(furnitureTemplates.tableTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 2) 
        {
            // Spawn random barrel
            int rndIndex = Random.Range(0, furnitureTemplates.barrelTemplates.Length);
            SpawnFurniture(furnitureTemplates.barrelTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 3)
        {
            // Spawn random shelf
            int rndIndex = Random.Range(0, furnitureTemplates.shelfTemplates.Length);
            SpawnFurniture(furnitureTemplates.shelfTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 4)
        {
            // Spawn random torch
            int rndIndex = Random.Range(0, furnitureTemplates.torchTemplates.Length);
            SpawnFurniture(furnitureTemplates.torchTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 5)
        {
            // Spawn random carpet
            int rndIndex = Random.Range(0, furnitureTemplates.carpetTemplates.Length);
            SpawnFurniture(furnitureTemplates.carpetTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 6)
        {
            // Spawn random oven
            int rndIndex = Random.Range(0, furnitureTemplates.ovenTemplates.Length);
            SpawnFurniture(furnitureTemplates.ovenTemplates[rndIndex], transform.position, transform.rotation);
        }
        else if(furnitureType == 7)
        {
            // Spawn random chest
            int rndIndex = Random.Range(0, furnitureTemplates.chestTemplates.Length);
            SpawnFurniture(furnitureTemplates.chestTemplates[rndIndex], transform.position, transform.rotation);
        }
    }

    private void SpawnFurniture(GameObject furnitureObject, Vector3 pos, Quaternion rot)
    {
        GameObject go = Instantiate(furnitureObject, pos, rot);
        furnitureTemplates.activeFurniture.Add(go);
        spawned = true;
    }

    public bool IsSpawned()
    {
        return spawned; 
    }
}
