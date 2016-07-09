using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

    public int health;
    public GameObject self;

    void Start()
    {
        self = gameObject;
    }

    void Update()
    {
        
    }

    	
    public void DoDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(self);
        }
    }
}
