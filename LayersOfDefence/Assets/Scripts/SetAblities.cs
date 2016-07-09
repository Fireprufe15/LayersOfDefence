using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetAblities : MonoBehaviour {

    public TowerStats towerStats;
    public Text costValueText;

    public void ToggleSlowEnemies(bool value)
    {
        if (value)
        {
            towerStats.abilities.SlowEnemies = true;
        }
        else
        {
            towerStats.abilities.SlowEnemies = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ToggleSplashDamage(bool value)
    {
        if (value)
        {
            towerStats.abilities.SplashDamage = true;
        }
        else
        {
            towerStats.abilities.SplashDamage = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ToggleDamageOverTime(bool value)
    {
        if (value)
        {
            towerStats.abilities.DamageOverTime = true;
        }
        else
        {
            towerStats.abilities.DamageOverTime = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ToggleMoreGoldPerKill(bool value)
    {
        if (value)
        {
            towerStats.abilities.MoreGoldPerKill = true;
        }
        else
        {
            towerStats.abilities.MoreGoldPerKill = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ToggleMoreGoldPerWave(bool value)
    {
        if (value)
        {
            towerStats.abilities.MoreGoldPerWave = true;
        }
        else
        {
            towerStats.abilities.MoreGoldPerWave = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ToggleArmourShred(bool value)
    {
        if (value)
        {
            towerStats.abilities.ArmourShred = true;
        }
        else
        {
            towerStats.abilities.ArmourShred = false;
        }

        costValueText.text = towerStats.GetPrice().ToString();
    }
}
