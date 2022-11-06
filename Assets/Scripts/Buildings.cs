using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Buildings : MonoBehaviour
{
    public GameObject target;
    Vector3 truePos;
    private float _gridSpaceSize = 1.0f;
    [SerializeField] GameObject object1, object2_prefab;
    private GameObject object2;
    [SerializeField] private Transform objectToPlace;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private bool followPointer=true;
    void Start()
    {
    }
    public void InstantiateObject()
    {

        object2 = Instantiate(object2_prefab);
        object2.transform.SetParent(object1.transform);
        target = object2;
        objectToPlace = object2.transform;
        followPointer=true;
        object2.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
    }
    public void SetFalse()
    {
        followPointer = false;

    }
    private void LateUpdate()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / _gridSpaceSize) * _gridSpaceSize + 0.5f;
        truePos.y = Mathf.Floor(target.transform.position.y / _gridSpaceSize) * _gridSpaceSize + 0.5f;
        truePos.z = Mathf.Floor(target.transform.position.z / _gridSpaceSize) * _gridSpaceSize + 0.5f;

        target.transform.position = truePos;
    }

    void Update()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        if (followPointer == true && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            objectToPlace.position = hitInfo.point;
        }
        if(object2 != null && followPointer == false)
        {
            object2.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
    }
}
