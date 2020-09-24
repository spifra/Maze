using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAndKillAlgorithm 
{
    private bool scanComplete;

    private int currentRow;
    private int currentColumn;

    private MazeCell[,] grid;

    private MazeBuilder mazeBuilder;

    private int rows;
    private int columns;

    //Reset Hunt and kill algorithm
    public void ResetHuntAndKill(MazeCell[,] grid, MazeBuilder mazeBuilder, int rows, int columns)
    {
        scanComplete = false;
        currentColumn = 0;
        currentRow = 0;

        this.grid = grid;
        this.mazeBuilder = mazeBuilder;
        this.rows = rows;
        this.columns = columns;

        grid[currentRow, currentColumn].visited = true;
    }

    /// <summary>
    /// - while there are more unvisited cells we call Walk and Hunt methods
    /// </summary>
    public void HuntAndKill()
    {
        while (!scanComplete)
        {
            Walk();
            Hunt();
        }
    }

    /// <summary>
    /// Method to create a path in the maze
    /// + while there are valid adjacent cells and unvisited:
    /// - we take a random direction and we destroy the walls between the two cells
    /// - we set the current row/column on the new cell (we move on the new cell)
    /// - we repeat this operation until the while condition is true
    /// </summary>
    void Walk()
    {
        while (AreThereUnvisitedNeighbors())
        {
            int direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0: //check up
                    if (IsCellValidAndUnvisited(currentRow - 1, currentColumn))
                    {
                        if (grid[currentRow, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].upWall);
                        }
                        currentRow--;
                        grid[currentRow, currentColumn].visited = true;

                        if (grid[currentRow, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].downWall);
                        }
                    }
                    break;
                case 1: //check down
                    if (IsCellValidAndUnvisited(currentRow + 1, currentColumn))
                    {
                        if (grid[currentRow, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].downWall);
                        }

                        currentRow++;

                        grid[currentRow, currentColumn].visited = true;

                        if (grid[currentRow, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].upWall);
                        }
                    }
                    break;
                case 2: //check left
                    if (IsCellValidAndUnvisited(currentRow, currentColumn - 1))
                    {

                        if (grid[currentRow, currentColumn].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].leftWall);
                        }

                        currentColumn--;
                        grid[currentRow, currentColumn].visited = true;

                        if (grid[currentRow, currentColumn].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].rightWall);
                        }
                    }
                    break;
                case 3: //check right
                    if (IsCellValidAndUnvisited(currentRow, currentColumn + 1))
                    {
                        if (grid[currentRow, currentColumn].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].rightWall);
                        }

                        currentColumn++;
                        grid[currentRow, currentColumn].visited = true;

                        if (grid[currentRow, currentColumn].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].leftWall);
                        }
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Method to search for the first unvisited cell with visited adjacent cell
    /// - for each cell in the maze:
    /// - if the cell is not visited and there visited adjacent cells
    /// - set mark the cell as visited.
    /// - set the scanComplete to false (to continue to walk and hunt) we move into the first cell which meet the if condition above
    /// - call DestroyAdjacentWall
    /// - exit from the method
    /// </summary>
    void Hunt()
    {
        scanComplete = true;

        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                if (!grid[i, j].visited && AreThereVisitedNeighbors(i, j))
                {
                    scanComplete = false;
                    currentRow = i;
                    currentColumn = j;
                    grid[currentRow, currentColumn].visited = true;
                    DestroyAdjacentWall();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Method to destroy a random wall between an unvisited cell and a visited cell
    /// + while no wall is destroy:
    /// - get a random direction
    /// - we take a random direction and we destroy the walls between the two cells
    /// - we set destroyed to true
    /// </summary>

    void DestroyAdjacentWall()
    {
        bool destroyed = false;

        while (!destroyed)
        {
            int direction = Random.Range(0, 4);
            switch (direction)
            {
                case 0: //check up
                    if (currentRow > 0 && grid[currentRow - 1, currentColumn].visited)
                    {
                        if (grid[currentRow - 1, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow - 1, currentColumn].downWall);
                        }
                        if (grid[currentRow, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].upWall);
                        }

                        destroyed = true;
                    }
                    break;
                case 1: //check down
                    if (currentRow < rows - 1 && grid[currentRow + 1, currentColumn].visited)
                    {
                        if (grid[currentRow + 1, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow + 1, currentColumn].upWall);

                        }
                        if (grid[currentRow, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].downWall);

                        }

                        destroyed = true;

                    }
                    break;
                case 2: //check left
                    if (currentColumn > 0 && grid[currentRow, currentColumn - 1].visited)
                    {

                        if (grid[currentRow, currentColumn - 1].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn - 1].rightWall);

                        }
                        if (grid[currentRow, currentColumn].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].leftWall);

                        }
                        destroyed = true;

                    }
                    break;
                case 3: //check right
                    if (currentColumn < columns - 1 && grid[currentRow, currentColumn + 1].visited)
                    {

                        if (grid[currentRow, currentColumn + 1].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn + 1].leftWall);

                        }
                        if (grid[currentRow, currentColumn].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].rightWall);

                        }

                        destroyed = true;
                    }
                    break;
            }
        }
    }

    //Check if the adjacent cells are valid or unvisited
    bool AreThereUnvisitedNeighbors()
    {

        if (IsCellValidAndUnvisited(currentRow - 1, currentColumn))
        {
            return true;
        }
        if (IsCellValidAndUnvisited(currentRow + 1, currentColumn))
        {
            return true;
        }
        if (IsCellValidAndUnvisited(currentRow, currentColumn - 1))
        {
            return true;
        }
        if (IsCellValidAndUnvisited(currentRow, currentColumn + 1))
        {
            return true;
        }

        return false;
    }

    //Check if cell as visited adjacent cells
    bool AreThereVisitedNeighbors(int row, int column)
    {
        if (row > 0 && grid[row - 1, column].visited)
        {
            return true;
        }
        if (row < rows - 1 && grid[row + 1, column].visited)
        {
            return true;
        }
        if (column > 0 && grid[row, column - 1].visited)
        {
            return true;
        }
        if (column < columns - 1 && grid[row, column + 1].visited)
        {
            return true;
        }

        return false;
    }

    //Check if the cell is valid and unvisited
    bool IsCellValidAndUnvisited(int row, int column)
    {
        if (row >= 0 && row < rows && column >= 0 && column < columns && !grid[row, column].visited)
        {
            return true;
        }
        return false;
    }
}
