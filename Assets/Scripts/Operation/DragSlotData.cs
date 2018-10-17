using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragSlotData : MonoBehaviour,IDropHandler {

    public int slotID;
    private Inventory mInventory;

    public void OnDrop(PointerEventData eventData)
    {
        mInventory = Inventory.GetInstance();

        DragItemData droppedItem = eventData.pointerDrag.GetComponent<DragItemData>();

        if (mInventory.slotItems[slotID].Style == -1)
        {
            mInventory.slotItems[droppedItem.slotIndex] = new SlotItem();
            droppedItem.slotIndex = slotID;
            mInventory.slotItems[slotID] = droppedItem.slotItem;
        }
        else if (droppedItem.slotIndex != slotID)
        {
            Transform _item = this.transform.GetChild(0);
            _item.GetComponent<DragItemData>().slotIndex = droppedItem.slotIndex;
            _item.transform.SetParent(mInventory.slots[droppedItem.slotIndex].transform);
            _item.transform.position = _item.parent.transform.position;

            mInventory.slotItems[droppedItem.slotIndex] = _item.GetComponent<DragItemData>().slotItem;
            droppedItem.slotIndex = slotID;
            mInventory.slotItems[slotID] = droppedItem.slotItem;
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
