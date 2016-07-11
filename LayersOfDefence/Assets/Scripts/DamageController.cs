using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

    [HideInInspector] public float currentHP;
    public float startingHP;
    public int slowDuration;
    public int goldPerKill;
    public AudioClip onDeath;
    public PlayerStats stats;
    public GameObject Explosion;

    [HideInInspector]
    public float goldMultiplier = 1f;
    [HideInInspector]
    public float damageMultiplier = 1f;
    [HideInInspector]
    public GameObject textObject;
    private TextMesh text;

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
        text = textObject.GetComponent<TextMesh>();                
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

    bool isDead = false;	

    public void DoDamage(int damage)
    {
        currentHP -= damage * damageMultiplier;
        text.text = (damage * damageMultiplier).ToString();
        text.color = Color.red;
        GameObject spawnedText = (GameObject)Instantiate(textObject, new Vector3(transform.position.x, transform.position.y+1.1f, transform.position.z), Quaternion.identity);
        spawnedText.GetComponent<MoveUpAndDie>().AttachedCreep = gameObject;
        if (currentHP <= 0)
        {
            stats.Gold += Mathf.RoundToInt(goldPerKill * goldMultiplier);
            text.text = (Mathf.RoundToInt(goldPerKill * goldMultiplier)).ToString();
            text.color = Color.yellow;            
            GameObject goldText = (GameObject)Instantiate(textObject, new Vector3(transform.position.x, transform.position.y + 2.1f, transform.position.z), Quaternion.identity);
            goldText.GetComponent<MoveUpAndDie>().AttachedCreep = gameObject;
            AudioSource.PlayClipAtPoint(onDeath, transform.position, 0.2f);
            if (!isDead)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                stats.CreepsOnMap--;
                isDead = true;
                Destroy(gameObject);
            }
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
