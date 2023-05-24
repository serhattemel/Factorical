using UnityEngine;

public class Factory_1 : MonoBehaviour
{
    private Buildings buildings;
    public ParticleSystem buildEffect;
    public int upgradeLevel;
    public int maxUpgradeLevel;
    [SerializeField] private int rotation_;
    public string levelText;
    [SerializeField] private string factoryType;
    [SerializeField] private string proccesorType;
    private GridCell gridCell;
    [SerializeField] private float cooldownDuration = 10f;

    [SerializeField] private float blueOre = 0;
    [SerializeField] private float redOre = 0;

    public string FactoryType { get => factoryType; set => factoryType = value; }
    public GridCell GridCell { get => gridCell; set => gridCell = value; }
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    public int Rotation_ { get => rotation_; set => rotation_ = value; }
    public float BlueOre { get => blueOre; set => blueOre = value; }
    public float RedOre { get => redOre; set => redOre = value; }


    void Awake()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        upgradeLevel = 0;
        maxUpgradeLevel = 3;
        Rotation_ = 0;
        

    }
    private void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.5f);
    }
    public void FindGridCell()
    {
        Vector2 pos;
        pos.x = Mathf.Floor(this.transform.position.x);
        pos.y = Mathf.Floor(this.transform.position.y);
        gridCell = GameObject.Find(pos.x + "," + pos.y).GetComponent<GridCell>();
    }

    public void Storing(string ore)
    {
        if (ore == "Blue Ore")
            BlueOre++;
        if (ore == "Red Ore")
            RedOre++;
    }
    public void Destroy()
    {
        if (factoryType == "Belt"&& this.GetComponent<Belt>().Collider != null)
        {
           Destroy(this.GetComponent<Belt>().Collider.gameObject);
        }
        gridCell.ObjectInThisGridSpace = gridCell.OreInThisGridSpace;
        Destroy(gameObject);
    }
 
    public void Upgrade()
    {
        switch (upgradeLevel)
        {
            case 0:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                break;
            case 1:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                break;
            case 2:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                break;
        }
        
        
    }
    public void RotateByDegrees()
    {
        Vector3 rotationToAdd = new Vector3(0,0, -90f);
        transform.Rotate(rotationToAdd);
        Rotation_ += 1;
        if (Rotation_ == 4)
        {
            Rotation_ = 0;
        }

    }
    public void BluePrintOff()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        if(buildEffect!=null)
        buildEffect.Play();
    }

}
