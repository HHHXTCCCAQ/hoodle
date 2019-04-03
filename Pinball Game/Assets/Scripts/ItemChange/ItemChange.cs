using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class ItemChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
    float angle = 0;
    // Update is called once per frame
    void Update () {
        if (!DelayStartGame._instance.startGame)
            return;
        angle -= Time.deltaTime * 20;
        transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
	}
}
