using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    Vector2Int Vector2Int;
    private Vector3 touchStart;
    public float groundZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector2Int = FindObjectOfType<GameGrid>().GetPosition();
        gameCamera.transform.position = new Vector3(Vector2Int.x / 2 - 0.5f, Vector2Int.y / 2 - 0.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            touchStart = GetWorldPosition(groundZ);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 direction = touchStart - GetWorldPosition(groundZ);
            Camera.main.transform.position += direction;
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
