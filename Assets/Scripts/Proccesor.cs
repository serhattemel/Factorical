using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proccesor : MonoBehaviour
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
        if (IsAvailable == true && buildings.buildingMode == false && CheckBelt() && belt.isSpaceTaken == false)
        {
            Procces();
        }
    }
    void Procces()
    {
        if (mining.ProccesorType == 0 && factory_1.Tree > 5)
        {
            mining.InstantiateProccesedOre(mining.ProccesorType, belt);
            factory_1.Tree -= 5;
        }
        else if (mining.ProccesorType == 1 && factory_1.BlueOre > 5)
        {
            mining.InstantiateProccesedOre(mining.ProccesorType, belt);
            factory_1.BlueOre -= 5;
        }
        if (mining.ProccesorType == 2 && factory_1.RedOre > 5)
        {
            mining.InstantiateProccesedOre(mining.ProccesorType, belt);
            factory_1.RedOre -= 5;
        }
        StartCoroutine(StartCooldown());
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(factory_1.CooldownDuration);
        IsAvailable = true;
    }
}
