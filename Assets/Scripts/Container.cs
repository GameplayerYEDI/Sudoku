using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static T[] SubArray<T>(this T[] array, int offset, int length)
    {
        T[] result = new T[length];
        Array.Copy(array, offset, result, 0, length);
        return result;
    }
}

public class Container : MonoBehaviour
{

    public CreateSudoku create;

    public int[] grid = new int[81];
    public int[] originalGrid = new int[81];

    public int screenSpace = (Screen.width + Screen.height) / 2;
    int temp = 0;

    private void Update()
    {
        screenSpace = (Screen.width + Screen.height) / 2;
        if(temp == 0)
        {
            create.Create(create.difficulty.difficulty);
            temp++;
        }
    }

    public void Assign(int value, int childIndex)
    {
        Transform childTransform = transform.GetChild(childIndex);
        GameObject child = childTransform.gameObject;
        Tile tile = child.GetComponent<Tile>();
        tile.Assign(value);
    }

    public void AssignAll(List<int[]> rows)
    {
        ResetTiles();
        for (int y = 0; y < rows.Count; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                Transform childTransform = transform.GetChild(x+y*9);
                GameObject child = childTransform.gameObject;
                Tile tile = child.GetComponent<Tile>();
                grid[x + y * 9] = rows[x][y];
                originalGrid[x + y * 9] = rows[x][y];
                tile.originalValue = rows[x][y];
                tile.index = x + y * 9;
                tile.Reset();
            }
        }
    }

    public void ResetTiles()
    {
        for (int y = 0; y<transform.childCount/9; y++)
        {
            for (int x = 0; x < transform.childCount / 9; x++)
            {
                int xOffset = 0;
                int yOffset = 0;

                if (x >= 3)
                {
                    xOffset = screenSpace / 45;
                    if (x >= 6)
                    {
                        xOffset += screenSpace / 45;
                    }
                }

                if(y >= 3)
                {
                    yOffset = screenSpace / 45;
                    if (y >= 6)
                    {
                        yOffset += screenSpace / 45;
                    }
                }


                Transform childTransform = transform.GetChild(x + y * 9);
                childTransform.transform.position = new Vector3(x * screenSpace/14 + xOffset + screenSpace / 6, -(y * screenSpace / 14 + yOffset) + (screenSpace / 1.5f), 0);
            }
        }
    }
}
