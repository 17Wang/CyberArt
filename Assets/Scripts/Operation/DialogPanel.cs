using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogPanel : MonoBehaviour {

    //对话
    private Transform TextInTarget;//对话框
    private int mDialogIndex;//计数
    private bool mPlayerTurn;//回合

    private Transform mPlayerTexture;
    private Transform mOthersTexture;

    private void FlashUpdate()
    {
        if (DataBase.GetInstance().FetchDialogByIndex(mDialogIndex).Player == "")
            mPlayerTurn = false;

        if (mPlayerTurn)
        {
            TextInTarget.GetComponent<Text>().text = DataBase.GetInstance().FetchDialogByIndex(mDialogIndex).Player;
            mPlayerTexture.gameObject.SetActive(true);
            mOthersTexture.gameObject.SetActive(false);
        }
        else 
        {
            TextInTarget.GetComponent<Text>().text = DataBase.GetInstance().FetchDialogByIndex(mDialogIndex).Info;
            mPlayerTexture.gameObject.SetActive(false);
            mOthersTexture.gameObject.SetActive(true);
        }

        Debug.Log(mPlayerTurn);
        Debug.Log(mDialogIndex+" "+TextInTarget.GetComponent<Text>().text);
    }

    // Use this for initialization
    void Start () {
        //输入框
        TextInTarget = transform.GetComponentInChildren<Text>().transform;
        //计数器
        mDialogIndex = DataBase.GetInstance().SaveGame.DialogIndex;//应该是等于0
        Debug.Log("当前对话框" + mDialogIndex);//检查计数器

        //初始次序
        if (DataBase.GetInstance().FetchDialogByIndex(mDialogIndex).Player != "")
            mPlayerTurn = true;
        else
            mPlayerTurn = false;

        //设置次序显示的Texture
        mPlayerTexture = transform.Find("Player");
        mOthersTexture = transform.Find("Others");
    }
	
	// Update is called once per frame
	void Update () {
        FlashUpdate();
       
        if (Input.GetMouseButtonDown(0))
        {
            if (!mPlayerTurn)
                mDialogIndex++;
            else if (DataBase.GetInstance().FetchDialogByIndex(mDialogIndex).Info == "")
            {
                mDialogIndex++;
                mPlayerTurn = false;
            }
            mPlayerTurn = (mPlayerTurn == true) ? false : true;
        }
    }
}
