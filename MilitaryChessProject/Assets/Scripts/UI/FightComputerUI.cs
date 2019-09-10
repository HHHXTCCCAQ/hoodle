using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class FightComputerUI : MonoBehaviour
{

    private Button but_RePlay;
    private Button but_Confirm;
    private Button but_Undo;
    private Button but_Back;
    public GameObject PlayChessUI;
    // Use this for initialization
    void Start()
    {
        but_RePlay = transform.Find("but_RePlay/").GetComponent<Button>();
        but_Confirm = transform.Find("but_Confirm/").GetComponent<Button>();
        but_Undo = transform.Find("but_Undo/").GetComponent<Button>();
        but_Back = transform.Find("but_Back/").GetComponent<Button>();
        but_Back.onClick.AddListener(OnBackClick);
    }
    private void OnBackClick()
    {
        PlayChessUI.SetActive(true);
        gameObject.SetActive(false);
    }

}