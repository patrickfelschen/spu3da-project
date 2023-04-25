using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat health;
    public Stat damage;
    public Stat armor;
    public Stat walkSpeed;
    public Stat runSpeed;
    public Stat critChance;
    public Stat critMultiplier;
    public Stat attackSpeed;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damageAmount)
    {
        damageAmount -= armor.GetValue();
        damageAmount = Mathf.Clamp(damageAmount, 0, float.MaxValue);

        health.decreaseValue(damageAmount);

        OnDamage(damageAmount);

        if(health.GetValue() <= 0)
        {
            Die();
        }
    }

    public virtual void Die ()
    {
        Debug.Log(transform.name + " died.");
    }

    public virtual void OnDamage (float damage)
    {
        Debug.Log(transform.name + " takes " + damage + " damage.");
    }
}

// https://www.youtube.com/watch?v=e8GmfoaOB4Y