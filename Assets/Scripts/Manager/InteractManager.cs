using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractManager : MonoBehaviour {
    //单例模式
    private static InteractManager sInstance;
    /// <summary>
    /// 单例模式
    /// </summary>
    public static InteractManager GetInstance()
    {
        return sInstance;
    }

    [SerializeField]
    private Vector3 mPositionInScreen;//目的屏幕位置坐标
    [SerializeField]
    private Vector3 mDestinationInGameWorld;//目的游戏世界坐标

    //从GameManager接玩家GameObject
    private GameObject mGamePlayer;

    //能量球_挂载
    public GameObject EnergyBall;

    /// <summary>
    /// 判断鼠标点击位置触发不同的事件的Update
    /// </summary>
    private void InteractUpdate()
    {
        //获取鼠标点击的屏幕坐标
        if (Input.GetMouseButton(0))
        {
            mPositionInScreen = Input.mousePosition;

            if (EventSystem.current.IsPointerOverGameObject())//先判断UI层
            {

            }
            else //点击地面
            {
                mDestinationInGameWorld = Camera.main.ScreenToWorldPoint(mPositionInScreen);//转为游戏世界坐标
                mDestinationInGameWorld.z = mGamePlayer.transform.parent.position.z;//z轴坐标设置为父组件的坐标，父组件坐标在初始化时设置（GameManager）
                mGamePlayer.GetComponent<Player>().SetDestinationInGameWorld(mDestinationInGameWorld);//传送游戏世界坐标
            }
        }
    }

    void Awake()
    {
        sInstance = this;
    }

    // Use this for initialization
    void Start () {
        mGamePlayer = GameManager.GetInstance().GetGamePlayer();
	}
	
	// Update is called once per frame
	void Update () {
        InteractUpdate();
	}
}
