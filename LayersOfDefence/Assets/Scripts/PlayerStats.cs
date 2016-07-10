using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public Text[] playerStatsTexts;

    #region Props
    private int gold;
    private int lives;
    private int wave;
    private int creepsOnMap;

    public int CreepsOnMap
    {
        get { return creepsOnMap; }
        set { creepsOnMap = value; playerStatsTexts[1].text = value.ToString(); }
    }


    public int Wave
    {
        get { return wave; }
        set { wave = value; playerStatsTexts[0].text = value.ToString(); }
    }


    public int Lives
    {
        get { return lives; }
        set { lives = value; playerStatsTexts[3].text = value.ToString(); }
    }


    public int Gold
    {
        get { return gold; }
        set { gold = value; playerStatsTexts[2].text = value.ToString(); }
    }

    #endregion

    void Start()
    {
        Gold = 500;
        Lives = 30;
        Wave = 1;
        CreepsOnMap = 0;
    }

}
