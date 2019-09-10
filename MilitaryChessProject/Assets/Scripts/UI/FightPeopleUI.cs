using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class FightPeopleUI : MonoBehaviour
{

    private Button but_RePlay_R;
    private Button but_GiveUp_R;
    private Button but_Undo_R;
    private Button but_Confirm_R;
    private Button but_RePlay_B;
    private Button but_GiveUp_B;
    private Button but_Undo_B;
    private Button but_Confirm_B;
    private Button but_Back;
    public GameObject PlayChessUI;
    // Use this for initialization
    void Start()
    {
        but_RePlay_R = transform.Find("but_RePlay_R/").GetComponent<Button>();
        but_GiveUp_R = transform.Find("but_GiveUp_R/").GetComponent<Button>();
        but_Undo_R = transform.Find("but_Undo_R/").GetComponent<Button>();
        but_Confirm_R = transform.Find("but_Confirm_R/").GetComponent<Button>();
        but_RePlay_B = transform.Find("but_RePlay_B/").GetComponent<Button>();
        but_GiveUp_B = transform.Find("but_GiveUp_B/").GetComponent<Button>();
        but_Undo_B = transform.Find("but_Undo_B/").GetComponent<Button>();
        but_Confirm_B = transform.Find("but_Confirm_B/").GetComponent<Button>();
        but_Back = transform.Find("but_Back/").GetComponent<Button>();
        but_Back.onClick.AddListener(OnBackClick);
    }
    private void OnBackClick()
    {
        PlayChessUI.SetActive(true);
        gameObject.SetActive(false);
    }
}