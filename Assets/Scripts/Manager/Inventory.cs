using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//添加JSON属性的时候 要在这个类中改四个地方（两个在初始化，两个在后续添加）
public class Inventory : MonoBehaviour
{
    //单例
    private static Inventory mInstance;
    public static Inventory GetInstance()
    {
        return mInstance;
    }

    //背包的容量
    private const int mCapacity = 24;
    public int GetCapacity() { return mCapacity; }

    //挂载的prefab
    public GameObject slot;
    public GameObject item;

    //统一拖拽的物体放入的UI层
    public GameObject DraggedItemPanel;

    //=================背包逻辑层，当前背包有的东西
    private List<MyItem> items = new List<MyItem>();
    private List<MyEnergy> energys = new List<MyEnergy>();

    //=================背包显示层
    public List<GameObject> slots = new List<GameObject>();
    public List<SlotItem> slotItems = new List<SlotItem>();

    //存放slot的panel
    public GameObject SlotPanel;

    // Use this for initialization
    void Start()
    {
        //单例
        mInstance = this;

        //初始化逻辑层存储信息（待完成）

        //初始化显示层存储信息
        InitialSlots();

        //Test
        AddInInventory(DataBase.STYLE_Energy, "200");
        AddInInventory(DataBase.STYLE_Energy, "201");
        AddInInventory(DataBase.STYLE_Energy, "202");
        AddInInventory(DataBase.STYLE_Item, "100");
        AddInInventory(DataBase.STYLE_Item, "101");
        AddInInventory(DataBase.STYLE_Item, "102");
        AddInInventory(DataBase.STYLE_Item, "103");
        Debug.Log(energys[0].Name);
    }

    //====后台数据对当前背包添加物品====
    public void AddInInventory(int _style, string _id)
    {
        switch (_style)
        {
            case DataBase.STYLE_Item: AddItem(_id); break;
            case DataBase.STYLE_Energy: AddEnerge(_id); break;
            default: Debug.Log("加入背包数据错误"); break;
        }
    }
    public void AddItem(string _id)
    {
        //加入逻辑层
        items.Add(GetComponent<DataBase>().FetchItemByID(_id));

        //=========同步修改显示层的List
        for (int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i].Style == -1)
            {
                slotItems[i].Style = DataBase.STYLE_Item;
                slotItems[i].ID = _id;
                GameObject itemObj = Instantiate(item);
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = itemObj.transform.parent.position;
                itemObj.name = GetComponent<DataBase>().FetchItemByID(_id).Name;
                itemObj.GetComponent<DragItemData>().slotItem = slotItems[i];
                itemObj.GetComponent<DragItemData>().slotIndex = i;
                itemObj.GetComponent<Image>().sprite = GetComponent<DataBase>().FetchItemByID(_id).sprite;
                break;
            }
        }
    }
    private void AddEnerge(string _id)
    {
        energys.Add(GetComponent<DataBase>().FetchEnergyByID(_id));

        //=========同步修改显示层的List
        for (int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i].Style == -1)
            {
                slotItems[i].Style = DataBase.STYLE_Energy;
                slotItems[i].ID = _id;
                GameObject itemObj = Instantiate(item);
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = itemObj.transform.parent.position;
                itemObj.name = GetComponent<DataBase>().FetchEnergyByID(_id).Name;
                itemObj.GetComponent<DragItemData>().slotItem = slotItems[i];
                itemObj.GetComponent<DragItemData>().slotIndex = i;
                itemObj.GetComponent<Image>().sprite = GetComponent<DataBase>().FetchEnergyByID(_id).sprite;
                break;
            }
        }
    }


    //===后台数据对当前背包删除物品====
    public void DeleteOutInventory(int _style, string _id)
    {
        switch (_style)
        {
            case DataBase.STYLE_Item: DeleteItem(_id); break;
            case DataBase.STYLE_Energy: DeleteEnergy(_id); break;
            default: Debug.Log("删除背包数据错误"); break;
        }
    }
    private void DeleteItem(string _id)
    {
        items.Remove(GetComponent<DataBase>().FetchItemByID(_id));
    }
    private void DeleteEnergy(string _id)
    {
        energys.Remove(GetComponent<DataBase>().FetchEnergyByID(_id));
    }


    //==前端显示后端List存储的背包物品==
    public void InitialSlots()//初始化显示层存储信息
    {
        for (int i = 0; i <= mCapacity; i++)
        {
            //用于暂存最后拉出去的东西
            if (i == mCapacity)
            {
                GameObject temp = Instantiate(slot);
                temp.SetActive(false);
                slots.Add(temp);
                slots[i].transform.SetParent(SlotPanel.transform);
                slots[i].GetComponent<DragSlotData>().slotID = i;
                slotItems.Add(new SlotItem());
            }
            else
            {
                slots.Add(Instantiate(slot));
                slots[i].transform.SetParent(SlotPanel.transform);
                slots[i].GetComponent<DragSlotData>().slotID = i;
                slotItems.Add(new SlotItem());
            }
        }

        /*暂时没有用  必须要改
        int index = 0;
        for (int i = 0; i < items.Count; i++)
        {
            for (; index < slotItems.Count; index++)
            {
                if (slotItems[index].Style == -1)
                {
                    slotItems[index].Style = DataBase.STYLE_Item;
                    slotItems[index].ID = items[i].ID;
                    GameObject itemObj = Instantiate(item);
                    itemObj.transform.SetParent(slots[index].transform);
                    itemObj.transform.position = Vector2.zero;
                    itemObj.name = items[i].Name;
                    itemObj.GetComponent<ControllItemPosition>().slotItem = slotItems[index];
                    itemObj.GetComponent<ControllItemPosition>().slotIndex = i;
                    break;
                }
            }
        }
        for (int i = 0; i < energys.Count; i++)
        {
            for (; index < slotItems.Count; index++)
            {
                if (slotItems[index].Style == -1)
                {
                    slotItems[index].Style = DataBase.STYLE_Energy;
                    slotItems[index].ID = items[i].ID;
                    GameObject itemObj = Instantiate(item);
                    itemObj.transform.SetParent(slots[index].transform);
                    itemObj.transform.position = Vector2.zero;
                    itemObj.name = items[i].Name;
                    itemObj.GetComponent<ControllItemPosition>().slotItem = slotItems[index];
                    itemObj.GetComponent<ControllItemPosition>().slotIndex = i;
                    break;
                }
            }
        }
        */

    }
    //==================================

   
}

public class SlotItem
{
    public int Style;
    public string ID;

    public SlotItem(int _style,string _id)
    {
        Style = _style;
        ID = _id;
    }
    public SlotItem()
    {
        Style = -1;
    }
}

