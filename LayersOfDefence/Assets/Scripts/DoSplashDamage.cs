using UnityEngine;
using System.Collections;

public class DoSplashDamage : MonoBehaviour {

    public float sustainTime = 0.1f;
    [HideInInspector]
    public int damage;
	// Use this for initialization
	void Start () {
        sustainTime = sustainTime + Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > sustainTime )
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            DamageController dmg = other.gameObject.GetComponent<DamageController>();
            dmg.DoDamage(damage);
        }        
    }
}
