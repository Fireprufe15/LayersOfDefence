using UnityEngine;
using System.Collections;

public class Meshes : MonoBehaviour {

    public Mesh[] topMeshes;
    public Mesh[] middleMeshes;
    public Mesh[] bottemMeshes;

    public MeshFilter topPiece;
    public MeshFilter middlePiece;
    public MeshFilter bottomPiece;

    int topIndex = 0, middleIndex = 0, bottomIndex = 0;

    public void ChangeTop(int direction)
    {
        if (direction == 1)
        {
            topIndex++;
            if (topIndex > topMeshes.Length - 1)
            {
                topIndex = 0;
            }
        }
        else
        {
            topIndex--;
            if (topIndex < 0)
            {
                topIndex = topMeshes.Length - 1;
            }
        }

        topPiece.mesh = topMeshes[topIndex];
    }

    public void ChangeMiddle(int direction)
    {
        if (direction == 1)
        {
            middleIndex++;
            if (middleIndex > middleMeshes.Length - 1)
            {
                middleIndex = 0;
            }
        }
        else
        {
            middleIndex--;
            if (middleIndex < 0)
            {
                middleIndex = middleMeshes.Length - 1;
            }
        }

        middlePiece.mesh = middleMeshes[middleIndex];
    }

    public void ChangeBottom(int direction)
    {
        if (direction == 1)
        {
            bottomIndex++;
            if (bottomIndex > bottemMeshes.Length - 1)
            {
                bottomIndex = 0;
            }
        }
        else
        {
            bottomIndex--;
            if (bottomIndex < 0)
            {
                bottomIndex = bottemMeshes.Length - 1;
            }
        }

        bottomPiece.mesh = bottemMeshes[bottomIndex];
    }


}
