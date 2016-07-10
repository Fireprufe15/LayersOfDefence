using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillButtons : MonoBehaviour {

    public Text[] ButtonTexts;
    public GameObject BaseTower;
    public TowerPlacement tpScript;

	void Start ()
    {
        for (int i = 0; i < 5; i++)
        {
            ButtonTexts[i].text = Towers.towers[i].name;
        }
	}

    public void PlaceTower(int towerIndex)
    {
        GameObject Top = BaseTower.transform.GetChild(0).gameObject;
        GameObject Middle = BaseTower.transform.GetChild(1).gameObject;
        GameObject Bottom = BaseTower.transform.GetChild(2).gameObject;

        TowerStats ts = BaseTower.GetComponent<TowerStats>();

        if (!ts)
        {
            ts = BaseTower.AddComponent<TowerStats>();
        }

        ts = Towers.towers[towerIndex];

        ts.UpdateMesh(Top, Middle, Bottom);
        tpScript.towerToSpawn = BaseTower;
        tpScript.enabled = true;
        tpScript.ts = ts;
    }
}
