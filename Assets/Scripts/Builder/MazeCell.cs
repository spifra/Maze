using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell
{
    public bool visited = false;

    public GameObject upWall;
    public GameObject downWall;
    public GameObject leftWall;
    public GameObject rightWall;


    public int row = 0;
    public int column = 0;
}
