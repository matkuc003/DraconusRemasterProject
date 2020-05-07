using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridScript : MonoBehaviour
{
    private bool visibleHiddenWall = false;
    public Tilemap tilemap;

    public void setVisibleHiddenWall(bool visible)
    {
        visibleHiddenWall = visible;
        changeVisibleWall();
    }

    void changeVisibleWall()
    {
        if (visibleHiddenWall)
        {
            Vector3Int currentCell = tilemap.WorldToCell(transform.position);
            for(int i=82; i<=85; i++)
            {
                for(int j=-11; j>=-13; j--)
                {
                    currentCell.x = i;
                    currentCell.y = j;
                    tilemap.SetTile(currentCell, null);
                }

                for (int j = -42; j >= -45; j--)
                {
                    currentCell.x = i;
                    currentCell.y = j;
                    tilemap.SetTile(currentCell, null);
                }
            }
        }
    }
}
