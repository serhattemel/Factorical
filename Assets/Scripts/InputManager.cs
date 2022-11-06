using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Buildings buildings;
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
        bool a = cellMouseIsOver.GetComponent<GridCell>().GetOcc();

        if (cellMouseIsOver!=null&&a==false)
        {
            if(Input.GetMouseButton(0))
            {
                cellMouseIsOver.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                Debug.Log("Cell Pos:"+ cellMouseIsOver.GetPosition());
                buildings.SetTrue();
                cellMouseIsOver.GetComponent<GridCell>().SetOccTrue();
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
