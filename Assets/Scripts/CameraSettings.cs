using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    private Buildings buildings;
    [SerializeField] private Camera gameCamera;
    Vector2Int Vector2Int;
    private Vector3 touchStart;
    public float groundZ = 0;
    [SerializeField] private bool scrolling=false;
    public bool Scrolling
    {
        get { return scrolling; }
        set { scrolling = value; }
    }
    void Start()
    {
        scrolling = false;
        Vector2Int = FindObjectOfType<GameGrid>().GetPosition();
        gameCamera.transform.position = new Vector3(Vector2Int.x / 2 - 0.5f, Vector2Int.y / 2 - 0.5f, -10);
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = GetWorldPosition(groundZ);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - GetWorldPosition(groundZ);
                Camera.main.transform.position += direction;
            }
        }
    }

    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = gameCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distence;
        ground.Raycast(mousePos, out distence);
        return mousePos.GetPoint(distence);
    }
}
