using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

    public float health;
    public int slowDuration;
    public int goldPerKill;
    public PlayerStats stats;

    [HideInInspector]
    public float goldMultiplier = 1f;
    [HideInInspector]
    public float damageMultiplier = 1f;

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
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
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
        health -= damage*damageMultiplier;
        if (health <= 0)
        {
            stats.Gold += Mathf.RoundToInt(goldPerKill*(float)goldMultiplier);
            Destroy(self);
        }
        else
        {
            goldMultiplier = 1;
        }
    }

    public void SustainedDamage(int damage)
    {
        sustainedDamageAmount = damage;
        sustainedDamage = true;
        sustainedDamageNext = Time.timeSinceLevelLoad + sustainedDamageDelay;
    }
}
