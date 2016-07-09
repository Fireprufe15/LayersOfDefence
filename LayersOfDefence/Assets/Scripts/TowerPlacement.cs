﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TowerPlacement : MonoBehaviour {

    public Material greenMaterial;
    public Material redMaterial;
    [HideInInspector]
    public List<GameObject> spawnedTowers;

    float rayLength = 300f;
    LayerMask mask;
    Vector3 placePosition;
    public GameObject towerToSpawn;
    GameObject spawnGhost;

    private bool canPlace;
    [HideInInspector]
    public bool onOtherTower;

	// Use this for initialization
	void Start () {
        mask = LayerMask.GetMask("Floor");    
    }

    void OnEnable()
    {
        spawnGhost = (GameObject)Instantiate(towerToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        spawnGhost.GetComponent<SphereCollider>().enabled = false;
        spawnGhost.GetComponent<BoxCollider>().enabled = true;
        spawnGhost.GetComponent<BoxCollider>().isTrigger = true;
        spawnGhost.tag = "Ghost";
     }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(spawnGhost);

            enabled = false;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength, mask))
        {
            placePosition = hit.point;
            placePosition = new Vector3(Mathf.Round(placePosition.x), placePosition.y, Mathf.Round(placePosition.z));
            
            spawnGhost.transform.position = new Vector3(placePosition.x, 0.01f, placePosition.z);
            Debug.DrawLine(Camera.main.transform.position, placePosition);
            List<Ray> cornerChecks = new List<Ray>();
            cornerChecks.Add(new Ray(spawnGhost.transform.position, Vector3.down));
            cornerChecks.Add(new Ray(new Vector3(spawnGhost.transform.position.x + (spawnGhost.transform.localScale.x*2 / 2)*0.99f,
                spawnGhost.transform.position.y, spawnGhost.transform.position.z + (spawnGhost.transform.localScale.z*2 / 2)*0.99f), Vector3.down));            
            cornerChecks.Add(new Ray(new Vector3(spawnGhost.transform.position.x + (spawnGhost.transform.localScale.x*2 / 2)*0.99f,
                spawnGhost.transform.position.y, spawnGhost.transform.position.z - (spawnGhost.transform.localScale.z*2 / 2)*0.99f), Vector3.down));
            cornerChecks.Add(new Ray(new Vector3(spawnGhost.transform.position.x - (spawnGhost.transform.localScale.x*2 / 2)*0.99f,
                spawnGhost.transform.position.y, spawnGhost.transform.position.z - (spawnGhost.transform.localScale.z*2 / 2)*0.99f), Vector3.down));
            cornerChecks.Add(new Ray(new Vector3(spawnGhost.transform.position.x - (spawnGhost.transform.localScale.x*2 / 2)*0.99f,
                spawnGhost.transform.position.y, spawnGhost.transform.position.z + (spawnGhost.transform.localScale.z*2 / 2)*0.99f), Vector3.down));
            canPlace = true;

            
            foreach (Ray r in cornerChecks)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(r, out hitInfo, rayLength))
                {
                    Debug.DrawLine(r.origin,hitInfo.point);
                    if (hitInfo.collider.gameObject.tag == "Tile" || hitInfo.collider.gameObject.tag == "Enemy")
                    {   
                        canPlace = false;
                        break;                      
                    }
                }
            }
        }
        if (onOtherTower == true)
        {
            canPlace = false;
        }
        if (canPlace == true)
        {
            for (int i = 0 ; i < 3; i++)
            {
                spawnGhost.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = greenMaterial;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                spawnGhost.transform.GetChild(i).gameObject.GetComponent<Renderer>().material = redMaterial;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canPlace)
            {
                GameObject spawnedTower = (GameObject)Instantiate(towerToSpawn, spawnGhost.transform.position, spawnGhost.transform.rotation);
                spawnedTower.tag = "Tower";               
                spawnedTowers.Add(spawnedTower);
            }
        }
	}    
}
