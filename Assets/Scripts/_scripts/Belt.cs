using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private static int _beltID = 0;

    public Belt beltInSequence;
    public Belt_Item belt_Item;
    public bool isSpaceTaken;

    private Belt_Manager _beltManager;

    private void Start()
    {
        _beltManager = FindObjectOfType<Belt_Manager>();
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"Belt: {_beltID++}";
    }
    private void Update()
    {
        if (beltInSequence == null)
            beltInSequence = FindNextBelt();
        if (belt_Item != null && belt_Item.item != null)
            StartCoroutine(StartBeltMove());
    }

    public Vector3 GetItemPosition()
    {
        var padding = 0.3f;
        var position = transform.position;
        return new Vector3(position.x, position.y + padding, position.z);
    }

    private IEnumerator StartBeltMove()
    {
        isSpaceTaken = true;

        if(belt_Item.item!=null&& beltInSequence!= null && beltInSequence.isSpaceTaken == false)
        {
            Vector3 toPosition = beltInSequence.GetItemPosition();
            beltInSequence.isSpaceTaken = true;

            var step = _beltManager.speed * Time.deltaTime;

            while(belt_Item.item.transform.position != toPosition)
            {
                belt_Item.item.transform.position = 
                    Vector3.MoveTowards(belt_Item.transform.position,toPosition,step);
                yield return null;
            }
            isSpaceTaken = false;
            beltInSequence.belt_Item = belt_Item;
            belt_Item = null;
        }

    }
    private Belt FindNextBelt()
    {
        Transform currentBeltTransform = transform;
        RaycastHit hit;
        var forward = transform.right;

        Ray ray = new Ray(currentBeltTransform.position, forward);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Belt belt=hit.collider.GetComponent<Belt>();
            if (belt != null)
                return belt;
        }
        return null;
    }
}
