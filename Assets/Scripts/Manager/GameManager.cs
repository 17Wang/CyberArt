using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //单例模式
    private static GameManager mInstance;

    //玩家
    public GameObject GamePlayer;

    /// <summary>
    /// 单例模式 初始化GameManager
    /// </summary>
    public static GameManager GetInstance()
    {
        return mInstance;
    }

    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    private void InitialGamePlayer()
    {
        GamePlayer = Instantiate(GamePlayer);
        GamePlayer.transform.SetParent(GameObject.Find("PlayerLevel").transform);
        GamePlayer.transform.position = GamePlayer.transform.parent.position;
    }

    /// <summary>
    /// 获取玩家GameObject
    /// </summary>
    public GameObject GetGamePlayer()
    {
        if (GamePlayer != null)
            return GamePlayer;
        return null;
    }

    void Awake()
    {
        mInstance = this;
        InitialGamePlayer();//必须在Awake里实例化出来玩家 后面接收到的玩家才不会乱
    }
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
