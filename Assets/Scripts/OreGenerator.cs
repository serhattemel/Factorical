using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    GridCell cell;
    readonly List<GameObject> Resources = new List<GameObject>();
    [SerializeField] GameObject object1, object2, object3;
    private Vector3 firstRangePositive, firstRangeNegative;
    private int _width,_height;
    private GameGrid gameGrid;
    private float rangeX, rangeY;
    public float magnification2 = 7f;
    public int x_offset = 0;
    public int y_offset = 0;
    private int resourceCount = 0;

    public void Start()
    {
        gameGrid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
        _width = gameGrid.Width;
        _height = gameGrid.Height;
        SetRange();
        InstantiateTree();
    }
    private void SetRange()
    {
        firstRangePositive.x = (_width / 2) + Mathf.FloorToInt(_width / 8) + 0.5f;
        firstRangePositive.y = (_height / 2) + Mathf.FloorToInt(_height / 8) + 0.5f;
        firstRangeNegative.x = (_width / 2) - Mathf.FloorToInt(_width / 8) + 0.5f;
        firstRangeNegative.y = (_height / 2) - Mathf.FloorToInt(_height / 8) + 0.5f;
    }
    int CalculateResource(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification2,
            (y - y_offset) / magnification2
            );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * (15);
        if (scaled_perlin == 15)
        {
            scaled_perlin = 14;
        }
        return Mathf.FloorToInt(scaled_perlin);
    }
    private void InstantiateTree()
    {
        rangeX = firstRangePositive.x - firstRangeNegative.x;
        rangeY = firstRangePositive.y - firstRangeNegative.y;

        for (int y = 0; y < rangeY; y++)
        {
            for (int x = 0; x < rangeX; x++)
            {
                cell = GameObject.Find((firstRangeNegative.x + x - 0.5f) + "," + (firstRangeNegative.y + y - 0.5f)).GetComponent<GridCell>();
                int tree = CalculateResource(x, y);
                if (cell.objectInThisGridSpace == null&& cell.transform.GetChild(0).name!= "ice")
                {
                    
                    
                    switch (tree)
                    {
                        case > 11:
                            Resources.Add(Instantiate(object3, new Vector3((firstRangeNegative.x + x), (firstRangeNegative.y + y), -0.5f), Quaternion.identity));
                            Resources[resourceCount].name = "ore " + resourceCount;
                            Resources[resourceCount].transform.SetParent(object2.transform);
                            cell.objectInThisGridSpace = Resources[resourceCount];
                            resourceCount++;
                            break;
                        case > 3:
                            break;
                        case < 3:
                            Resources.Add(Instantiate(object1, new Vector3((firstRangeNegative.x + x), (firstRangeNegative.y + y), -0.5f), Quaternion.identity));
                            Resources[resourceCount].name = "tree " + resourceCount;
                            Resources[resourceCount].transform.SetParent(object2.transform);
                            cell.objectInThisGridSpace = Resources[resourceCount];
                            resourceCount++;
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
