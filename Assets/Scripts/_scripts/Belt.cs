using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private Buildings buildings;
    //private static int _beltID = 0;
    public Belt beltInSequence;
    public bool isSpaceTaken=false;
    public float speed = 1f;
    public Collider _Collider;
    private void Start()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        //gameObject.name = $"Belt: {_beltID++}";
        ShakeForBug();
    }
    private void ShakeForBug()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, -3f);
        this.transform.rotation = Quaternion.Euler(0, 0, 0.1f);
    }
    private void Update()
    {
        if (beltInSequence == null && buildings.buildingMode == false)
            beltInSequence = FindNextBelt();
        
        
        if (FindItem())
        {
            StartCoroutine(StartBeltMove());
        }

    }
    

    bool CheckForCollisions()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f); // Trigger alanındaki nesneleri bul
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Ore"))
            {
                _Collider = hitCollider;
                return true;
            }
            
        }
        return false;
    }

    private IEnumerator StartBeltMove()
    {
        if (_Collider != null && beltInSequence != null && beltInSequence.isSpaceTaken == false)
        {
            Vector3 toPosition = beltInSequence.transform.position + new Vector3(0, 0, -0.5f);

            var step = speed * Time.deltaTime;

            while (_Collider != null && _Collider.transform.position != toPosition)
            {
                _Collider.transform.position =
                    Vector3.MoveTowards(_Collider.transform.position, toPosition, step);
                yield return null;
            }
            beltInSequence._Collider = _Collider;
            _Collider = null;
        }

    }
    private Belt FindNextBelt()
    {
        RaycastHit hit;
        Ray ray;
        
        ray = new Ray(this.transform.position, transform.up);
        
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.DrawRay(transform.position, transform.up, Color.red);
            Belt belt = hit.collider.GetComponent<Belt>();
                
            return belt;
        }
        return null;
    }
    
    public bool FindItem()
    {
        RaycastHit hit;
        var up = transform.forward;

        Ray ray = new Ray(transform.position, -up);
        
        if (Physics.Raycast(ray, out hit,2f))
        {
            Debug.DrawRay(transform.position, -transform.forward, Color.red,2f);
            _Collider = hit.collider;
            
            if (_Collider != null && _Collider.gameObject.CompareTag("Ore"))
            {
                isSpaceTaken = true;
                return true;
            }
        }
        isSpaceTaken = false;
        return false;
    }
}