using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossStateController : EnemyStateController
{
    public override void PlayHitSound()
    {
        AudioManager.instance.Play("BlueBossAttack");
    }
}
