using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    //UI界面
    public GameObject UIVisual;

    //物体和玩家之间的最大UI可见距离
    [SerializeField]
    private float mMaxDistanceForUI=1;

    /// <summary>
    /// 必须设置的函数
    /// </summary>
    public void SetMaxDistanceForUI(float _maxDistanceForUI)
    {
        mMaxDistanceForUI = _maxDistanceForUI;
    }

    //判断
    public void ShowUI_Update()
    {
        if (Vector2.Distance(transform.position, GameManager.GetInstance().GetGamePlayer().transform.position) <= mMaxDistanceForUI)
        {
            UIVisual.SetActive(true);
        }
        else
        {
            UIVisual.SetActive(false);
        }
        
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ShowUI_Update();
	}
}
