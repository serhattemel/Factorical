using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX;
    private int posY;

    [SerializeField] private GameObject objectInThisGridSpace = null;
    private GameObject oreInThisGridSpace;
    public GameObject ObjectInThisGridSpace { get => objectInThisGridSpace; set => objectInThisGridSpace = value; }
    public GameObject OreInThisGridSpace { get => oreInThisGridSpace; set => oreInThisGridSpace = value; }

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
