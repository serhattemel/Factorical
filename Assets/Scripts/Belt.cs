using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Belt : MonoBehaviour
{
    private Buildings buildings;
    private static int _beltID = 0;
    public Belt beltInSequence;
    public Factory_1 factoryInSequence;
    public bool isSpaceTaken=false;
    public float speed = 1f;
    [SerializeField]private Collider _Collider;
    private Collider tempCollider;

    public Collider Collider { get => _Collider; set => _Collider = value; }

    private void Start()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        beltInSequence = null;
        factoryInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"Belt: {_beltID++}";
        ShakeForBug();
        this.GetComponent<Factory_1>().FactoryPrice = 2f;
    }
    private void ShakeForBug()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, -183);
        this.transform.rotation = Quaternion.Euler(0, 0, 180f);
    }
    private void Update()
    {
        if (beltInSequence == null && buildings.buildingMode == false && factoryInSequence == null)
        {
            beltInSequence = FindNextBelt();
            if (beltInSequence == null)
            {
                factoryInSequence = FindFactory();
            }
        }


        if (FindItem())
        {
            StartCoroutine(StartBeltMove());
        }

    }


    //Factory_1 CheckForCollisions()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f); // Trigger alanındaki nesneleri bul
    //    foreach (Collider hitCollider in hitColliders)
    //    {
    //        if (hitCollider.gameObject.CompareTag("Factory"))
    //        {
    //            factoryInSequence = hitCollider.gameObject.GetComponent<Factory_1>();
    //            return factoryInSequence;
    //        }
            
    //    }
    //    return null;
    //}

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
        else if (_Collider != null && factoryInSequence != null && factoryInSequence.Rotation_ == this.GetComponent<Factory_1>().Rotation_)
        {
            Vector3 toPosition = factoryInSequence.transform.position + new Vector3(0, 0, -0.5f);

            var step = speed * Time.deltaTime;

            while (_Collider != null && _Collider.transform.position != toPosition)
            {
                _Collider.transform.position =
                    Vector3.MoveTowards(_Collider.transform.position, toPosition, step);
                yield return null;
            }
            
            if (_Collider != null)
            {
                
                if (_Collider != tempCollider)
                {
                    if (_Collider.name == "Tree")
                    {
                        factoryInSequence.Storing(_Collider.name);
                    }
                    if (_Collider.name == "Blue Ore")
                    {
                        factoryInSequence.Storing(_Collider.name);
                    }
                    if (_Collider.name == "Red Ore")
                    {
                        factoryInSequence.Storing(_Collider.name);
                    }
                    if (_Collider.name == "Proccesed Blue Ore")
                    {
                        factoryInSequence.Storing(_Collider.name);
                    }
                    if (_Collider.name == "Proccesed Red Ore")
                    {
                        factoryInSequence.Storing(_Collider.name);
                    }

                }
                tempCollider = _Collider;
                Destroy(_Collider.gameObject);
            }

            //_Collider = null;
        }
    }
    
    private Factory_1 FindFactory()
    {
        Ray ray;

        ray = new Ray(this.transform.position, transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.up, Color.blue);
            Factory_1 factory = hit.collider.GetComponent<Factory_1>();
            return factory;
        }
        return null;
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