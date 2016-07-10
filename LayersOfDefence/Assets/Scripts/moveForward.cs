using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour {

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public TowerStats ts;

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

        Destroy(gameObject);
    }
}
