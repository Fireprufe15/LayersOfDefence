using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetAtributes : MonoBehaviour {

    public Text[] attributeValueTexts;
    public Text costValueText;
    public TowerStats towerStats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeDamage(float value)
    {
        int amount = Mathf.RoundToInt(value * 10f);

        amount = amount <= 0 ? 1 : amount;

        towerStats.damage = amount;
        attributeValueTexts[0].text = amount.ToString();
        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ChangeRange(float value)
    {
        int amount = Mathf.RoundToInt(value * 10f);

        amount = amount <= 0 ? 1 : amount;

        towerStats.range = amount;
        attributeValueTexts[1].text = amount.ToString();
        costValueText.text = towerStats.GetPrice().ToString();
    }

    public void ChangeAttackSpeed(float value)
    {
        int amount = Mathf.RoundToInt(value * 10f);

        amount = amount <= 0 ? 1 : amount;

        towerStats.attackSpeed = amount;
        attributeValueTexts[2].text = amount.ToString();
        costValueText.text = towerStats.GetPrice().ToString();
    }
}
