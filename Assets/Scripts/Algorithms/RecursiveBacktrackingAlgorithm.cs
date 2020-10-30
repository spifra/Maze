using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktrackingAlgorithm
{
    #region PRIVATE AND NOT SERIALIZED
    private Stack<MazeCell> cellsStack;

    private int rows;
    private int columns;

    private int currentRow;
    private int currentColumn;

    private MazeBuilder mazeBuilder;

    private MazeCell[,] grid;
    #endregion

    private AlgorithmUtilities utilities;

    // Reset parameters
    public void ResetRecursiveBacktracking(MazeCell[,] grid, MazeBuilder mazeBuilder, int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.mazeBuilder = mazeBuilder;
        this.grid = grid;

        currentColumn = 0;
        currentRow = 0;

        cellsStack = new Stack<MazeCell>();

        cellsStack.Push(grid[currentRow, currentColumn]);

        grid[currentRow, currentColumn].visited = true;

        utilities = new AlgorithmUtilities(rows, columns, grid);

    }


    /// <summary>
    /// - set the first element of the stack (LIFO) as the current grid
    /// - while there are adjacent valid cell we get a random direction and try to build a path
    /// - if the while condition isn't true we call the funcion again to check the previous element of the path
    /// </summary>
    public void RecursiveBacktracking()
    {
        grid[currentRow, currentColumn] = cellsStack.Pop();

        currentColumn = grid[currentRow, currentColumn].column;
        currentRow = grid[currentRow, currentColumn].row;

        while (utilities.AreThereUnvisitedNeighbors(currentRow, currentColumn))
        {
            int direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0: //check up
                    if (utilities.IsCellValidAndUnvisited(currentRow - 1, currentColumn))
                    {
                        if (grid[currentRow, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].upWall);
                        }
                        currentRow--;

                        //Set the next cell to check
                        SetNextCell();

                        if (grid[currentRow, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].downWall);
                        }
                    }
                    break;
                case 1: //check down
                    if (utilities.IsCellValidAndUnvisited(currentRow + 1, currentColumn))
                    {
                        if (grid[currentRow, currentColumn].downWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].downWall);
                        }

                        currentRow++;

                        SetNextCell();


                        if (grid[currentRow, currentColumn].upWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].upWall);
                        }
                    }
                    break;
                case 2: //check left
                    if (utilities.IsCellValidAndUnvisited(currentRow, currentColumn - 1))
                    {

                        if (grid[currentRow, currentColumn].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].leftWall);
                        }

                        currentColumn--;

                        SetNextCell();

                        if (grid[currentRow, currentColumn].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].rightWall);
                        }
                    }
                    break;
                case 3: //check right
                    if (utilities.IsCellValidAndUnvisited(currentRow, currentColumn + 1))
                    {
                        if (grid[currentRow, currentColumn].rightWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].rightWall);
                        }

                        currentColumn++;

                        SetNextCell();

                        if (grid[currentRow, currentColumn].leftWall)
                        {
                            mazeBuilder.DestroyGameObject(grid[currentRow, currentColumn].leftWall);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
        if (cellsStack.Count > 0)
        {
            RecursiveBacktracking();
        }
        else
        {
            return;
        }
    }

    /// Set the next cell to check
    void SetNextCell()
    {

        grid[currentRow, currentColumn].visited = true;

        grid[currentRow, currentColumn].row = currentRow;
        grid[currentRow, currentColumn].column = currentColumn;

        cellsStack.Push(grid[currentRow, currentColumn]);

    }

}
