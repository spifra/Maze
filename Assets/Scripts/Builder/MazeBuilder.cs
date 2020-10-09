using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeBuilder : MonoBehaviour
{
    /*Prefabs info*/
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject floorPrefab;

    [HideInInspector]
    public MazeCell[,] grid;

    [HideInInspector]
    public int rows;
    [HideInInspector]
    public int columns;

    #region PRIVATE AND NOT SERIALIZED
    private float wallSize;
    private float floorSize;

    //maze container in scene
    private GameObject mazeContainer;

    private HuntAndKillAlgorithm hek;
    private RecursiveBacktrackingAlgorithm rb;
    #endregion

    private void Awake()
    {
        //we take the width of the predfabs wall and floor
        wallSize = wallPrefab.transform.localScale.x;
        floorSize = floorPrefab.transform.localScale.x;

        //Parent GameObject for walls and floor
        mazeContainer = GameObject.Find("MazeContainer");
    }

    //Called by UI script when Build button is pressed
    public void BuildMaze(int r, int c, int algorithm)
    {
        //destroy the previous maze
        foreach (Transform t in mazeContainer.transform)
        {
            Destroy(t.gameObject);
        }

        //rows and columns from input
        rows = r;
        columns = c;

        //set the number of cells in the maze
        grid = new MazeCell[rows, columns];

        //create grid with walls and floor
        Builder();

        //Use algorthm from input
        UseAlgorithmFromInput(algorithm);

        //call the method to reset the camera position
        CameraFollow camera = Camera.main.GetComponent<CameraFollow>();
        camera.CameraReset();
    }

    /// <summary>
    /// - for each cell of the maze we create the floor and the walls.
    /// - we give to every cell the references of its walls
    /// - if it's the first or the last cell we destroy the wall to make an entrace/exit
    /// </summary>
    void Builder()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {

                GameObject floor = Instantiate(floorPrefab, new Vector3(j * floorSize, 0, -i * floorSize), Quaternion.identity, mazeContainer.transform);
                floor.name = "Floor" + i + j;

                GameObject horizontalWall = Instantiate(wallPrefab, new Vector3(j * wallSize, 0, -i * wallSize + 1.25f), Quaternion.identity, mazeContainer.transform);
                horizontalWall.name = "Orizontal Wall" + i + j;

                GameObject verticalWall = Instantiate(wallPrefab, new Vector3(j * wallSize, 0, -i * wallSize - 1.25f), Quaternion.identity, mazeContainer.transform);
                verticalWall.name = "Vertical Wall" + i + j;

                GameObject leftWall = Instantiate(wallPrefab, new Vector3(j * wallSize - 1.25f, 0, -i * wallSize), Quaternion.Euler(0, 90, 0), mazeContainer.transform);
                leftWall.name = "Left Wall" + i + j;

                GameObject rightWall = Instantiate(wallPrefab, new Vector3(j * wallSize + 1.25f, 0, -i * wallSize), Quaternion.Euler(0, 90, 0), mazeContainer.transform);
                rightWall.name = "Right Wall" + i + j;

                //Cell references
                grid[i, j] = new MazeCell();

                grid[i, j].upWall = horizontalWall;
                grid[i, j].downWall = verticalWall;
                grid[i, j].leftWall = leftWall;
                grid[i, j].rightWall = rightWall;

                if (i == 0 && j == 0)
                {
                    Destroy(leftWall);
                }
                else if (i == rows - 1 && j == columns - 1)
                {
                    Destroy(rightWall);
                }
            }
        }

    }

    void UseAlgorithmFromInput(int value)
    {
        switch (value)
        {
            case 0: // Hunt and Kill

                hek = new HuntAndKillAlgorithm();

                //reset
                hek.ResetHuntAndKill(grid, this, rows, columns);

                //Hunt and kill algorithm
                hek.HuntAndKill();
                break;

            case 1: //Recursive Backtracking 
                rb = new RecursiveBacktrackingAlgorithm();

                rb.ResetRecursiveBacktracking(grid, this, rows, columns);

                rb.RecursiveBacktracking();
                break;
            default:
                break;
        }
    }

    public void DestroyGameObject(GameObject go)
    {
        Destroy(go);
    }
}
