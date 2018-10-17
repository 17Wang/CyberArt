using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

    //生命值变量
    protected float mMaxHp;//最大生命值
    protected float mCurrentHp;//当前生命值
    protected bool mIsDead = false;//是否死亡

    //移动部分
    [SerializeField]
    protected bool mCanMove;//是否可以移动
    public Vector2 Speed=new Vector2(2,2);//速度——数值面板
    public Vector2 Direction=new Vector2(1,1);//方向——数值面板
    protected Vector2 mMovement;//移动向量

    [SerializeField]
    protected Vector3 mPositionInGameWorld;//玩家游戏世界坐标

    public void SetCanMove(bool canMove){
        mCanMove = canMove;
    }
    
    public Vector3 GetPositionInGameWorld()
    {
        return mPositionInGameWorld;
    }

    public void RecoverHp(float recoverHp){
        mCurrentHp += recoverHp;
        if (mCurrentHp >= mMaxHp)
            mCurrentHp = mMaxHp;

    }
    public void DropHp(float damage){
        mCurrentHp -= damage;
        if (mCurrentHp <= 0)
            mIsDead = true;
    }

    protected void DeathUpdate(){

    }
   
}
