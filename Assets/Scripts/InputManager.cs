using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private Buildings buildings;
    private CameraSettings moving;
    public Button secondButton;
    public Button thirdButton;
    public Button rotationButton;
    private Factory_1 _factory;
    private Belt _belt;
    private GridCell _gridCell, cellMouseIsOver;
    public Text factoryName,factoryLevel;
    private float startTime, endTime,totalTime;
    [SerializeField] private LayerMask whatIsAGridLayer;
    List<RaycastResult> results;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        endTime = 0f;
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        moving = GameObject.Find("Main Camera").GetComponent<CameraSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            totalTime = endTime - startTime;
            cellMouseIsOver = IsMouseOverAGridSpace();
            switch (totalTime)
            {
                case < 0.2f:
                    moving.Scrolling = false;
                    if (cellMouseIsOver != null)
                    {
                        ClickOnGrid();
                    }
                    return;
                default:
                    return;
            }
        }
        
        
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        buildings.fallowPointer = false;
        buildings.firstButton.gameObject.SetActive(true);
        rotationButton.gameObject.SetActive(false);
    }
    private void ClickOnGrid()
    {
        if (buildings.buildingMode == true&& results.Count==0)
        {
            Vector2 pos = cellMouseIsOver.GetPosition();
            _gridCell = GameObject.Find(pos.x + "," + pos.y).GetComponent<GridCell>();
            string terrainName = _gridCell.transform.GetChild(0).name;
            if (/*_gridCell.objectInThisGridSpace == null && */moving.Scrolling==false&&terrainName!="ice")
            {
                Debug.Log("Cell Pos:" + cellMouseIsOver.GetPosition());
                StartCoroutine(ExampleCoroutine());

            }
        }
        else if(buildings.buildingMode == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitinfo, 100f))
            {
                if (hitinfo.collider.tag == "Factory" || hitinfo.collider.tag == "Belt")
                {
                    secondButton.gameObject.SetActive(true);
                    thirdButton.gameObject.SetActive(true);
                    _factory = hitinfo.transform.GetComponent<Factory_1>();
                    factoryName.text = _factory.name;
                    factoryLevel.text = _factory.upgradeLevel.ToString();

                }
                else
                {
                    secondButton.gameObject.SetActive(false);
                    thirdButton.gameObject.SetActive(false);
                }
            }
        }

    }
    public void DestroySelectedBuilding()
    {
        if (_factory)
        {
            if (buildings.fallowPointer == true)
            {
                _factory.Destroy();
                secondButton.gameObject.SetActive(false);
            }
            else
            {
                _factory.Destroy();
                secondButton.gameObject.SetActive(false);
            }
        }   
        else
            Debug.Log("Fabrika seçin");
    }
    public void UpgradeSelectedBuilding()
    {
        if (_factory)
        {
            _factory.Upgrade();
            factoryName.text = _factory.name;
            factoryLevel.text = _factory.upgradeLevel.ToString();
            //if (_factory.upgradeLevel == 3)
            //    thirdButton.gameObject.SetActive(false);
        }
        else
            Debug.Log("Fabrika seçin");
    }
    public void RotateSelectedBuilding()
    {
        string factoryname = "factory " + buildings._buildingCount;
        _factory = GameObject.Find(factoryname).GetComponent<Factory_1>();
        if (_factory)
        {
            _factory.RotateByDegrees();
        }
        else
            Debug.Log("Fabrika seçin");
    }
    private GridCell IsMouseOverAGridSpace()
    {
        results = null;
        PointerEventData eventDataCurrentPos = new PointerEventData(EventSystem.current);
        eventDataCurrentPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPos, results);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo, 100f, whatIsAGridLayer))
        {
            return hitInfo.transform.GetComponent<GridCell>();
        }
        else
        {
            return null;
        }
    }

}
