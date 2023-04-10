using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;


    public GameObject[,] _gameGrid;
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

    public Vector2Int GetPosition()
    {
        return new Vector2Int(_width,_height);
    }

    void Update()
    {
    }

    void Awake()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        _gameGrid = new GameObject[_width, _height];
        map = new int[_width, _height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }
        DrawMap();
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Random.Range(0, 1000000).ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (x < (_width/10) || x > _width - (_width/10) || y < (_height / 10) || y > _height - (_height/10))
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }
    

    void SmoothMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }
    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < _width && neighbourY >= 0 && neighbourY < _height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
    void DrawMap()
    {
        if (map != null && terrainList[0] != null)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    float angle;
                    switch (Random.Range(0, 1))
                    {
                        case 1:
                            angle = 0f;
                            break;
                        case 2:
                            angle = 90f;
                            break;
                        case 3:
                            angle = 180f;
                            break;
                        case 4:
                            angle = 270f;
                            break;
                        default:
                            angle = 0f;
                            break;
                            

                    }
                    _gameGrid[x, y] = (map[x, y] == 1) ? Instantiate(terrainList[0], new Vector3(x * _gridSpaceSize, y * _gridSpaceSize), Quaternion.identity)
                                                        : _gameGrid[x, y] = Instantiate(terrainList[1], new Vector3(x * _gridSpaceSize, y * _gridSpaceSize), Quaternion.identity); ;
                    _gameGrid[x, y].transform.GetChild(0).Rotate(0, 0, angle);
                    _gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                    _gameGrid[x, y].transform.parent = transform;
                    _gameGrid[x, y].gameObject.name = x.ToString() + "," + y.ToString();
                    _gameGrid[x, y].gameObject.tag = "GridCell";
                    
                    
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
