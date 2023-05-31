<<<<<<< Updated upstream
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
=======
﻿using UnityEngine;
using UnityEngine.UI;
>>>>>>> Stashed changes

public class Factory_1 : MonoBehaviour
{
    private Buildings buildings;
<<<<<<< Updated upstream
    public int upgradeLevel;
    public int maxUpgradeLevel;
    // Start is called before the first frame update
=======
    private GameManager gameManager;
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
    private float factoryPrice = 10f;

    public string FactoryType { get => factoryType; set => factoryType = value; }
    public GridCell GridCell { get => gridCell; set => gridCell = value; }
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    public int Rotation_ { get => rotation_; set => rotation_ = value; }
    public float BlueOre { get => blueOre; set => blueOre = value; }
    public float RedOre { get => redOre; set => redOre = value; }
    public float FactoryPrice { get => factoryPrice; set => factoryPrice = value; }

>>>>>>> Stashed changes
    void Awake()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        gameManager = GameObject.Find("Building").GetComponent<GameManager>();
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        upgradeLevel = 0;
        maxUpgradeLevel = 3;
<<<<<<< Updated upstream

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
=======
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
        if (ore == "Blue Ore")
        {

            BlueOre++;


        }
        if (ore == "Red Ore")
        {
            RedOre++;
        }
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

>>>>>>> Stashed changes
    public void Upgrade()
    {
        switch (upgradeLevel)
        {
            case 0:
                GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                upgradeLevel++;
                break;
            case 1:
                GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
                upgradeLevel++;
                break;
            case 2:
                GetComponentInChildren<MeshRenderer>().material.color = Color.magenta;
                upgradeLevel++;
                break;
        }


    }
    public void RotateByDegrees()
    {
<<<<<<< Updated upstream
        Vector3 rotationToAdd = new Vector3(0, 0, 90);
=======
        Vector3 rotationToAdd = new Vector3(0, 0, -90f);
>>>>>>> Stashed changes
        transform.Rotate(rotationToAdd);

    }
    public void BluePrintOff()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
<<<<<<< Updated upstream
    }
    // Update is called once per frame
    void Update()
    {
=======
        if (buildEffect != null)
            buildEffect.Play();
>>>>>>> Stashed changes
    }

}
