using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private int _height = 10;
    private int _width = 10;
    private float _gridSpaceSize = 1.0f;

    public GameObject target;
    Vector3 truePos;


    [SerializeField] private GameObject _gridCellPrefab;
    private GameObject[,] _gameGrid;

    private void LateUpdate()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / _gridSpaceSize) * _gridSpaceSize + 0.5f;
        truePos.y = Mathf.Floor(target.transform.position.y / _gridSpaceSize) * _gridSpaceSize + 0.5f;
        truePos.z = Mathf.Floor(target.transform.position.z / _gridSpaceSize) * _gridSpaceSize + 0.5f;

        target.transform.position = truePos;
    }
    void Start()
    {

        CreateGrid();
        
    }

    private void CreateGrid()
    {
        _gameGrid = new GameObject[_height,_width];
        if(_gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab not assigned");
        }

        for(int y = 0; y < _height; y++)
        {
            for(int x=0; x < _width; x++)
            {
                _gameGrid[x, y] = Instantiate(_gridCellPrefab, new Vector3(x * _gridSpaceSize, y * _gridSpaceSize), Quaternion.identity);
                _gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                _gameGrid[x, y].transform.parent = transform;
                _gameGrid[x, y].gameObject.name = "Grid Space (X: " + x.ToString() + ", Y:" + y.ToString() + ")";
            }
        }
    }
    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.z / _gridSpaceSize);

        x = Mathf.Clamp(x, 0, _width);
        y = Mathf.Clamp(x,0,_height);

        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * _gridSpaceSize;
        float y = gridPos.y * _gridSpaceSize;

        return new Vector3(x,0, y);

    }
}
