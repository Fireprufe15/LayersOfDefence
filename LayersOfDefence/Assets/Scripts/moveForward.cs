using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour {

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 target;
    [HideInInspector]
    public int damage;

    GameObject self;
    float spawnTime;
	// Use this for initialization
	void Start () {
        self = gameObject;
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        self.transform.position = Vector3.MoveTowards(self.transform.position, target, speed*Time.deltaTime);
        if (Time.time > spawnTime+0.3f)
        {
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
}
