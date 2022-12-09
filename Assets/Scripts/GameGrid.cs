using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    GridCell cell;
    readonly List<GameObject> Resources = new List<GameObject>();
    private int resourceCount = 0;
    private Vector3 firstRangePositive, firstRangeNegative;
    public float rangeX, rangeY;
    [SerializeField] GameObject object1,object2,object3;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    private float _gridSpaceSize = 1f;

    public List<GameObject> terrainList = new List<GameObject>();
    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }
    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }
    public GameObject Prefab1;

    private GameObject[,] _gameGrid;
    public int x_offset= 0;
    public int y_offset= 0;
    public float magnification= 7f;
    public float magnification2= 7f;
    public Vector2Int GetPosition()
    {
        return new Vector2Int(_width,_height);
    }

    [System.Obsolete]
    void Start()
    {
       
        x_offset = Random.RandomRange(-_width,0);
        y_offset = Random.RandomRange(-_height,0);
        CreateGrid();
        SetRange();
        InstantiateTree();
    }
    private void SetRange()
    {
        firstRangePositive.x = (_width / 2) + Mathf.FloorToInt(_width / 8) + 0.5f;
        firstRangePositive.y = (_height / 2) + Mathf.FloorToInt(_height / 8) + 0.5f;
        firstRangeNegative.x = (_width / 2) - Mathf.FloorToInt(_width / 8) + 0.5f;
        firstRangeNegative.y = (_height / 2) - Mathf.FloorToInt(_height / 8 ) + 0.5f;
    }
    private void CreateGrid()
    {
        _gameGrid = new GameObject[_width, _height];
        if(terrainList[0] == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab not assigned");
        }

        for(int y = 0; y <_height ; y++)
        {
            for(int x=0; x < _width; x++)
            {
                int terrainNumber = CalculateTerrain(x, y);
                _gameGrid[x, y] = Instantiate(terrainList[terrainNumber], new Vector3(x * _gridSpaceSize, y * _gridSpaceSize), Quaternion.identity);
                _gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                _gameGrid[x, y].transform.parent = transform;
                _gameGrid[x, y].gameObject.name = x.ToString()+","+y.ToString();
                _gameGrid[x, y].gameObject.tag = "GridCell";
            }
        }
    }
    int CalculateTerrain(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x-x_offset)/magnification,
            (y-y_offset)/magnification
            );

        float  clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * (terrainList.Count);
        if (scaled_perlin == terrainList.Count)
        {
            scaled_perlin = terrainList.Count-1;
        }

        
        return Mathf.FloorToInt(scaled_perlin);
    }
    int CalculateResource(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x-x_offset)/magnification2,
            (y-y_offset)/magnification2
            );

        float  clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
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
                
                cell = GameObject.Find((firstRangeNegative.x + x-0.5f) + "," + (firstRangeNegative.y + y-0.5f)).GetComponent<GridCell>();
                if (cell.objectInThisGridSpace == null) 
                {
                    int tree = CalculateResource(x, y);
                    switch (tree)
                    {
                        case > 11 :
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
        for (int y = 0; y < rangeY; y++)
        {
            for (int x = 0; x < rangeX; x++)
            {

                cell = GameObject.Find((firstRangeNegative.x + x - 0.5f) + "," + (firstRangeNegative.y + y - 0.5f)).GetComponent<GridCell>();
                if (cell.objectInThisGridSpace == null)
                {
                    int tree = CalculateResource(x, y);
                   
                }
            }
        }
    }
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * _gridSpaceSize;
        float y = gridPos.y * _gridSpaceSize;

        return new Vector3(x,0, y);

    }
}
