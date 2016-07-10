using UnityEngine;
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
    [HideInInspector]
    public TowerStats ts;
    public PlayerStats playerStats;
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
            Ray r = new Ray(spawnGhost.transform.position, Vector3.down);
            RaycastHit rh;
            canPlace = true;
            if (Physics.Raycast(r, out rh, rayLength))
            {
                Debug.DrawLine(r.origin, placePosition);
                if (rh.collider.tag == "Tile" || rh.collider.tag == "Enemy" || rh.collider.tag == "Tower")
                {
                    canPlace = false;
                }
            }
            if (spawnedTowers.Where(p => p.transform.position == spawnGhost.transform.position).Count() > 0)
            {
                canPlace = false;
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
                playerStats.Gold -= ts.GetPrice();
                spawnedTower.tag = "Tower";
                spawnedTower.transform.FindChild("Bottom").tag = "Tower";             
                spawnedTowers.Add(spawnedTower);

                TowerAttack topStats = spawnedTower.transform.GetChild(2).gameObject.GetComponent<TowerAttack>();

                topStats.Damage = ts.damage;
                topStats.Range = ts.range;
                topStats.AttackSpeed = ts.attackSpeed;
                topStats.ts = ts;
            }
        }
	}    
}
