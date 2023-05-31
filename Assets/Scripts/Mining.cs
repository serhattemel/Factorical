using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Mining : MonoBehaviour
{
    public readonly List<GameObject> MiningList = new List<GameObject>();
    public readonly List<GameObject> proccesedMiningList = new List<GameObject>();
    [SerializeField] public List<GameObject> oreList = new List<GameObject>();
    [SerializeField] public List<GameObject> proccesedOreList = new List<GameObject>();
    public GameObject parentOre;
    private static int oreID = 0;
    [SerializeField] private int proccesorType;

    public int ProccesorType { get => proccesorType; set => proccesorType = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InstantiateOre(int _oreId, Belt belt)
    {
        MiningList.Add(Instantiate(oreList[_oreId], new Vector3(belt.transform.position.x, belt.transform.position.y, -1f), Quaternion.identity) as GameObject);
        //MiningList[_oreID].name = $"Ore: {_oreID}";
        if (_oreId == 0)
            MiningList[oreID].name = "Tree";
        else if (_oreId == 1)
            MiningList[oreID].name = "Blue Ore";
        else if (_oreId == 2)
            MiningList[oreID].name = "Red Ore";

        MiningList[oreID].transform.parent = parentOre.transform;
        oreID++;
        return;
    }
    public void InstantiateProccesedOre(int _oreId, Belt belt)
    {
        MiningList.Add(Instantiate(proccesedOreList[_oreId], new Vector3(belt.transform.position.x, belt.transform.position.y, -1f), Quaternion.identity) as GameObject);
        //MiningList[_oreID].name = $"Ore: {_oreID}";
        if (_oreId == 2)
            MiningList[oreID].name = "Proccesed Tree";
        else if (_oreId == 0)
            MiningList[oreID].name = "Proccesed Blue Ore";
        else if (_oreId == 1)
            MiningList[oreID].name = "Proccesed Red Ore";

        MiningList[oreID].transform.parent = parentOre.transform;
        oreID++;
        return;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
