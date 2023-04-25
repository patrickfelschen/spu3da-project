using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBossStats : CharacterStats
{
    public Slider healthBar;

    public Canvas enemyCanvas;

    public GameObject damageText;


    private Animator animator;

    private EnemyStateController enemyController;

    private Collider collider;

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
        AudioManager.instance.Play("BlueBossDie");

        enemyCanvas.enabled = false;
        collider.enabled = false;

        // loot
        PlayerManager.instance.GetPlayerStats().IncreasePoints(10);
        PlayerManager.instance.levelBossKilled = true;
        AudioManager.instance.Play("Victory");
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        animator.SetTrigger("Hit");
        healthBar.value = health.GetValue();

        DamageIndicator indicator =
            Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
}

