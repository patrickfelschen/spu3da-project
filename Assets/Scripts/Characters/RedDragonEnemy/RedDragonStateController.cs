using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonStateController : EnemyStateController
{
    public override void PlayHitSound()
    {
        AudioManager.instance.Play("RedDragonAttack");
    }
}
