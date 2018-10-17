using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour{
    private GameObject LerpAim;
    public float LerpSpeed;

	// Use this for initialization
	void Start () {
        LerpAim = GameManager.GetInstance().GetGamePlayer();
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,LerpAim.transform.position+new Vector3(0,0,-5),Time.deltaTime*LerpSpeed);
	}
}
