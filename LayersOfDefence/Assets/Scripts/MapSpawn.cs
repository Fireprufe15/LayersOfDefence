using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapSpawn : MonoBehaviour {

    [HideInInspector]
    public List<Vector3> fullPath;

    public int seed;
    public int xSize;
    public int ySize;
    public GameObject roadTile;
    private Camera gameCam;

	// Use this for initialization
	void Start () {
        gameCam = Camera.main;
        fullPath = GenerateRoad();
        InstaintiateRoad();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InstaintiateRoad()
    {
        foreach (Vector3 point in fullPath)
        {
            Instantiate(roadTile, point, Quaternion.identity);
        }
    }

    private List<Vector3> GenerateRoad()
    {
        List<Vector3> path = new List<Vector3>();
        int currentXPos = 0;
        int currentYPos = 0;
        Random.seed = seed;
        currentYPos = Random.Range(0, ySize);
        path.Add(new Vector3(currentXPos, 0, currentYPos));
        while (currentXPos < xSize)
        {
            int direction = Random.Range(0, 4);
            int newXPos = currentXPos;
            int newYPos = currentYPos;
            switch (direction)
            {
                case 0:
                    if (currentYPos < ySize && !path.Exists(x => x.x == currentXPos && x.z == currentYPos+1))
                    {
                        newYPos += 1;                                                
                    }
                    break;
                case 1:
                    if (currentXPos < xSize && !path.Exists(x => x.x == currentXPos+1 && x.z == currentYPos))
                    {
                        newXPos += 1;
                    }
                    break;
                case 2:
                    if (currentYPos > 0 && !path.Exists(x=>x.x == currentXPos && x.z == currentYPos - 1))
                    {
                        newYPos -= 1;
                    }
                    break;
                case 3:
                    if (currentXPos > 0 && !path.Exists(x => x.x == currentXPos - 1 && x.z == currentYPos))
                    {
                        newXPos -= 1;
                    }
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
        if (path.Where(x => (x.x == newX && x.z == newY + 1)
            || (x.x == newX + 1 && x.z == newY)
            || (x.x == newX && x.z == newY - 1)
            || (x.x == newX - 1 && x.z == newY))
            .Count() > 1)
            return false;
        return true;
    }
    
}
