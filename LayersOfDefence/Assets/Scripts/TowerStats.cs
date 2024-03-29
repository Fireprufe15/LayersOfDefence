﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class SpecialAbilities
{
    public SpecialAbilities(SpecialAbilities s)
    {
        count = s.Count;
        slowEnemies = s.SlowEnemies;
        splashDamage = s.SplashDamage;
        moreGoldPerKill = s.MoreGoldPerKill;
        moreGoldPerWave = s.MoreGoldPerWave;
        armourShred = s.ArmourShred;
        damageOverTime = s.DamageOverTime;
    }

    public SpecialAbilities()
    {

    }

    private int count;

    public int Count
    {
        private set
        {
            if (value < 0)
            {
                count = 0;
            }
            else
            {
                count = value;
            }
        }

        get { return count; }
    }


    ///////////------------------
    private bool slowEnemies;

    public bool SlowEnemies
    {
        get { return slowEnemies; }
        set
        {
            slowEnemies = value;
            count = value == true ? count + 1 : count - 1;
        }
    }
    ///////////------------------
    private bool splashDamage;

    public bool SplashDamage
    {
        get { return splashDamage; }
        set
        {
            splashDamage = value;
            count = value == true ? count + 1 : count - 1;
        }
    }
    ///////////------------------
    private bool damageOverTime;

    public bool DamageOverTime
    {
        get { return damageOverTime; }
        set
        {
            damageOverTime = value;
            count = value == true ? count + 1 : count - 1;
        }
    }
    ///////////------------------
    private bool moreGoldPerKill;

    public bool MoreGoldPerKill
    {
        get { return moreGoldPerKill; }
        set
        {
            moreGoldPerKill = value;
            count = value == true ? count + 1 : count - 1;
        }
    }
    ///////////------------------
    private bool moreGoldPerWave;

    public bool MoreGoldPerWave
    {
        get { return moreGoldPerWave; }
        set
        {
            moreGoldPerWave = value;
            count = value == true ? count + 1 : count - 1;
        }
    }
    ///////////------------------
    private bool armourShred;

    public bool ArmourShred
    {
        get { return armourShred; }
        set
        {
            armourShred = value;
            count = value == true ? count + 1 : count - 1;
        }
    }

}

public class TowerStats : MonoBehaviour
{
    public static int towerIndex;

    void Awake()
    {
        towerIndex = 0;
    }

    int attribauteMultiplier = 5, abilityMultiplier = 75;

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int attackSpeed;
    [HideInInspector]
    public int range;
    [HideInInspector]
    public string name;
    [HideInInspector]
    public Mesh Top;
    [HideInInspector]
    public Mesh Middle;
    [HideInInspector]
    public Mesh Bottom;

    [HideInInspector] public Color[] blockColors;

    public SpecialAbilities abilities = new SpecialAbilities();
    GameObject builderGO;

    void Start()
    {
        damage = 1;
        range = 1;
        attackSpeed = 1;
        builderGO = GameObject.Find("BuilderTower");

        blockColors = new Color[3];
    }

    public int GetPrice()
    {
        int price = 0;
        price += damage * attribauteMultiplier * damage;
        price += range * attribauteMultiplier * range;
        price += attackSpeed * attribauteMultiplier * attackSpeed;

        price += abilities.Count * abilityMultiplier * abilities.Count * price / 10;

        return price;
    }

    public void AddTower(Text name)
    {
        this.name = name.text;
        Top = builderGO.transform.GetChild(0).gameObject.GetComponent<MeshFilter>().mesh;
        Middle = builderGO.transform.GetChild(1).gameObject.GetComponent<MeshFilter>().mesh;
        Bottom = builderGO.transform.GetChild(2).gameObject.GetComponent<MeshFilter>().mesh;
        
        blockColors[0] = builderGO.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;
        blockColors[1] = builderGO.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color;
        blockColors[2] = builderGO.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.color;

        towerIndex++;

        Towers.towers[towerIndex - 1] = (TowerStats)MemberwiseClone();
        Towers.towers[towerIndex - 1].abilities = new SpecialAbilities(abilities);
        Towers.towers[towerIndex - 1].blockColors = new Color[3] { new Color(blockColors[0].r, blockColors[0].g, blockColors[0].b), new Color(blockColors[1].r, blockColors[1].g, blockColors[1].b), new Color(blockColors[2].r, blockColors[2].g, blockColors[2].b) };
        if (towerIndex == 5)
        {
            towerIndex = 0;
            // start Level
            SceneManager.LoadScene("Game");
        }
    }
    
    internal void UpdateMesh(GameObject top, GameObject middle, GameObject bottom)
    {
        top.GetComponent<MeshFilter>().mesh = Top;
        middle.GetComponent<MeshFilter>().mesh = Middle;
        bottom.GetComponent<MeshFilter>().mesh = Bottom;
    }
}
