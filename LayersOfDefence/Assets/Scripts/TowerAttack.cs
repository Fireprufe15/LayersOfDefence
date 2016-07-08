using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TowerAttack : MonoBehaviour {

    public int Range;
    public int Damage;
    public int AttackSpeed;

    private GameObject self;  
    private SphereCollider attackCollider;
    private GameObject lockedEnemy;
    private Queue<GameObject> lockQueue;
    private float nextFire;
    private bool towerIsShooting;

	// Use this for initialization
	void Start () {
        self = this.gameObject;                        
        attackCollider = GetComponent<SphereCollider>();
        attackCollider.radius = 1 * Range;
        lockQueue = new Queue<GameObject>();        
    }
	
	// Update is called once per frame
	void Update () {
        if (lockedEnemy != null)
        {
            RotateToEnemy();
            if (Time.time >= nextFire)
            {
                Shoot();
                nextFire = Time.time + 1 / AttackSpeed;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
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
            lockQueue = (Queue<GameObject>)lockQueue.Where(p => p != other.gameObject);
        }    
    }

    void RotateToEnemy()
    {
        //rotate tower turret towards enemy if neccesary
    }

    void Shoot()
    {
        //know where to shoot
        //spawn projectile/raycast/whatever and make it go to enenmy
    }
}
