using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWorks : MonoBehaviour
{
    Factory_1 factory_1;
    public GameObject parentOre;
    string factoryType;
    private Buildings buildings;
    private Mining mining;
    [SerializeField] private GameObject ore;
    private GridCell gridCell;
    Belt belt;
    public readonly List<GameObject> oreList = new List<GameObject>();

    public bool IsAvailable = true;
    public bool IsExtractorAvailable = true;
    //[SerializeField] public float CooldownDuration = 10f;




    void Start()
    {
        //get instance from Factory_1 
        factory_1 = GetComponent<Factory_1>();
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        mining = GameObject.Find("ExtractedOres").GetComponent<Mining>();
        factoryType = factory_1.FactoryType;
        StartCoroutine(StartCooldown());
    }
    private Belt CheckBelt()
    {
        RaycastHit hit;
        Ray ray;

        ray = new Ray(this.transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.DrawRay(transform.position, -transform.up, Color.red);
            belt = hit.collider.GetComponent<Belt>();

            return belt;
        }
        return null;
    }

    void Update()
    {
        if (factoryType == "Extractor" && CheckBelt() && buildings.buildingMode == false && belt.isSpaceTaken == false)
        {
            if (IsAvailable == false)
            {
                return;
            }

            gridCell = factory_1.GridCell;
            if (gridCell.OreInThisGridSpace != null)
            {
                ore = gridCell.OreInThisGridSpace;
            }
            Mining();
        }
        
    }
    
    void Mining()
    {
        if (ore != null)
        {
            //print("çıkarıcı");
            if (ore.name == "tree")
            {
                mining.InstantiateOre(0,belt);
            }
            else if (ore.name == "blue ore")
            {
                mining.InstantiateOre(1, belt);
            }
            else if (ore.name == "red ore")
            {
                mining.InstantiateOre(2, belt);
            }
            
            StartCoroutine(StartCooldown());
        }

    }
    
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(factory_1.CooldownDuration);
        IsAvailable = true;
    }
}
