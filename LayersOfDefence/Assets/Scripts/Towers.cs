using UnityEngine;
using System.Collections;

public static class Towers
{
    public static TowerStats[] towers = new TowerStats[5];

    public static void Clear()
    {
        towers = new TowerStats[5];
    }
}
