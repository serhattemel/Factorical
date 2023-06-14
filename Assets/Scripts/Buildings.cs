using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Buildings : MonoBehaviour
{
    private GridCell _gridCell;
    private GameGrid gameGrid;
    private GameManager gameManager;
    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;
    public Button rotationButton;
    [SerializeField] public GameObject buildingMenu;
    public GameObject Target { get; set; }
    Vector3 truePos;
    private float _gridSpaceSize = 1.0f;
    [SerializeField] GameObject object1, object2;
    public readonly List<GameObject> _buildingsList = new List<GameObject>();
    [SerializeField] List<GameObject> factoryList = new List<GameObject>();
    public Transform objectToPlace { get; set; }
    [SerializeField] private Camera gameCamera;
    [SerializeField] private bool followPointer = true;
    [SerializeField] public bool buildingMode = false;
    public int _buildingCount = 0;
    public int factortCount { get; set; }


    void Start()
    {

        gameGrid = GameObject.Find("GameGrid").GetComponent<GameGrid>();
        gameManager = this.GetComponent<GameManager>();
        //InstantiateObject(3);

    }
    IEnumerator WaitInstantiateObject(int factory)
    {
        yield return new WaitForSeconds(0.1f);
        rotationButton.gameObject.SetActive(true);
        firstButton.gameObject.SetActive(false);
        secondButton.gameObject.SetActive(false);
        thirdButton.gameObject.SetActive(false);
        buildingMode = true;
        _buildingsList.Add(Instantiate(factoryList[factory], new Vector3(gameGrid.Width / 2, gameGrid.Height / 2, -0.5f), Quaternion.identity));
        _buildingsList[_buildingCount].name = "factory " + _buildingCount;
        _buildingsList[_buildingCount].transform.SetParent(object1.transform);
        Target = _buildingsList[_buildingCount];
        objectToPlace = _buildingsList[_buildingCount].transform;
        SetFactoryType(factory);
        followPointer = true;
        buildingMenu.SetActive(false);
        gameManager.Gold -= _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice;

    }

    private void SetFactoryType(int factory)
    {
        switch (factory)
        {
            case 0:
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryType = "Ore Factory";
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice = 10f;
                break;
            case 1:
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryType = "Wood Factory";
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice = 10f;
                break;
            case 2:
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryType = "Extractor";
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice = 10f;
                break;
            case 3:
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryType = "Main";
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice = 1000f;
                break;
            case 4:
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryType = "Belt";
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FactoryPrice = 2f;
                break;

        }
    }
    public void InstantiateObject(int factory)
    {

        if (factory == 4)
        {
            if (gameManager.Gold < 2)
            {
                buildingMode = false;
                return;
            }
        }
        else if (factory == 3)
        {
            if (gameManager.Gold < 1000)
            {
                buildingMode = false;
                return;
            }
        }
        else
        {
            if (gameManager.Gold < 10)
            {
                buildingMode = false;
                return;
            }
        }
        StartCoroutine(WaitInstantiateObject(factory));
    }
    public bool fallowPointer
    {
        get { return followPointer; }
        set { followPointer = value; }
    }
    private void LateUpdate()
    {
        if (buildingMode == true)
        {
            truePos.x = Mathf.Floor(Target.transform.position.x / _gridSpaceSize) * _gridSpaceSize + 0.5f;
            truePos.y = Mathf.Floor(Target.transform.position.y / _gridSpaceSize) * _gridSpaceSize + 0.5f;
            truePos.z = Mathf.Floor(Target.transform.position.z / _gridSpaceSize) * _gridSpaceSize + 0.5f;

            Target.transform.position = truePos;
        }
    }

    void Update()
    {

        if (buildingMode == true)
        {

            if (followPointer == false)
            {
                _buildingsList[_buildingCount].GetComponent<Factory_1>().BluePrintOff();
                _buildingsList[_buildingCount].gameObject.transform.position = new Vector3(_buildingsList[_buildingCount].gameObject.transform.position.x, _buildingsList[_buildingCount].gameObject.transform.position.y, -0.5f);
                if (_gridCell.ObjectInThisGridSpace != null)
                {
                    _gridCell.OreInThisGridSpace = _gridCell.ObjectInThisGridSpace;
                }
                _gridCell.ObjectInThisGridSpace = _buildingsList[_buildingCount] as GameObject;
                _buildingsList[_buildingCount].GetComponent<Factory_1>().FindGridCell();
                _buildingCount++;
                buildingMode = false;
                rotationButton.gameObject.SetActive(false);
            }
            Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
            Vector2 pos;
            pos.x = Mathf.Floor(objectToPlace.position.x);
            pos.y = Mathf.Floor(objectToPlace.position.y);
            _gridCell = GameObject.Find(pos.x + "," + pos.y).GetComponent<GridCell>();
            if (followPointer == true && Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                objectToPlace.position = hitInfo.point;
                //if (_buildingCount == 0)
                //{
                //    objectToPlace.position = new Vector3(gameGrid.Width / 2, gameGrid.Height / 2, -0.5f);
                //    fallowPointer = false;
                //    firstButton.gameObject.SetActive(true);
                //}
            }

        }
    }
}
