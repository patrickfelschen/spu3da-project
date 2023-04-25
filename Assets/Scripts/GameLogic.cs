using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GameLogic : MonoBehaviour
{
    private RoomTemplates roomTemplates;
    private FurnitureTemplates furnitureTemplates;
    private List<GameObject> enemies = new();

    public GameObject player;
    public GameObject levelCompleteScreen;
    public GameObject gameOverScreen;
    public bool isMenuOpen = false;

    public float enemySpawnDelay = 5;

    void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Templates").GetComponent<RoomTemplates>();
        furnitureTemplates = GameObject.FindGameObjectWithTag("Templates").GetComponent<FurnitureTemplates>();
        levelCompleteScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        AudioManager.instance.Play("MainMenuTheme");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowScreen(levelCompleteScreen, true);
        }
        if (PlayerManager.instance != null && PlayerManager.instance.GetPlayerStats().health.GetValue() <= 0)
        {
            ShowScreen(gameOverScreen, true);
        }
    }

    public void Init()
    {
        Debug.Log("GameLogic: Initializing Level");

        AudioManager.instance.Stop("MainMenuTheme");
        AudioManager.instance.Play("GameTheme");

        Cursor.visible= false;

        GameObject go = Instantiate(roomTemplates.spawnRoom, transform.position, Quaternion.identity);
        roomTemplates.activeRooms.Add(go);
        // player.GetComponent<ThirdPersonMovement>().cameraTransform = Camera.main.transform;
        GameObject playerInstance = Instantiate(player);
        //Camera.main.GetComponent<CameraController>().target = playerInstance.transform;
        Invoke(nameof(GenerateNavMesh), enemySpawnDelay);
        Invoke(nameof(SpawnEnemies), enemySpawnDelay + 5);
    }

    public void ShowScreen(GameObject screen, bool show)
    {
        isMenuOpen = show;
        Cursor.visible= show;
        screen.SetActive(show);
    }

    public void ResetLevel()
    {
        Debug.Log("GameLogic: Resetting Level");
        DeleteRooms();

        PlayerManager.instance.RespawnPlayer();

        GameObject spawnRoom = Instantiate(roomTemplates.spawnRoom, transform.position, Quaternion.identity);
        roomTemplates.activeRooms.Add(spawnRoom);

        Invoke(nameof(GenerateNavMesh), enemySpawnDelay);
        Invoke(nameof(SpawnEnemies), enemySpawnDelay + 5);

        ShowScreen(levelCompleteScreen, false);
        ShowScreen(gameOverScreen, false);
    }

    public void ExitGame()
    {
        Debug.Log("GameLogic: Application stopped");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void GenerateNavMesh()
    {
        foreach (GameObject room in roomTemplates.activeRooms)
        {
            if (room != null && !room.CompareTag("ClosedRoom"))
            {
                room.transform.Find("Ground").GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }
    }

    void SpawnEnemies()
    {
        foreach (GameObject room in roomTemplates.activeRooms)
        {
            if (room != null)
            {
                room.GetComponent<EnemySpawner>().SpawnEnemies(1, 1, PlayerManager.instance.GetPlayerStats().GetLevel());
            }
        }
        Debug.Log("Boss Room: " + roomTemplates.activeRooms[roomTemplates.activeRooms.Count - 1].gameObject.name);
        roomTemplates.activeRooms[roomTemplates.activeRooms.Count - 1].GetComponent<EnemySpawner>().SpawnBoss();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    private void DeleteRooms()
    {
        ClearGameObjects(roomTemplates.activeRooms);
        ClearGameObjects(roomTemplates.activeCorridors);
        ClearGameObjects(roomTemplates.activeClosedRooms);
        ClearGameObjects(furnitureTemplates.activeFurniture);
        ClearGameObjects(enemies);
    }

    private void ClearGameObjects(List<GameObject> objects)
    {
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }
}
