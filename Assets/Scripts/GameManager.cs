using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text goldText;

    [SerializeField] private float gold = 100;

    public float Gold { get => gold; set => gold = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString();
    }
}
