using UnityEngine;

[CreateAssetMenu(fileName = "HP Item", menuName = "Items")]
public class HPItem : ConsumableItem
{
    public int healValue;

    public override void Consume()
    {
        AudioManager.instance.Play("Heal");
        PlayerManager.instance.GetPlayerStats().IncreaseHealth(healValue);      
    }
}
