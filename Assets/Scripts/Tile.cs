using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public CreateSudoku create;
    public Difficulty difficulty;
    int value = 0;
    public int originalValue;
    public int index;

    public void Reset()
    {
        value = originalValue;
        Assign(value);
    }

    public void Assign(int value)
    {
        if (!(value == 0))
        {
            text.text = value.ToString();
        }
        else
        {
            text.text = " ";
        }
    }

    public void OnButtonPress()
    {
        originalValue = transform.parent.GetComponent<Container>().originalGrid[index];
        if(originalValue == 0)
        {
            value++;
            if (value > 9)
            {
                value = 0;
            }
            if (!(value == 0))
            {
                text.text = value.ToString();
            }
            else
            {
                text.text = " ";
            }
            transform.parent.GetComponent<Container>().grid[index] = value;

            List<int[]> rows = new List<int[]>();
            for (int i = 0; i < 9; i++)
            {
                rows.Add(transform.parent.GetComponent<Container>().grid.SubArray(i * 9, 9));
            }

            if (Check(rows))
            {
                create.Create(difficulty.difficulty); 
            }
        }
        else
        {
            return;
        }
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
            if (row.Length != row.Distinct().Count())
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
        foreach (int[] column in columns)
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
