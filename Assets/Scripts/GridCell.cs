using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX;
    private int posY;

    public GameObject objectInThisGridSpace = null;

    public bool isOccupied = false;

    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }
    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
