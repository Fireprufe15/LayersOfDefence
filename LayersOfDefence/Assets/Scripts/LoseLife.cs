using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoseLife : MonoBehaviour {
    
    PlayerStats playerStats;
    CamEffects shakeScript;
    public GameObject Explosion;

    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        shakeScript = Camera.main.GetComponent<CamEffects>();
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<DamageController>().onDeath, transform.position, 0.5f);
            Destroy(other.gameObject);
            playerStats.Lives--;
            playerStats.CreepsOnMap--;
            Instantiate(Explosion, transform.position, Quaternion.identity);
            shakeScript.ShakeCam();
        }
    }
}
