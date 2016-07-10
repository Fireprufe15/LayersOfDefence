using UnityEngine;
using System.Collections;

public class ChangeColorOfBlock : MonoBehaviour {

    public ChangeColor changeColor;
    [HideInInspector] public GameObject block;
    Color c;

	// Use this for initialization
	void Start () {
        c = Color.white;
	}

	void UpdateBlock () {
        block.GetComponent<Renderer>().material.color = c;
        changeColor.isPicking = false;
    }

    public void ChangeRed(float value)
    {
        c.r = value;
        UpdateBlock();
    }
    public void ChangeGreed(float value)
    {
        c.g = value;
        UpdateBlock();
    }
    public void ChangeBlue(float value)
    {
        c.b = value;
        UpdateBlock();
    }

    public void close()
    {
        this.gameObject.SetActive(false);
    }
}
