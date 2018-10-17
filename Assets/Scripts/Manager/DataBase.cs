using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class DataBase : MonoBehaviour {
    //单例
    private static DataBase mInstance;
    public static DataBase GetInstance()
    {
        return mInstance;
    }

    //物品
    private JsonData ItemDataBase;
    private JsonData EnergyDataBase;
    
    private List<MyItem> ItemList = new List<MyItem>();
    private List<MyEnergy> EnergyList = new List<MyEnergy>();

    public const int STYLE_Item = 0;
    public const int STYLE_Energy = 1;

    //对话交互
    private JsonData DialogDataBase;
    private List<DialogSystem> DialogList = new List<DialogSystem>();

    //当前数据
    public Save SaveGame = new Save();

    /// <summary>
    /// 初始化数据库（Item和Energy）
    /// </summary>
    private void ConstructDataBase()
    {
        //加载物品
        for (int i=0;i<ItemDataBase.Count;i++)
        {
            ItemList.Add(new MyItem(ItemDataBase[i]["ID"].ToString(),
                ItemDataBase[i]["Name"].ToString(),
               (int)ItemDataBase[i]["Pro"],
                ItemDataBase[i]["Description"].ToString()
                ));
        }

        //加载能量
        for (int i = 0; i < EnergyDataBase.Count; i++)
        {
            EnergyList.Add(new MyEnergy(EnergyDataBase[i]["ID"].ToString(),
                EnergyDataBase[i]["Name"].ToString(),
                (int)EnergyDataBase[i]["Energy"],
                EnergyDataBase[i]["Description"].ToString()
                ));
        }

        //加载交互对话
        for(int i=0;i<DialogDataBase.Count;i++)
        {
            DialogList.Add(new DialogSystem(DialogDataBase[i]["Player"].ToString(),
                DialogDataBase[i]["Info"].ToString()
                ));
        }
    }

    //===================Item读取数据
    public MyItem FetchItemByID(string _id)
    {
        for(int i=0;i<ItemList.Count;i++)
        {
            if(_id==ItemList[i].ID)
            {
                return ItemList[i];
            }
        }

        return null;
    }
    public MyItem FetchItemByName(string _name)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (_name == ItemList[i].Name)
            {
                return ItemList[i];
            }
        }

        return null;
    }

    //===================Energy读取数据
    public MyEnergy FetchEnergyByID(string _id)
    {
        for (int i = 0; i < EnergyList.Count; i++)
        {
            if (_id == EnergyList[i].ID)
            {
                return EnergyList[i];
            }
        }

        return null;
    }
    public MyEnergy FetchEnergyByName(string _name)
    {
        for (int i = 0; i < EnergyList.Count; i++)
        {
            if (_name == EnergyList[i].Name)
            {
                return EnergyList[i];
            }
        }

        return null;
    }

    //===================Dialog读取数据
    public DialogSystem FetchDialogByIndex(int _index)
    {
        if (_index >= DialogList.Count)
            return new DialogSystem();
        return DialogList[_index]; 
    }
   
    void Awake()
    {
        mInstance = this;
        ItemDataBase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        EnergyDataBase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Energys.json"));
        DialogDataBase = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/SystemNotification.json"));
        ConstructDataBase();
    }
    // Use this for initialization
    void Start() {
        //单例
        
    }
	
	// UpData is called once per frame
	void UpData () {
		
	}
}

public class MyItem
{
    public string ID { get; set; }
    public string Name { get; set; }
    public float Probability { get; set; }
    public string Description { get; set; }

    public Sprite sprite { get; set; }

    public MyItem(string _id,string _name,float _probability,string _description)
    {
        ID = _id;
        Name = _name;
        Probability = _probability;
        Description = _description;
        sprite = Resources.Load<Sprite>("Items/" + Name);
    }

    public MyItem()
    {
        ID = "000";
    }
}

public class MyEnergy
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Energy { get; set; }
    public string Description { get; set; }
    public Sprite sprite { get; set; }

    public MyEnergy(string _id,string _name,int _energy, string _description)
    {
        ID = _id;
        Name = _name;
        Energy = _energy;
        Description = _description;
        sprite = Resources.Load<Sprite>("Energys/" + Name);
    }

    public MyEnergy()
    {
        ID = "000";
    }
}

public class DialogSystem
{
    public string Player;
    public string Info;

    public DialogSystem(string _player,string _info)
    {
        Player = _player;
        Info = _info;
    }

    public DialogSystem()
    {
        Player = "";
        Info = "";
    }
}

//存储当前信息类
public class Save
{
    //存储能量
    public int CurrentEnergy;
    public int MaxEnergy;

    //存储通信信息
    public int DialogIndex;

    //存储生命值
    public int CurrentPlayerHp;
    public int MaxPlayerHp;

    //存储天数...格式待定

    //
    public Save()
    {
        CurrentEnergy = 50;
        MaxEnergy = 100;

        DialogIndex = 0;

        CurrentPlayerHp = 100;
        MaxPlayerHp = 100;
    }
}
