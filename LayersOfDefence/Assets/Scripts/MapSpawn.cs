﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MapSpawn : MonoBehaviour {

    [HideInInspector]
    public List<Vector3> fullPath;

    public int seed;
    public bool randomSeed;
    public int xSize;
    public int ySize;
    public GameObject roadTile;
    public GameObject tilesParent;
    private Camera gameCam;

	// Use this for initialization
	void Start () {

        if (randomSeed)
            seed = UnityEngine.Random.seed = new System.Random().Next(int.MaxValue);

        gameCam = Camera.main;
        fullPath = GenerateRoad();
        InstaintiateRoad();

        CentreCamera();
	}

    private void CentreCamera()
    {
        gameCam.transform.position = new Vector3(xSize / 2, gameCam.transform.position.y, ySize / 2);
    }

    private void InstaintiateRoad()
    {
        foreach (Vector3 point in fullPath)
        {
            GameObject currentTile = (GameObject)Instantiate(roadTile, point, Quaternion.Euler(90,0,0));
            currentTile.transform.parent = tilesParent.transform;
        }
    }

    private List<Vector3> GenerateRoad()
    {
        List<Vector3> path = new List<Vector3>();
        int currentXPos = 0;
        int currentYPos = 0;
        UnityEngine.Random.seed = seed;
        currentYPos = UnityEngine.Random.Range(0, ySize);
        path.Add(new Vector3(currentXPos, 0, currentYPos));
        while (currentXPos < xSize)
        {
            int direction = UnityEngine.Random.Range(0, 3);
            int newXPos = currentXPos;
            int newYPos = currentYPos;
            switch (direction)
            {
                case 0:
                    if (currentYPos < ySize && !path.Exists(x => x.x == currentXPos && x.z == currentYPos+1))
                    {
                        newYPos += 1;                                                
                    }
                    else { continue; }
                    break;
                case 1:
                    if (currentXPos < xSize && !path.Exists(x => x.x == currentXPos+1 && x.z == currentYPos))
                    {
                        newXPos += 1;
                    }
                    else { continue; }
                    break;
                case 2:
                    if (currentYPos > 0 && !path.Exists(x=>x.x == currentXPos && x.z == currentYPos - 1))
                    {
                        newYPos -= 1;
                    }
                    else { continue; }
                    break;
                case 3:
                    if (currentXPos > 0 && !path.Exists(x => x.x == currentXPos - 1 && x.z == currentYPos))
                    {
                        newXPos -= 1;
                    }
                    else {continue;}
                    break;
                default:
                    break;
            }
            if (!checkBorders(path, newXPos, newYPos) && (newXPos != currentXPos || newYPos != currentYPos))
            {
                continue;
            }
            currentXPos = newXPos;
            currentYPos = newYPos;
            path.Add(new Vector3(currentXPos, 0, currentYPos));
        }
        return path;
        
    }
    private bool checkBorders(List<Vector3> path, int newX, int newY)
    {
        if ((path.Where(x => (x.x == newX && x.z == newY + 1)
                || (x.x == newX + 1 && x.z == newY)
                || (x.x == newX && x.z == newY - 1)
                || (x.x == newX - 1 && x.z == newY))
            .Count() > 1))
            return false;
        return true;
    }
    
}