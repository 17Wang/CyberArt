using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemData : MonoBehaviour ,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler{
    public SlotItem slotItem;
    public int slotIndex;

    //物体悬浮介绍面板
    public GameObject ItemIntroPanel;

    private Inventory mInventory;

    //记录在非背包栏里拖动前的父组件
    private Transform mParentSlot;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("触发");
        if (slotItem != null)
        {
            this.transform.SetParent(mInventory.DraggedItemPanel.transform);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slotItem != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.slotIndex != mInventory.GetCapacity())
        {
            this.transform.SetParent(mInventory.slots[slotIndex].transform);
            this.transform.position = mInventory.slots[slotIndex].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            if (mParentSlot != null)
            {
                this.transform.SetParent(mParentSlot.transform);
                this.transform.position = mParentSlot.transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
                Debug.Log("DragItemData父组件不存在");
        }
            //OnEndDragInEnergyPanel();
    }

    public void OnEndDragInEnergyPanel(GameObject _parent)
    {
        mParentSlot = _parent.transform;//很尴尬的权宜之计
        Debug.Log("触发2");
        this.transform.SetParent(_parent.transform);
        this.transform.position = _parent.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemIntroPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ItemIntroPanel.SetActive(false);
    }

    private void ShowIntroPanelUpdata()
    {
        if (ItemIntroPanel != null && ItemIntroPanel.activeSelf)
        {
            ItemIntroPanel.transform.position = Input.mousePosition;
            switch (slotItem.Style)
            {
                case DataBase.STYLE_Item:
                    {
                        ItemIntroPanel.transform.Find("Name").GetComponent<Text>().text = DataBase.GetInstance().FetchItemByID(slotItem.ID).Name;
                        ItemIntroPanel.transform.Find("Intro").GetComponent<Text>().text = DataBase.GetInstance().FetchItemByID(slotItem.ID).Description;
                    }
                    break;
                case DataBase.STYLE_Energy:
                    {
                        ItemIntroPanel.transform.Find("Name").GetComponent<Text>().text = DataBase.GetInstance().FetchEnergyByID(slotItem.ID).Name;
                        ItemIntroPanel.transform.Find("Intro").GetComponent<Text>().text = DataBase.GetInstance().FetchEnergyByID(slotItem.ID).Description;
                    }
                    break;
            }
        }
    }


    // Use this for initialization
    void Start () {
        mInventory = Inventory.GetInstance();
        ItemIntroPanel = Instantiate(ItemIntroPanel);
        ItemIntroPanel.transform.SetParent(mInventory.DraggedItemPanel.transform);
        ItemIntroPanel.transform.position = transform.position;
        ItemIntroPanel.name = transform.name+" Intro Panel";
        ItemIntroPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        ShowIntroPanelUpdata();
    }

    
}
