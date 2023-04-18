using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    GridCell cell;
    readonly List<GameObject> Resources = new List<GameObject>();
    [SerializeField] GameObject  object2, treePrefab, ore_blue, ore_red;
    private Vector3 firstRangePositive, firstRangeNegative;
    private Vector3 secondRangePositive, secondRangeNegative;
    private int _width,_height;
    private GameGrid gameGrid;
    private float firstRangeX, firstRangeY;
    private float secondRangeX, secondRangeY;
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
        InstantiateResource();
    }
    private void SetRange()
    {
        firstRangePositive.x = (_width / 2) + Mathf.FloorToInt(_width / 6) + 0.5f;
        firstRangePositive.y = (_height / 2) + Mathf.FloorToInt(_height / 6) + 0.5f;
        firstRangeNegative.x = (_width / 2) - Mathf.FloorToInt(_width / 6) + 0.5f;
        firstRangeNegative.y = (_height / 2) - Mathf.FloorToInt(_height / 6) + 0.5f;
        

    }
    int CalculateResource(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification2,
            (y - y_offset) / magnification2
            );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * (20);
        if (scaled_perlin == 20)
        {
            scaled_perlin = 19;
        }
        return Mathf.FloorToInt(scaled_perlin);
    }
    //private void InstantiateTree()
    //{
    //    firstRangeX = firstRangePositive.x - firstRangeNegative.x;
    //    firstRangeY = firstRangePositive.y - firstRangeNegative.y;


    //    for (int y = 0; y < firstRangeY; y++)
    //    {
    //        for (int x = 0; x < firstRangeX; x++)
    //        {
    //            cell = GameObject.Find((firstRangeNegative.x + x - 0.5f) + "," + (firstRangeNegative.y + y - 0.5f)).GetComponent<GridCell>();
    //            int tree = CalculateResource(x, y);
    //            if (cell.objectInThisGridSpace == null&& cell.transform.GetChild(0).name!= "sand")
    //            {
    //                switch (tree)
    //                {
    //                    case > 11:
    //                        Resources.Add(Instantiate(ore_blue, new Vector3((firstRangeNegative.x + x), (firstRangeNegative.y + y), -0.15f), Quaternion.identity));
    //                        Resources[resourceCount].name = "ore " + resourceCount;
    //                        Resources[resourceCount].transform.SetParent(object2.transform);
    //                        cell.objectInThisGridSpace = Resources[resourceCount];
    //                        Resources[resourceCount].gameObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Resources[resourceCount].gameObject.transform.position.y, Resources[resourceCount].gameObject.transform.position.z);
    //                        resourceCount++;
    //                        break;
    //                    case > 3:
    //                        break;
    //                    case < 3:
    //                        Resources.Add(Instantiate(treePrefab, new Vector3((firstRangeNegative.x + x), (firstRangeNegative.y + y), -0.5f), Quaternion.identity));
    //                        Resources[resourceCount].name = "tree " + resourceCount;
    //                        Resources[resourceCount].transform.SetParent(object2.transform);
    //                        cell.objectInThisGridSpace = Resources[resourceCount];
    //                        resourceCount++;
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //    }

        
        
    //}
    private void InstantiateResource()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                cell = GameObject.Find((x) + "," + (y)).GetComponent<GridCell>();
                int tree = CalculateResource(x, y);
                if (cell.objectInThisGridSpace == null && cell.transform.GetChild(0).name != "sand")
                {
                    if (x > firstRangeNegative.x && x < firstRangePositive.x && y > firstRangeNegative.y && y < firstRangePositive.y)
                    {
                        switch (tree)
                        {
                            case > 15:
                                Resources.Add(Instantiate(ore_blue, new Vector3((x + 0.5f), (y + 0.5f), -0.5f), Quaternion.Euler(0, 0, Random.Range(0, 360))));
                                Resources[resourceCount].name = "blue ore";
                                Resources[resourceCount].transform.SetParent(object2.transform);
                                cell.objectInThisGridSpace = Resources[resourceCount];
                                //Resources[resourceCount].gameObject.transform.rotation = Quaternion.Euler(Resources[resourceCount].gameObject.transform.position.x, Resources[resourceCount].gameObject.transform.position.y, Random.Range(0, 360));
                                resourceCount++;
                                break;
                                
                            case > 3:
                                break;

                            case < 3:
                                Resources.Add(Instantiate(treePrefab, new Vector3((x+0.5f), (y + 0.5f), -0.5f), Quaternion.identity));
                                Resources[resourceCount].name = "tree " + resourceCount;
                                Resources[resourceCount].transform.SetParent(object2.transform);
                                cell.objectInThisGridSpace = Resources[resourceCount];
                                resourceCount++;
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (tree)
                        {
                            case > 15:
                                Resources.Add(Instantiate(ore_red, new Vector3((x + 0.5f), (y + 0.5f), -0.5f), Quaternion.Euler(0, 0, Random.Range(0, 360))));
                                Resources[resourceCount].name = "red ore";
                                Resources[resourceCount].transform.SetParent(object2.transform);
                                cell.objectInThisGridSpace = Resources[resourceCount];
                                //Resources[resourceCount].gameObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Resources[resourceCount].gameObject.transform.position.y, Resources[resourceCount].gameObject.transform.position.z);
                                resourceCount++;
                                break;
                                
                            case > 2:
                                break;
                            case < 2:
                                Resources.Add(Instantiate(treePrefab, new Vector3((x + 0.5f), (y + 0.5f), -0.5f), Quaternion.identity));
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
}
