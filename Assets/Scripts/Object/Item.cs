using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private GameObject mPlayer;

    private void DistanceUpdate()
    {
        float _x = transform.position.x;
        float _y = transform.position.y;
        //float _z = transform.position.z;
        if (transform.position.y <= mPlayer.transform.position.y)
        {
            transform.position = new Vector3(_x, _y, transform.parent.position.z-1);
        }
        else
        {
            transform.position = new Vector3(_x, _y, transform.parent.position.z+1);
        }

    }
    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () {
        mPlayer = GameManager.GetInstance().GetGamePlayer();
    }
	
	// Update is called once per frame
	void Update () {
        DistanceUpdate();
	}
}
