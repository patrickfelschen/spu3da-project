using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private HealthBar healthBar;

    public GameObject damageText;

    public float healthRegTime;

    private Animator animator;

    private int points = 0;
    private int level = 1;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        healthBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        Debug.Log("Healthbar: + " + healthBar);
        healthBar.SetMaxHealth(health.GetValue());

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > healthRegTime && health.currentValue < health.baseValue)
        {
            timer = 0;
            health.IncreaseValue(1);
        }

        healthBar.SetHealth(health.GetValue());
    }

    public void DoBlock()
    {
        armor.IncreaseValue(armor.GetValue());
    }

    public void ExitBlock()
    {
        armor.decreaseValue(armor.GetValue() / 2);
    }

    public override void Die()
    {
        base.Die();
        AudioManager.instance.Play("Death");
        animator.SetTrigger("Die");
        PlayerManager.instance.KillPlayer();
    }

    public void IncreaseHealth(float amount)
    {
        if (health.GetValue() >= health.GetBaseValue())
        {
            return;
        }

        health.IncreaseValue(amount);

        if (health.GetValue() > health.GetBaseValue())
        {
            health.SetValue(health.GetBaseValue());
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        health.IncreaseValue(amount);
    }


    public void IncreaseArmor(float amount)
    {
        armor.IncreaseValue(amount);
    }

    public void IncreaseDamage(float amount)
    {
        damage.IncreaseValue(amount);
    }

    public void IncreaseSpeed(float amount)
    {
        walkSpeed.IncreaseValue(amount);
        runSpeed.IncreaseValue(amount);
    }

    public void IncreasePoints(int amount)
    {
        points += amount;
    }

    public void DecreasePoints(int amount)
    {
        points -= amount;
    }

    public void IncreaseCritChance(float amount)
    {
        critChance.IncreaseValue(amount);
    }

    public void IncreaseCritMultiplier(float amount)
    {
        critMultiplier.IncreaseValue(amount);
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetLevel()
    {
        return level;
    }

    public void IncreaseLevel(int amount)
    {
        level += amount;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        animator.SetTrigger("Hit");
        AudioManager.instance.Play("Damage");

        DamageIndicator indicator =
               Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(damage);
    }

    public void ResetStats()
    {
        health.RestValue();
        damage.RestValue();
        walkSpeed.RestValue();
        runSpeed.RestValue();
        critChance.RestValue();
        critMultiplier.RestValue();
        armor.RestValue();
        points = 0;
        level = 1;
    }
}

// COMBAT - Making an RPG in Unity (E11) https://www.youtube.com/watch?v=FhAdkLC-mSg
