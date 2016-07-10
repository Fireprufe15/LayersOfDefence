using UnityEngine;
using System.Collections;

public class MoveUpAndDie : MonoBehaviour {
    [HideInInspector]
    public GameObject AttachedCreep;
    float spawnedTime;
    TextMesh thisText;
    // Use this for initialization
    void Start () {
        spawnedTime = Time.timeSinceLevelLoad;
        thisText = gameObject.GetComponent<TextMesh>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad - spawnedTime > 0.5f)
        {
            Destroy(this.gameObject);
        }
        if (AttachedCreep != null)
        {
            gameObject.transform.position = new Vector3(AttachedCreep.transform.position.x, gameObject.transform.position.y, AttachedCreep.transform.position.z);
        }
        gameObject.transform.Translate(0, 3f * Time.deltaTime, 0);         
        thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, thisText.color.a - 0.5f*Time.deltaTime);
	}

    
}
