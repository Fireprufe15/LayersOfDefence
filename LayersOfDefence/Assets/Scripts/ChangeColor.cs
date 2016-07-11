using UnityEngine;
using System.Collections;
using System;

public class ChangeColor : MonoBehaviour {
    
    public GameObject colourPicker;
    [HideInInspector] public bool isPicking = false;

	// Use this for initialization
	void Start () {
	
	}

    bool isFirst = true;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && !isPicking)
        {
            GameObject index = GetBlockIndex();
            if (!index)
            {
                return;
            }
            else
            {
                if (isFirst)
                {
                    isFirst = false;
                    return;
                }

                isPicking = true;
                ShowColorPicker(index);
            }
        }
	}

    private void ShowColorPicker(GameObject obj)
    {
        colourPicker.SetActive(true);
        colourPicker.GetComponent<ChangeColorOfBlock>().block = obj;
    }

    GameObject GetBlockIndex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 300f, LayerMask.GetMask("CustomizeTower")))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
