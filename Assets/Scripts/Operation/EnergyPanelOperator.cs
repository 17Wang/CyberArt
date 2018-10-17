using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPanelOperator : MonoBehaviour {
    private static EnergyPanelOperator mInstance;
    public static EnergyPanelOperator GetInstance()
    {
        return mInstance;
    }

    public int MaxEnergy;
    public int CurrentEnergy;

    public GameObject EnergySlot; 

    void Awake()
    {
        mInstance = this;

        EnergySlot = Instantiate(EnergySlot);
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        CurrentEnergy = DataBase.GetInstance().SaveGame.CurrentEnergy;
        MaxEnergy = DataBase.GetInstance().SaveGame.MaxEnergy;

        transform.Find("Info").GetComponent<Text>().text = "能量值:" + CurrentEnergy + "/" + MaxEnergy;
        
    }

    
}
