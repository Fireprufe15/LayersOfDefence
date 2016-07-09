using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nav : MonoBehaviour {

    [HideInInspector]
    public List<Vector3> points;
    [HideInInspector]
    public bool isNavigating;

    Rigidbody playerRB;
    int currentPoint, lastPoint;
    public float speed = 5f;

    bool isMoving = false;

	// Use this for initialization
	void Start ()
    {
        // leave here
        currentPoint = 0;
        lastPoint = points.Count;
        playerRB = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        if (!isNavigating) return;

        if (new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z))  == points[currentPoint])
        {
            if (!(currentPoint + 1 > points.Count - 1))
            {
                currentPoint++;
            }
            else
            {
                isNavigating = false;
                playerRB.velocity = Vector3.zero;
                return;
            }
        }

        // Navigate
        Vector3 direction = (points[currentPoint] - transform.position).normalized;
        playerRB.velocity = direction * speed;
    }
}
