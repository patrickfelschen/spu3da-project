using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackDelay = 0.01f; // verzoegerung fuer die Animation

    private CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        StartCoroutine(DoDamage(targetStats, attackDelay));
    }

    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Calculate Crit
        if(Random.Range(0, 100f) <= myStats.critChance.GetValue())
        {
            stats.TakeDamage(myStats.damage.GetValue() * myStats.critMultiplier.GetValue());
        }
        else
        {
            stats.TakeDamage(myStats.damage.GetValue());
        }
    }
}

// COMBAT - Making an RPG in Unity (E11) https://www.youtube.com/watch?v=FhAdkLC-mSg
