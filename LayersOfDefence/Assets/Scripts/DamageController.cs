using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

    public int health;
    public int slowDuration;

    bool isSlowed;
    GameObject self;
    Nav movementScript;
    float slowEnd = 0f;

    void Start()
    {
        self = gameObject;
        movementScript = GetComponent<Nav>();
    }

    void Update()
    {
        if (isSlowed)
        {
            if (Time.timeSinceLevelLoad > slowEnd)
            {
                isSlowed = false;
                movementScript.speed *= 2;
            }
        }
    }

    public void Slow()
    {
        if (isSlowed) return;

        movementScript.speed /= 4f;
        slowEnd = Time.timeSinceLevelLoad + slowDuration;
        isSlowed = true;
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
