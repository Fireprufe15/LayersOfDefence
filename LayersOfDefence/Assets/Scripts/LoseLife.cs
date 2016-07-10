using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoseLife : MonoBehaviour {
    
    PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            playerStats.Lives--;
            playerStats.CreepsOnMap--;
        }
    }
}
