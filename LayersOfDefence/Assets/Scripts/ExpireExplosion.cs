using UnityEngine;
using System.Collections;

public class ExpireExplosion : MonoBehaviour {

    float expireTime;
    public float expireTimeAmt;

	// Use this for initialization
	void Start () {
        expireTime = Time.timeSinceLevelLoad + expireTimeAmt;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad > expireTime)
        {
            Destroy(gameObject);
        }
	}
}
