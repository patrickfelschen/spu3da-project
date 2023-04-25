using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    public GameLogic gameLogic;

    public Button confirmButton;
    public Button cancelButton;
    public Button upgradeDamageButton;
    public Button upgradeHealthButton;
    public Button upgradeSpeedButton;
    public Button upgradeCritChanceButton;
    public Button upgradeCritMultiplierButton;

    public TextMeshProUGUI headingText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI pointsLeftText;
    public TextMeshProUGUI critChanceText;
    public TextMeshProUGUI critMultiplierText;

    private void Start()
    {
        confirmButton.onClick.AddListener(onConfirm);
        cancelButton.onClick.AddListener(onCancel);
        upgradeDamageButton.onClick.AddListener(UpgradeDamage);
        upgradeHealthButton.onClick.AddListener(UpgradeArmor);
        upgradeSpeedButton.onClick.AddListener(UpgradeSpeed);
        upgradeCritChanceButton.onClick.AddListener(UpgradeCritChance);
        upgradeCritMultiplierButton.onClick.AddListener(UpgradeCritMultiplier);
    }

    private void Update()
    {
        if(PlayerManager.instance == null) return;

        headingText.text = "Level " + PlayerManager.instance.GetPlayerStats().GetLevel();
        dmgText.text = "Schaden: " + PlayerManager.instance.GetPlayerStats().damage.GetValue().ToString("F2");
        armorText.text = "Verteidigung: " + PlayerManager.instance.GetPlayerStats().armor.GetValue().ToString("F2");
        speedText.text = "Geschwindigkeit: " + PlayerManager.instance.GetPlayerStats().walkSpeed.GetValue().ToString("F2");
        critChanceText.text = "Krit. Chance: " + PlayerManager.instance.GetPlayerStats().critChance.GetValue().ToString("F2") + "%";
        critMultiplierText.text = "Krit. Multiplier: " + PlayerManager.instance.GetPlayerStats().critMultiplier.GetValue().ToString("F2");
        pointsLeftText.text = "Verfügbare Punkte: " + PlayerManager.instance.GetPlayerStats().GetPoints();
    }

    private void onConfirm()
    {
        if (PlayerManager.instance.levelBossKilled)
        {
            gameLogic.ResetLevel();
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
        
    }

    private void onCancel()
    {
        gameLogic.ShowScreen(this.gameObject, false);
    }

    private void UpgradeDamage()
    {
        if(PlayerManager.instance.GetPlayerStats().GetPoints() > 0)
        {
            AudioManager.instance.Play("Upgrade");
            PlayerManager.instance.GetPlayerStats().IncreaseDamage(0.15f);
            PlayerManager.instance.GetPlayerStats().DecreasePoints(1);
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
    }

    private void UpgradeArmor()
    {
        if (PlayerManager.instance.GetPlayerStats().GetPoints() > 0)
        {
            AudioManager.instance.Play("Upgrade");
            PlayerManager.instance.GetPlayerStats().IncreaseArmor(0.2f);
            PlayerManager.instance.GetPlayerStats().DecreasePoints(1);
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
    }

    private void UpgradeSpeed()
    {
        if (PlayerManager.instance.GetPlayerStats().GetPoints() > 0)
        {
            AudioManager.instance.Play("Upgrade");
            PlayerManager.instance.GetPlayerStats().IncreaseSpeed(1);
            PlayerManager.instance.GetPlayerStats().DecreasePoints(1);
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
    }

    private void UpgradeCritChance()
    {
        if (PlayerManager.instance.GetPlayerStats().GetPoints() > 0)
        {
            AudioManager.instance.Play("Upgrade");
            PlayerManager.instance.GetPlayerStats().IncreaseCritChance(1);
            PlayerManager.instance.GetPlayerStats().DecreasePoints(1);
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
    }

    private void UpgradeCritMultiplier()
    {
        if (PlayerManager.instance.GetPlayerStats().GetPoints() > 0)
        {
            AudioManager.instance.Play("Upgrade");
            PlayerManager.instance.GetPlayerStats().IncreaseCritMultiplier(0.1f);
            PlayerManager.instance.GetPlayerStats().DecreasePoints(1);
        }
        else
        {
            AudioManager.instance.Play("Deny");
        }
    }
}
