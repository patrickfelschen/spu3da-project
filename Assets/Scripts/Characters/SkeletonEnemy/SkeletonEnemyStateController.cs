using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyStateController : EnemyStateController
{

    public override void PlayHitSound()
    {
        AudioManager.instance.Play("SkeletonAttack");
    }
}
