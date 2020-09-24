using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private MazeBuilder mazeBuilder;

    private void Start()
    {
        mazeBuilder = FindObjectOfType<MazeBuilder>();
    }

    public void CameraReset()
    {

        GameObject maze = GameObject.Find("MazeContainer");

        //We take the center floor object of the maze, we need to set as a center of the camera
        Vector3 center = maze.transform.Find("Floor" + mazeBuilder.rows / 2 + mazeBuilder.columns / 2).transform.position;

        float cameraY = 0.0f;

        if (mazeBuilder.rows >= mazeBuilder.columns)
        {
            cameraY = mazeBuilder.rows;
        }
        else
        {
            cameraY = mazeBuilder.columns;
        }

        //Set camera transform
        transform.position = new Vector3(center.x, cameraY * 3.5f, center.z);
    }
}
