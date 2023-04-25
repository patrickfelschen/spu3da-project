using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public bool isAlive = true;

    public bool levelBossKilled = false;

    public void KillPlayer()
    {
        // TODO: animation + open menu
        player.GetComponent<Collider>().enabled = false;
        isAlive = false;
    }

    public void RespawnPlayer()
    {
        if (!isAlive)
        {
            GetPlayerStats().ResetStats();
            isAlive = true;
        }
        else
        {
            GetPlayerStats().IncreaseLevel(1);
            levelBossKilled = false;
        }
        Animator playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Respawn");
        playerAnimator.ResetTrigger("Die");
        playerAnimator.ResetTrigger("Hit");
        playerAnimator.ResetTrigger("Attack");

        player.GetComponent<Collider>().enabled = true;

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = Vector3.zero;
        player.GetComponent<CharacterController>().enabled = true;
    }

    public PlayerStats GetPlayerStats()
    {
        return player.GetComponent<PlayerStats>();
    }

    public Inventory GetInventory()
    {
        return player.GetComponent<Inventory>();
    }
}
