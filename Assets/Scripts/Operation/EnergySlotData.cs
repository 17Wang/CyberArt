using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnergySlotData : MonoBehaviour,IDropHandler {
    private Inventory mInventory;

    private DragItemData droppedItem;

    public void OnDrop(PointerEventData eventData)
    {
        mInventory = Inventory.GetInstance();

        droppedItem = eventData.pointerDrag.GetComponent<DragItemData>();
        //SlotItem droppedSlotItem = droppedItem.slotItem;

        if (true)//能量槽为空
        {
            droppedItem.GetComponent<DragItemData>().OnEndDragInEnergyPanel(this.gameObject);
            mInventory.slotItems[droppedItem.slotIndex] = new SlotItem();

            droppedItem.slotIndex = mInventory.GetCapacity();
            mInventory.slotItems[droppedItem.slotIndex] = droppedItem.slotItem;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressEnergyButton()
    {
        if (droppedItem == null)
        {
            Debug.Log("当前无物体");
        }
        else
        {
            if (droppedItem.GetComponent<DragItemData>().slotItem.Style == DataBase.STYLE_Item)
                Debug.Log("该物体不能用于能量解析");
            if (droppedItem.GetComponent<DragItemData>().slotItem.Style == DataBase.STYLE_Energy)
            {
                MyEnergy _energy = DataBase.GetInstance().FetchEnergyByID(droppedItem.GetComponent<DragItemData>().slotItem.ID);
                DataBase.GetInstance().SaveGame.CurrentEnergy += _energy.Energy;
                Destroy(droppedItem.gameObject);
            }
        }   
        
    }
}
