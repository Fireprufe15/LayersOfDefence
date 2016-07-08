using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

    [Range(1, 100)]
    public float panSpeed;
    [Range(1, 100)]
    public float scrollSpeed;

    GameObject cam;

    void Start()
    {
        cam = Camera.main.gameObject;
    }

	void Update ()
    {
        bool isTop = Input.mousePosition.y >= Screen.height - 1;
        bool isBottom = Input.mousePosition.y <= 0;
        bool isRight = Input.mousePosition.x >= Screen.width - 1;
        bool isLeft = Input.mousePosition.x <= 0;

        PanVertical(isTop, isBottom);
        PanHorizontal(isLeft, isRight);

        float d = Input.GetAxis("Mouse ScrollWheel");
        Zoom(d);
	}

    private void Zoom(float d)
    {
        if (d < 0f)
        {
            // scroll up
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + panSpeed * Time.deltaTime, cam.transform.position.z - panSpeed * Time.deltaTime);
        }
        else if (d > 0f)
        {
            // scroll down
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - panSpeed * Time.deltaTime, cam.transform.position.z + panSpeed * Time.deltaTime);
        }
    }

    void PanVertical(bool isTop, bool isBottom)
    {
        float z = 0f;

        if (isTop)
        {
            z += panSpeed * Time.deltaTime;
        }
        else if(isBottom)
        {
            z -= panSpeed * Time.deltaTime;
        }

        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, z + cam.transform.position.z);
    }

    void PanHorizontal(bool isLeft, bool isRight)
    {
        float x = 0f;

        if (isLeft)
        {
            x -= panSpeed * Time.deltaTime;
        }
        else if (isRight)
        {
            x += panSpeed * Time.deltaTime;
        }

        cam.transform.position = new Vector3(x + cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
}
