using System.Collections.Generic;
using UnityEngine;

public class YedekGameGridd : MonoBehaviour
{
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

    public GameObject[,] _gameGrid;
    private int x_offset, y_offset;
    public float magnification = 7f;
    public Vector2Int GetPosition()
    {
        return new Vector2Int(_width, _height);
    }

    [System.Obsolete]
    void Awake()
    {
        x_offset = Random.RandomRange(-_width, 0);
        y_offset = Random.RandomRange(-_height, 0);
        CreateGrid();
    }

    private void CreateGrid()
    {
        _gameGrid = new GameObject[_width, _height];
        if (terrainList[0] == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab not assigned");
        }

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                int terrainNumber = CalculateTerrain(x, y);
                _gameGrid[x, y] = Instantiate(terrainList[terrainNumber], new Vector3(x * _gridSpaceSize, y * _gridSpaceSize), Quaternion.identity);
                _gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                _gameGrid[x, y].transform.parent = transform;
                _gameGrid[x, y].gameObject.name = x.ToString() + "," + y.ToString();
                _gameGrid[x, y].gameObject.tag = "GridCell";
            }
        }
    }
    int CalculateTerrain(int x, int y)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x - x_offset) / magnification,
            (y - y_offset) / magnification
            );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        float scaled_perlin = clamp_perlin * (terrainList.Count);
        if (scaled_perlin == terrainList.Count)
        {
            scaled_perlin = terrainList.Count - 1;
        }


        return Mathf.FloorToInt(scaled_perlin);
    }
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * _gridSpaceSize;
        float y = gridPos.y * _gridSpaceSize;

        return new Vector3(x, 0, y);

    }
}
