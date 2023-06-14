using UnityEngine;
using UnityEngine.UI;

public class Factory_1 : MonoBehaviour
{
    private Buildings buildings;
    private GameManager gameManager;
    public ParticleSystem buildEffect;
    public int upgradeLevel;
    public int maxUpgradeLevel;
    [SerializeField] private int rotation_;
    public string levelText;
    [SerializeField] private string factoryType;
    private GridCell gridCell;
    [SerializeField] private float cooldownDuration = 10f;
    [SerializeField] private float blueOre = 0;
    [SerializeField] private float redOre = 0;
    [SerializeField] private float proccesedBlueOre = 0;
    [SerializeField] private float proccesedRedOre = 0;
    [SerializeField] private float tree = 0;
    private float factoryPrice = 10f;

    public string FactoryType { get => factoryType; set => factoryType = value; }
    public GridCell GridCell { get => gridCell; set => gridCell = value; }
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    public int Rotation_ { get => rotation_; set => rotation_ = value; }
    public float BlueOre { get => blueOre; set => blueOre = value; }
    public float RedOre { get => redOre; set => redOre = value; }
    public float FactoryPrice { get => factoryPrice; set => factoryPrice = value; }
    public float Tree { get => tree; set => tree = value; }
    public float ProccesedBlueOre { get => proccesedBlueOre; set => proccesedBlueOre = value; }
    public float ProccesedRedOre { get => proccesedRedOre; set => proccesedRedOre = value; }

    void Awake()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        gameManager = GameObject.Find("Building").GetComponent<GameManager>();
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
        if (factoryType == "Main")
        {
            switch (ore)
            {
                case "Blue Ore":
                    gameManager.Gold += 1;
                    break;
                case "Red Ore":
                    gameManager.Gold += 2;
                    break;
                case "Proccesed Blue Ore":
                    gameManager.Gold += 3;
                    break;
                case "Proccesed Red Ore":
                    gameManager.Gold += 6;
                    break;
                default:
                    gameManager.Gold += 1;
                    break;
            }
            return;
        }
        switch (ore)
        {
            case "Tree":
                Tree++;
                break;
            case "Blue Ore":
                BlueOre++;
                break;
            case "Red Ore":
                RedOre++;
                break;
            case "Proccesed Blue Ore":
                ProccesedBlueOre++;
                break;
            case "Proccesed Red Ore":
                ProccesedRedOre++;
                break;
        }
        //if (ore == "Tree")
        //{
        //    Tree++;
        //}
        //if (ore == "Blue Ore")
        //{
        //    BlueOre++;
        //}
        //if (ore == "Red Ore")
        //{
        //    RedOre++;
        //}
        //if (ore == "Proccesed Blue Ore")
        //{
        //    ProccesedBlueOre++;
        //}
        //if (ore == "Proccesed Red Ore")
        //{
        //    ProccesedRedOre++;
        //}
    }
    public void Destroy()
    {
        if (factoryType == "Belt" && this.GetComponent<Belt>().Collider != null)
        {
            Destroy(this.GetComponent<Belt>().Collider.gameObject);
        }

        gameManager.Gold += factoryPrice / 2;
        gridCell.ObjectInThisGridSpace = gridCell.OreInThisGridSpace;
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        if (gameManager.Gold < 10)
        {
            return;
        }
        switch (upgradeLevel)
        {
            case 0:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                gameManager.Gold -= 10;
                break;
            case 1:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                gameManager.Gold -= 10;
                break;
            case 2:
                CooldownDuration = CooldownDuration - 2;
                upgradeLevel++;
                gameManager.Gold -= 10;
                break;
        }


    }
    public void RotateByDegrees()
    {
        Vector3 rotationToAdd = new Vector3(0, 0, -90f);
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
        if (buildEffect != null)
            buildEffect.Play();
        if (gameObject.GetComponent<Animation>() != null)
            gameObject.GetComponent<Animation>().Play();
    }

}