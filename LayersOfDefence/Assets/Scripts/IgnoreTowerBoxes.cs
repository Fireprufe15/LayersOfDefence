using UnityEngine;
using System.Collections;

public class IgnoreTowerBoxes : MonoBehaviour {
    
    void Start()
    {

    }
    void Update()
    {

    }
	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Tower")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<SphereCollider>(), other.gameObject.GetComponent<BoxCollider>());
        }
    }

}
