using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour {

    public GameObject SplashArea;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public TowerStats ts;
    public GameObject SplashGO;

    GameObject self;
    float spawnTime;
	// Use this for initialization
	void Start () {
        self = gameObject;
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(target.transform.position);
            GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            return;
        }
        

        DamageController dmg = other.gameObject.GetComponent<DamageController>();
        dmg.DoDamage(damage);


        if (ts.abilities.SlowEnemies) dmg.Slow();
        if (ts.abilities.SplashDamage)
        {
            GameObject splash = (GameObject)Instantiate(SplashArea, transform.position, Quaternion.identity);
            splash.GetComponent<DoSplashDamage>().damage = damage;
            Instantiate(SplashGO, transform.position, Quaternion.identity);
        }
        if (ts.abilities.DamageOverTime) { dmg.SustainedDamage(1); }
        if (ts.abilities.MoreGoldPerKill) { dmg.goldMultiplier = 1.5f; }
        if (ts.abilities.ArmourShred) { dmg.damageMultiplier += 0.1f; }

        Destroy(gameObject);
    }
}
