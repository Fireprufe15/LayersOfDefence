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
    [HideInInspector]
    public TowerStats ts;
    
    private SphereCollider attackCollider;
    private GameObject lockedEnemy;
    private Queue<GameObject> lockQueue;
    private float nextFire;
    private bool towerIsShooting;
    public GameObject towerPlacer;
    AudioSource shootSound;

    // Use this for initialization
    void Start()
    {
        shootSound = transform.parent.GetComponent<AudioSource>();
        attackCollider = gameObject.GetComponent<SphereCollider>();
        attackCollider.radius += Range * 0.01f;
        lockQueue = new Queue<GameObject>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (lockedEnemy != null)
        {
            RotateToEnemy();
            if (Time.timeSinceLevelLoad >= nextFire)
            {
                Shoot();
                float addedTime = 1f / (AttackSpeed / 2f);
                nextFire = Time.timeSinceLevelLoad + addedTime;
            }
        }
        else
        {
            if (lockQueue.Count > 0)
            {
                lockedEnemy = lockQueue.Dequeue();
            }
        }
        
        transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.parent.gameObject.tag == "Ghost")
        {
            return;
        }

        if (other.gameObject.tag != "Enemy")
        {
            return;
        }
        if (lockedEnemy == null)
        {
            lockedEnemy = other.gameObject;
        }
        else
        {
            lockQueue.Enqueue(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {        

        if (gameObject.transform.parent.gameObject.tag == "Ghost")
        {      
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
        transform.LookAt(lockedEnemy.transform);
    }

    void Shoot()
    {
        //know where to shoot
        //spawn projectile/raycast/whatever and make it go to enenmy
        transform.LookAt(lockedEnemy.transform);
        GameObject bullet = (GameObject)Instantiate(Bullet, transform.position + spawnDistance * transform.forward, transform.rotation);
        if (shootSound.isPlaying) shootSound.Stop();
            shootSound.Play();
        bullet.transform.Rotate(0, 0, 90f);
        moveForward mover = bullet.GetComponent<moveForward>();
        mover.damage = Damage;
        mover.speed = 250f;
        mover.target = lockedEnemy;
        mover.ts = ts;
    }    
}
