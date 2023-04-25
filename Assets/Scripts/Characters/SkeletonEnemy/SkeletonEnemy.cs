using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class SkeletonEnemy : Interactable
{
    private PlayerManager playerManager;

    private CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();

        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}

// ENEMY AI - Making an RPG in Unity (E10) https://www.youtube.com/watch?v=xppompv1DBg
// COMBAT - Making an RPG in Unity (E11) https://www.youtube.com/watch?v=FhAdkLC-mSg