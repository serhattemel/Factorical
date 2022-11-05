using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform objectToPlace;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private bool followPointer=false;
    private GameGrid gameGrid;
    [SerializeField] private LayerMask whatIsAGridLayer;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        if (followPointer == true && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            objectToPlace.position = hitInfo.point;
        }

        GridCell cellMouseIsOver = IsMouseOverAGridSpace();
        if(cellMouseIsOver!=null)
        {
            if(Input.GetMouseButton(0))
            {
                cellMouseIsOver.GetComponentInChildren<SpriteRenderer>().material.color = Color.red;
                Debug.Log("Cell Pos:"+ cellMouseIsOver.GetPosition());
                followPointer = false;
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
