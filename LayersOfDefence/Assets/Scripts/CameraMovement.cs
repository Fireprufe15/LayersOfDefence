using UnityEngine;
using System.Collections;
using System;

public class CameraMovement : MonoBehaviour {

    [Range(1, 100)]
    public float panSpeed;
    [Range(1, 100)]
    public float scrollSpeed;
    [Header("Mouse Zoom")]
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;

    GameObject cam;

    void Start()
    {
        cam = Camera.main.gameObject;
    }

	void Update ()
    {
        bool isTop = Input.mousePosition.y >= Screen.height - 1 || Input.GetAxisRaw("Vertical") > 0f;
        bool isBottom = Input.mousePosition.y <= 0 || Input.GetAxisRaw("Vertical") < 0f;
        bool isRight = Input.mousePosition.x >= Screen.width - 1 || Input.GetAxisRaw("Horizontal") > 0f;
        bool isLeft = Input.mousePosition.x <= 0 || Input.GetAxisRaw("Horizontal") < 0f;

        PanVertical(isTop, isBottom);
        PanHorizontal(isLeft, isRight);

        Zoom();
	}

    void Zoom()
    {
        float yZoom = transform.position.y + Input.mouseScrollDelta.y * -zoomSpeed;
        float zZoom = transform.position.z - Input.mouseScrollDelta.y * -zoomSpeed;

        if (yZoom < maxZoom && yZoom > minZoom)
        {
            transform.position = new Vector3
               (
                   transform.position.x,
                   yZoom,
                   zZoom
               );
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
