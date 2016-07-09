using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TowerAttack : MonoBehaviour
{

    public int Range;
    public int Damage;
    public int AttackSpeed;
    public GameObject Bullet;
    public float spawnDistance = 1.2f;

    private GameObject self;
    private SphereCollider attackCollider;
    private GameObject lockedEnemy;
    private Queue<GameObject> lockQueue;
    private float nextFire;
    private bool towerIsShooting;
    private GameObject towerPlacer;



    // Use this for initialization
    void Start()
    {
        self = this.gameObject;
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.radius = Mathf.RoundToInt(Range / 2) + 1;
        lockQueue = new Queue<GameObject>();
        towerPlacer = GameObject.Find("TowerBuilder");
    }

    // Update is called once per frame
    void Update()
    {
        if (lockedEnemy != null)
        {
            RotateToEnemy();
            if (Time.time >= nextFire)
            {
                Shoot();
                nextFire = Time.time + 1 - AttackSpeed * 0.01f * 3;
            }
        }
        else
        {
            if (lockQueue.Count > 0)
            {
                lockedEnemy = lockQueue.Dequeue();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (tag == "Ghost")
        {
            if (other.gameObject.tag == "Tower")
            {
                towerPlacer.GetComponent<TowerPlacement>().onOtherTower = true;
            }
            
            return;
        }

        if (other.gameObject.tag != "Enemy")
        {
            return;
        }
        if (lockedEnemy == null)
        {
            lockedEnemy = other.gameObject;
            nextFire = Time.time + 1 / AttackSpeed;
        }
        else
        {
            lockQueue.Enqueue(other.gameObject);
        }
    
        

    }

    void OnTriggerExit(Collider other)
    {        
        if (tag == "Ghost")
        {
            if (other.gameObject.tag == "Tower")
            {
                towerPlacer.GetComponent<TowerPlacement>().onOtherTower = false;
            }

            return;
        }
        if (other.gameObject == lockedEnemy)
        {
            if (lockQueue.Count > 0)
            {
                lockedEnemy = lockQueue.Dequeue();
            }
            else
            {
                lockedEnemy = null;
            }
        }
        else
        {
            lockQueue = new Queue<GameObject>(lockQueue.Where(p => p != other.gameObject));
        }

        

    }

    void RotateToEnemy()
    {
        //rotate tower turret towards enemy if neccesary
        self.transform.LookAt(lockedEnemy.transform);
    }

    void Shoot()
    {
        //know where to shoot
        //spawn projectile/raycast/whatever and make it go to enenmy
        Debug.Log("Shooty shooty");
        self.transform.LookAt(lockedEnemy.transform);
        GameObject bullet = (GameObject)Instantiate(Bullet, self.transform.position + spawnDistance * self.transform.forward, self.transform.rotation);
        bullet.transform.Rotate(0, 0, 90f);
        moveForward mover = bullet.GetComponent<moveForward>();
        mover.damage = Damage;
        mover.speed = 20f;
        mover.target = lockedEnemy.transform.position;
    }    
}
