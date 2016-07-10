using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

    public int health;
    public int slowDuration;

    bool isSlowed;
    bool sustainedDamage;
    GameObject self;
    Nav movementScript;
    float slowEnd = 0f;
    int sustainedDamageAmount;
    float sustainedDamageDelay = 1f;
    float sustainedDamageNext;

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
        if (sustainedDamage)
        {
            if (Time.timeSinceLevelLoad > sustainedDamageNext)
            {
                DoDamage(sustainedDamageAmount);
                sustainedDamageNext = Time.timeSinceLevelLoad + sustainedDamageDelay;
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

    public void SustainedDamage(int damage)
    {
        sustainedDamageAmount = damage;
        sustainedDamage = true;
        sustainedDamageNext = Time.timeSinceLevelLoad + sustainedDamageDelay;
    }
}
