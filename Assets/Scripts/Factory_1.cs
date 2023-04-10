using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.VFX;

public class Factory_1 : MonoBehaviour
{
    private Buildings buildings;
    public ParticleSystem buildEffect;
    public int upgradeLevel;
    public int maxUpgradeLevel;
    // Start is called before the first frame update
    void Awake()
    {
        buildings = GameObject.Find("Building").GetComponent<Buildings>();
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        upgradeLevel = 0;
        maxUpgradeLevel = 3;

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
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
        Vector3 rotationToAdd = new Vector3(0,0, 90f);
        transform.Rotate(rotationToAdd);

    }
    public void BluePrintOff()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        buildEffect.Play();
    }
    // Update is called once per frame
    void Update()
    {
    }

}
