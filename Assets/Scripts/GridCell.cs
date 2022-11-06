using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX;
    private int posY;

    public GameObject objectInThisGridSpace = null;

    [SerializeField] private bool isOccupied = false;
    public void SetOccTrue()
    {
        isOccupied = true;
    }
    public bool GetOcc()
    {
        return isOccupied;
    }
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
