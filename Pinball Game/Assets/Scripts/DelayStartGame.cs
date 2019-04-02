using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class DelayStartGame : MonoBehaviour {

    public static DelayStartGame _instance;
    [HideInInspector]
    public bool startGame = false;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] images;
    private Animation TimeUIanimation;
    private float time;
	// Use this for initialization
	void Start () {
        _instance = this;
        TimeUIanimation = image.GetComponent<Animation>();
        TimeUIanimation.Play();
    }
    private int index=0;
	// Update is called once per frame
	void Update () {
        
        if (!startGame)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                time = 0;
                if (index >= images.Length - 1)
                {
                    startGame = true;
                    return;
                }                 
                index += 1;
                TimeUIanimation.Play();
                image.sprite = images[index];

            }
        }      
	}

}
