using UnityEngine;
using System.Collections;
using System;

public class SpawnCreeps : MonoBehaviour {

    public int creepsPerWave;
    public float timeBetweenCreeps;
    public float timeBetweenWaves;
    public GameObject creep;
    public MapSpawn mapSpawn;

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
        SpawnWave();
	}

    private void SpawnWave()
    {
        if (Time.timeSinceLevelLoad >= nextWaveSpawn)
        {
            if (Time.timeSinceLevelLoad >= nextCreepSpawn)
            {
                // Spawn creep
                GameObject spawnedCreep = (GameObject)Instantiate(creep, StartingTile, Quaternion.identity);
                DamageController dmg = spawnedCreep.GetComponent<DamageController>();
                dmg.health = 50;
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
                }
            }
        }
    }
}
