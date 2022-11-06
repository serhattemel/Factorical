using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Buildings buildings;
    private GridCell _gridCell;
    private bool _cellOccupied;
    [SerializeField] private LayerMask whatIsAGridLayer;

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
    }

    // Update is called once per frame
    void Update()
    {

        GridCell cellMouseIsOver = IsMouseOverAGridSpace();
        
        if (cellMouseIsOver!=null)
        {
            print("asdas");
            Vector2 pos = cellMouseIsOver.GetPosition();
            _gridCell = GameObject.Find(pos.x + "," + pos.y).GetComponent<GridCell>();
            _cellOccupied = _gridCell.GetOcc();
            if (Input.GetMouseButton(0)&& _cellOccupied == false)
            {
                //cellMouseIsOver.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                Debug.Log("Cell Pos:"+ cellMouseIsOver.GetPosition());
                buildings.SetFalse();
                _gridCell.SetOccTrue();
            }
            
        }
    }

    private GridCell IsMouseOverAGridSpace()
    {
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
