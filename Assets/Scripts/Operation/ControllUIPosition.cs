using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllUIPosition : MonoBehaviour ,IBeginDragHandler,IDragHandler,IEndDragHandler{

    private GameObject _this;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _this.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    // Use this for initialization
    void Start () {
        //Inventory的特殊处理，只有拖动Title的Panel才能移动
        if (this.transform.parent.name == "Inventory")
        {
            _this = this.transform.parent.gameObject;
        }
        else
        {
            _this = this.transform.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
