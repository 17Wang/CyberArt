using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Life {

    //移动部分
    private GameObject mGameObject;
    public Vector3 mDestinationInGameWorld;//目的游戏世界坐标

    //动画部分
    private Animator mAnimator;

    /// <summary>
    /// 移动部分
    /// 控制物体不出地图?
    /// 控制物体不出摄像机位置?
    /// </summary>
    private void MoveUpdate()
    {
        mPositionInGameWorld = transform.position;       
        Direction = mDestinationInGameWorld - mPositionInGameWorld;

        //向量数值化
        float _sqrtDirection = Mathf.Sqrt(Direction.x * Direction.x + Direction.y * Direction.y);
        Direction.x = Direction.x / _sqrtDirection;
        Direction.y = Direction.y / _sqrtDirection;
        mMovement = new Vector2(Direction.x * Speed.x, Direction.y * Speed.y);

        if (_sqrtDirection <= 0.05f)
            mMovement = Vector2.zero;
    }

    ///<summary>
    ///动画转移
    /// </summary>
    private void AnimatorUpdate()
    {
        if (mAnimator == null)
            return;
        //AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);

        if (mMovement == Vector2.zero)
        {
            mAnimator.SetBool("Stand", true);
        }
        else
        {
            mAnimator.SetBool("Stand", false);
        }

        //向左走
        if (Direction.x <= 0 && mMovement != Vector2.zero)
        {
            mAnimator.SetBool("MoveLeft", true);
        }
        else
        {
            mAnimator.SetBool("MoveLeft", false);
        }

        //向右走
        if (Direction.x>= 0 && mMovement != Vector2.zero)
        {
            mAnimator.SetBool("MoveRight", true);
        }
        else
        {
            mAnimator.SetBool("MoveRight", false);
        }

        if (mCanMove == false)
        {
            mAnimator.SetBool("MoveRight", false);
            mAnimator.SetBool("MoveLeft", false);
            mAnimator.SetBool("Stand", true);
        }


    }

    /// <summary>
    /// 设置目的地
    /// </summary>
    public void SetDestinationInGameWorld(Vector3 _destination)
    {
        mDestinationInGameWorld = _destination;
    }

	// Use this for initialization
	void Start () {
        mCanMove = true;
        mPositionInGameWorld = transform.position;
        mDestinationInGameWorld = mPositionInGameWorld;

        //动画部分
        mAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
            MoveUpdate();
            AnimatorUpdate();
            
    }

    void FixedUpdate()
    {
        if (mCanMove == true)
            GetComponent<Rigidbody2D>().velocity = mMovement;
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
