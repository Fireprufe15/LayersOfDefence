using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class MapSpawn : MonoBehaviour {

    [HideInInspector]
    public List<Vector3> fullPath;
    public GameObject Cave;
    public GameObject Castle;

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
        Camera.main.transform.position = new Vector3(fullPath[0].x, fullPath[0].y + 8, fullPath[0].z - 5);
    }

    private void InstaintiateRoad()
    {
        foreach (Vector3 point in fullPath)
        {
            GameObject currentTile = (GameObject)Instantiate(roadTile, point, Quaternion.Euler(90,0,0));
            currentTile.transform.parent = tilesParent.transform;

            if (fullPath[fullPath.Count - 1] == point)
            {
                currentTile.GetComponent<LoseLife>().enabled = true;
                currentTile.GetComponent<BoxCollider>().enabled = true;
            }
        }

        Vector3 cavPos = new Vector3(fullPath[0].x - 4f, fullPath[0].y, fullPath[0].z + 0.25f);
        Instantiate(Cave, cavPos, Quaternion.Euler(0, 90, 0));

        Vector3 castle = new Vector3(fullPath[fullPath.Count - 1].x + 4f, fullPath[fullPath.Count - 1].y, fullPath[fullPath.Count - 1].z);
        Instantiate(Castle, castle, Quaternion.Euler(0, 180, 0));
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
