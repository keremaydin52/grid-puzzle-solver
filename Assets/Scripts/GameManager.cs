using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Grid Items")]
    [SerializeField] private Transform gridTransform;
    [SerializeField] private GameObject gridCell;

    private int poolStartingSize = 10;

    private int rowCount;
    private int columnCount;
    private int maxIndex;
    private int currentIndex;   

    private GridCell[,] grid;

    // This array is to calculate total number of 0's and 1's in a row
    private Dictionary<int, int>[] rowCounters;

    // This array is to calculate total number of 0's and 1's in a cloumn
    private Dictionary<int, int>[] columnCounters;

    private void Start()
    {
        GridCell.gridCellPool = new Pooler(gridCell, poolStartingSize);
    }

    public void GenerateGrid()
    {
        rowCount = UIManager.Instance.RowNumber;
        columnCount = UIManager.Instance.ColumnNumber;

        GenerateGrid(rowCount, columnCount);
    }

    private void GenerateGrid(int rowCount, int columnCount)
    {
        maxIndex = (rowCount * columnCount) - 1;

        grid = new GridCell[rowCount, columnCount];

        for (int x = 0; x < rowCount; x++)
        {
            for (int y = 0; y < columnCount; y++)
            {
                GameObject cell = GridCell.gridCellPool.Get(Vector3.zero, Quaternion.identity);
                cell.SetActive(true);
                cell.transform.SetParent(gridTransform);
                Vector3 cellPosition = new Vector3(y * 50, -x * 50, 0f);
                cell.GetComponent<RectTransform>().anchoredPosition = cellPosition;
                GridCell gridCell = cell.GetComponent<GridCell>();
                grid[x, y] = gridCell;
                gridCell.Value = -1; // Populate cells with -1
            }
        }
    }

    public void GenerateSolution()
    {
        currentIndex = 0;

        // Use a dictionary to keep total number of 0's and 1's
        rowCounters = Enumerable.Range(0, rowCount).Select(i => new Dictionary<int, int>() { { 0, 0 }, { 1, 0 } }).ToArray();
        columnCounters = Enumerable.Range(0, columnCount).Select(i => new Dictionary<int, int>() { { 0, 0 }, { 1, 0 } }).ToArray();
        Backtrack();
    }

    private bool Backtrack()
    {
        if (currentIndex > maxIndex) return true;

        int row = GetRowIndex(currentIndex);
        int column = GetColumnIndex(currentIndex);

        int nextIndex = currentIndex + 1;
        int randomValue = Random.Range(0, 2); // Get a random number, either 0 or 1

        int[] values = { randomValue, 1 - randomValue };

        // First try the random number, if it would not work then try the other one
        for (int i = 0; i <= 1; i++)
        {
            int value = values[i];
            if (!IsCellAvailableForValue(value)) continue;

            // If cell is available for the value then update the cell with the value
            grid[row, column].Value = value;
            rowCounters[row][value]++;
            columnCounters[column][value]++;

            currentIndex = nextIndex;
            if (Backtrack()) return true;

            // If Backtrack return false then go to the previous step
            currentIndex = currentIndex - 1;
            grid[row, column].Value = -1;
            rowCounters[row][value]--;
            columnCounters[column][value]--;
        }

        return false;
    }

    /// <summary>
    /// Check if a value is available for the current cell 
    /// </summary>
    /// <param name="value"></param>
    /// This value should be either 0 or 1
    /// <returns></returns>
    private bool IsCellAvailableForValue(int value)
    {
        int row = GetRowIndex(currentIndex);
        int column = GetColumnIndex(currentIndex);

        // if 0 or 1 count is equal to the either half of the column or row count then no need to continue 
        if (rowCounters[row][value] == columnCount / 2) return false;
        if (columnCounters[column][value] == rowCount / 2) return false;

        // Check if there are consecutive values on the left
        if (row > 1 && grid[row - 1, column].Value == value && grid[row - 2, column].Value == value)
        {
            return false;
        }

        // Check if there are consecutive values on top
        if (column > 1 && grid[row, column - 1].Value == value && grid[row, column - 2].Value == value)
        {
            return false;
        }

        return true;
    }

    private int GetRowIndex(int i) => Mathf.FloorToInt(i / columnCount);
    private int GetColumnIndex(int i) => Mathf.FloorToInt(i % columnCount);

    /// <summary>
    /// First release all cells before creating a new grid
    /// </summary>
    public void ReleaseAllGridCells()
    {
        if(grid == null) return;

        for (int x = 0; x < rowCount; x++)
        {
            for (int y = 0; y < columnCount; y++)
            {
                if (grid[x, y] != null)
                {
                    GridCell.gridCellPool.Free(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
                
            }
        }
    }
}