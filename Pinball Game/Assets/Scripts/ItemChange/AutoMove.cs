using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class AutoMove : MonoBehaviour {

    // Use this for initialization
    public  Vector2[] postions;
    public float speed;
    private Transform selftransform;
    private bool isArrive = false;
	void Start () {
        selftransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(selftransform.localPosition, postions[0]) <= 5)
            isArrive = true;
        if (Vector2.Distance(selftransform.localPosition, postions[1]) <= 5)
            isArrive = false;
        if (isArrive)
        {
            selftransform.localPosition = Vector2.Lerp(this.transform.localPosition, postions[1], Time.deltaTime * speed);
        }
        else
        {
            selftransform.localPosition = Vector2.Lerp(this.transform.localPosition, postions[0], Time.deltaTime * speed);
        }
        
	}
}
