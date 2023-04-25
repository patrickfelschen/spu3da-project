using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDragonEnemyStats : CharacterStats
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
        AudioManager.instance.Play("RedDragonDie");

        enemyCanvas.enabled = false;
        collider.enabled = false;

        // loot
        PlayerManager.instance.GetPlayerStats().IncreasePoints(1);
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        animator.SetTrigger("Hit");
        AudioManager.instance.Play("Damage");
        healthBar.value = health.GetValue();

        DamageIndicator indicator =
            Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }
}

