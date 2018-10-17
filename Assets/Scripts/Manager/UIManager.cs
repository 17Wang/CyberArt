using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
    private static UIManager sInstance;
    public static UIManager getInstance()
    {
        return sInstance;
    }

    /// <summary>
    /// 打开操作台后的操作
    /// </summary>
    public void OperatorFunction(GameObject _aUI)
    {
        
        string _text = _aUI.GetComponent<Dropdown>().GetComponentInChildren<Text>().text;
        if (_text == "Overview")
            _aUI.transform.parent.Find("OverviewPanel").gameObject.SetActive(true);//true
        else
            _aUI.transform.parent.Find("OverviewPanel").gameObject.SetActive(false);


        if(_text == "Log")
            _aUI.transform.parent.Find("LogPanel").gameObject.SetActive(true);//true
        else
            _aUI.transform.parent.Find("LogPanel").gameObject.SetActive(false);

        if (_text == "Community")
            _aUI.transform.parent.Find("CommunityPanel").gameObject.SetActive(true);//true
        else
            _aUI.transform.parent.Find("CommunityPanel").gameObject.SetActive(false);

        if (_text == "Composite")
            _aUI.transform.parent.Find("CompositePanel").gameObject.SetActive(true);//true
        else
            _aUI.transform.parent.Find("CompositePanel").gameObject.SetActive(false);
    }


    /// <summary>
    /// 专门控制玩家移动
    /// </summary>
    public void SetPlayerMove(bool _canMove)
    {
        GameManager.GetInstance().GetGamePlayer().GetComponent<Player>().SetCanMove(_canMove);
    }

    void Awake()
    {
        sInstance = this;
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
