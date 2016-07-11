using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SpawnCreeps : MonoBehaviour {

    public GameObject[] creepTypes;
    public int creepsPerWave;
    public float timeBetweenCreeps;
    public float timeBetweenWaves;
    public GameObject creep;
    public MapSpawn mapSpawn;
    public GameObject indicatorText;
    public float initialWait;
    public Text waveText;
    public PlayerStats ps;
    public int goldPerWaveMultiplier;
    [HideInInspector] public int towerAfterWaveGold = 0;

    [HideInInspector]
    public int wave;
    float nextCreepSpawn = 0f;
    float nextWaveSpawn;
    int creepCount = 0;
    Vector3 StartingTile;
    
	void Start ()
    {
        StartingTile = mapSpawn.fullPath[0];
	}

	void Update ()
    {
        if (Time.timeSinceLevelLoad < initialWait)
        {
            waveText.text = "Next Wave In: " + Mathf.Round((initialWait - Time.timeSinceLevelLoad + 1)).ToString();
            return;
        }

        SpawnWave();
    }

    int creepTypeToSpawnIndex = 0;

    private void SpawnWave()
    {
        if (Time.timeSinceLevelLoad >= nextWaveSpawn)
        {
            if (Time.timeSinceLevelLoad >= nextCreepSpawn)
            {
                waveText.enabled = false;
                // Spawn creep
                GameObject spawnedCreep = (GameObject)Instantiate(creepTypes[creepTypeToSpawnIndex], StartingTile, Quaternion.identity);
                ps.CreepsOnMap++;
                DamageController dmg = spawnedCreep.GetComponent<DamageController>();
                dmg.currentHP = dmg.startingHP * ((wave + wave + 1)/2);
                dmg.goldPerKill = 10 + Mathf.RoundToInt((wave + 1) * (wave + 1) / 2);
                dmg.textObject = indicatorText;
                Nav n = spawnedCreep.GetComponent<Nav>();
                n.points = mapSpawn.fullPath;
                n.isNavigating = true;

                creepCount++;
                nextCreepSpawn = Time.timeSinceLevelLoad + timeBetweenCreeps;

                if (creepCount >= creepsPerWave)
                {
                    creepCount = 0;
                    nextWaveSpawn = Time.timeSinceLevelLoad + timeBetweenWaves;
                    wave++;

                    creepTypeToSpawnIndex = UnityEngine.Random.Range(0, creepTypes.Length);

                    ps.Wave = wave;
                    ps.Gold += wave * goldPerWaveMultiplier + towerAfterWaveGold;
                }
            }
        }
        else
        {
            waveText.enabled = true;
            waveText.text = "Next Wave In: " + Mathf.Round(nextWaveSpawn - Time.timeSinceLevelLoad + 1).ToString();
        }
    }
}
