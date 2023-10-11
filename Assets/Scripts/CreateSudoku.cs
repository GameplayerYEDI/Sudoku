using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CreateSudoku : MonoBehaviour
{
    public Difficulty difficulty;
    public RemoveTiles remover;
    public Container sudoku;

    public void Create(byte difficulty)
    {
        sudoku.AssignAll(createSudoku(difficulty));
    }

    public List<int[]> createSudoku(byte difficulty)
    {
        int[] row1 = new int[9];
        int[] row2 = new int[9];
        int[] row3 = new int[9];

        int[] row4 = new int[9];
        int[] row5 = new int[9];
        int[] row6 = new int[9];

        int[] row7 = new int[9];
        int[] row8 = new int[9];
        int[] row9 = new int[9];

        row1 = SetFirstRow(row1);

        row2 = SetOtherRow(row1, 2);
        row3 = SetOtherRow(row2, 3);

        row4 = SetOtherRow(row3, 4);
        row5 = SetOtherRow(row4, 5);
        row6 = SetOtherRow(row5, 6);

        row7 = SetOtherRow(row6, 7);
        row8 = SetOtherRow(row7, 8);
        row9 = SetOtherRow(row8, 9);

        List<int[]> rows = new List<int[]>();
        rows.Add(row1);
        rows.Add(row2);
        rows.Add(row3);
        rows.Add(row4);
        rows.Add(row5);
        rows.Add(row6);
        rows.Add(row7);
        rows.Add(row8);
        rows.Add(row9);

        if (Check(rows))
        {
            return remover.Remove(rows, difficulty);
        }
        else
        {
            return createSudoku(difficulty);
        }
    }

    int[] SetFirstRow(int[] row)
    {
        List<int> values = new List<int>();
        values.Add(1);
        values.Add(2);
        values.Add(3);

        values.Add(4);
        values.Add(5);
        values.Add(6);

        values.Add(7);
        values.Add(8);
        values.Add(9);


        for(int i = 0; i<row.Length; i++)
        {
            int value = Random.Range(0, values.Count);
            row[i] = values[value];
            values.RemoveAt(value);
        }

        return row;
    }

    int[] SetOtherRow(int[] row, int rowNum)
    {
        int[] rowReturn = new int[9];
        if ((rowNum == 4) || (rowNum == 7))         // Shift rows 3 and 6 by positive 1 instead of 3
        {
            for (int i = 0; i < 9; i++)
            {
                if (i >= 1)
                {
                    rowReturn[i - 1] = row[i];
                }
                else
                {
                    rowReturn[8 - i] = row[i];
                }
            }
        }
        else                                        // Shift other rows by negative 3
        {
            for (int i = 0; i < 9; i++)
            {
                if (i > 2)
                {
                    rowReturn[i - 3] = row[i];
                }
                else
                {
                    rowReturn[8 - (2 - i)] = row[i];
                }
            }
        }

        return rowReturn;
    }

    public bool Check(List<int[]> rows)
    {
        // Set up column list
        List<int[]> columns = new List<int[]>();
        for (int i = 0; i < 9; i++)
        {
            int[] column = { rows[0][i], rows[1][i], rows[2][i], rows[3][i], rows[4][i], rows[5][i], rows[6][i], rows[7][i], rows[8][i] };
            columns.Add(column);
        }

        // Set up field list
        List<int[]> fields = new List<int[]>();
        for (int j = 0; j < 9; j++)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] field = { rows[j][i], rows[j][i + 1], rows[j][i + 2], /* Next row */ rows[j + 1][i], rows[j + 1][i + 1], rows[j + 1][i + 2], /* Next row */ rows[j + 2][i], rows[j + 2][i + 1], rows[j + 2][i + 2] };
                fields.Add(field);
                i += 2;
            }
            j += 2;
        }

        // Check if value is contained twice in any row, column or field
        bool okay = true;

        // Check rows
        foreach (int[] row in rows)
        {
            if(row.Length != row.Distinct().Count())
            {
                okay = false;
            }

            // Check for empty tiles
            if (row.Contains(0))
            {
                okay = false;
            }
        }

        // Check columns
        foreach(int[] column in columns)
        {
            if (column.Length != column.Distinct().Count())
            {
                okay = false;
            }
        }

        // Check field
        foreach (int[] field in fields)
        {
            if (field.Length != field.Distinct().Count())
            {
                okay = false;
            }
        }

        return okay;
    }
}
