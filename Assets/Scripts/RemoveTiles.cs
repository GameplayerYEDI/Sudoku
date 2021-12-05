using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RemoveTiles : MonoBehaviour
{
    public List<int[]> Remove(List<int[]> rows, byte difficulty)
    {
        foreach(int[] row in rows)
        {
            int countRemove = 0;
            switch (difficulty)
            {
                case 0:
                    countRemove = Random.Range(2, 6);
                    break;
                case 1:
                    countRemove = Random.Range(4, 7);
                    break;
                case 2:
                    countRemove = Random.Range(6, 8);
                    break;
            }
            List<int> removed = new List<int>();
            while(removed.Count < countRemove)
            {
                int removePos = Random.Range(0,9);
                if(!removed.Contains(removePos))
                {
                    row[removePos] = 0;
                    removed.Add(removePos);
                }
            }
        }
        return rows;
    }
}
