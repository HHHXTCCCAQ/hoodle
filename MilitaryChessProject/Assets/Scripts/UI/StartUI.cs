using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class StartUI : MonoBehaviour
{

    private Button but_Through;
    private Button but_Play;
    private Button but_Composition;
    private Button but_Teaching;
    public GameObject PlayChessUI;
    // Use this for initialization
    void Start()
    {
        but_Through = transform.Find("but_Through/").GetComponent<Button>();
        but_Play = transform.Find("but_Play/").GetComponent<Button>();
        but_Composition = transform.Find("but_Composition/").GetComponent<Button>();
        but_Teaching = transform.Find("but_Teaching/").GetComponent<Button>();
        but_Play.onClick.AddListener(OnPlayClick);
        but_Through.onClick.AddListener(OnThroughClick);
    }

    private void OnPlayClick()
    {
        PlayChessUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
    private void OnThroughClick()
    {

    }

    private void OnBackClick()
    {

    }
}