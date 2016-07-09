using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerAdder : MonoBehaviour {

    public Text currentTowerText;

    public void AddTower()
    {
        currentTowerText.text = "Tower: " + (TowerStats.towerIndex + 1) + "/5";
    }
}
