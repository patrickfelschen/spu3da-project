using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonEnemyStats : CharacterStats
{
    public Slider healthBar;

    public Canvas enemyCanvas;

    public GameObject damageText;


    private Collider collider;

    private Animator animator;

    private EnemyStateController enemyController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyStateController>();
        collider = GetComponent<Collider>();

        healthBar.maxValue = health.GetBaseValue();
        healthBar.value = health.GetValue();
    }

    public override void Die()
    {
        base.Die();

        enemyController.isDead = true;

        animator.SetTrigger("Die");
        enemyCanvas.enabled = false;
        collider.enabled = false;


        // loot
        PlayerManager.instance.GetPlayerStats().IncreasePoints(1);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        animator.SetTrigger("Hit");
        AudioManager.instance.Play("SkeletonDamage");
        healthBar.value = health.GetValue();

        DamageIndicator indicator =
            Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
}

// ENEMY AI - Making an RPG in Unity (E10) https://www.youtube.com/watch?v=xppompv1DBg
