using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmUtilities
{
    public int rows;
    public int columns;
    public MazeCell[,] grid;

    public AlgorithmUtilities(int rows, int columns, MazeCell[,] grid)
    {
        this.rows = rows;
        this.columns = columns;
        this.grid = grid;
    }

    //Check if the cell is valid and unvisited
    public bool IsCellValidAndUnvisited(int currentRow, int currentColumn)
    {
        if (currentRow >= 0 && currentRow < rows && currentColumn >= 0 && currentColumn < columns && !grid[currentRow, currentColumn].visited)
        {
            return true;
        }
        return false;
    }

    //Check if cell as visited adjacent cells
    public bool AreThereVisitedNeighbors(int currentRow, int currentColumn)
    {
        if (currentRow > 0 && grid[currentRow - 1, currentColumn].visited)
        {
            return true;
        }
        if (currentRow < rows - 1 && grid[currentRow + 1, currentColumn].visited)
        {
            return true;
        }
        if (currentColumn > 0 && grid[currentRow, currentColumn - 1].visited)
        {
            return true;
        }
        if (currentColumn < columns - 1 && grid[currentRow, currentColumn + 1].visited)
        {
            return true;
        }

        return false;
    }


    //Check if the adjacent cells are valid or unvisited
    public bool AreThereUnvisitedNeighbors(int currentRow, int currentColumn)
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


}
