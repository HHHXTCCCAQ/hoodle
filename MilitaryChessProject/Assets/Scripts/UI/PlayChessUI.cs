using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class PlayChessUI : MonoBehaviour
{

    private Button but_FightingP;
    private Button but_FightingC;
    private Button but_Back;
    public GameObject FightComputerUI;
    public GameObject FightPeopleUI;
    public GameObject StartUI;
    // Use this for initialization
    void Start()
    {
        but_FightingP = transform.Find("but_FightingP/").GetComponent<Button>();
        but_FightingC = transform.Find("but_FightingC/").GetComponent<Button>();
        but_Back = transform.Find("but_Back/").GetComponent<Button>();
        but_Back.onClick.AddListener(OnBackClick);
        but_FightingC.onClick.AddListener(OnFightingCClick);
        but_FightingP.onClick.AddListener(OnFightingPClick);

    }

    private void OnFightingPClick()
    {
        FightPeopleUI.SetActive(true);
        gameObject.SetActive(false);
        
    }
    private void OnFightingCClick()
    {
        FightComputerUI.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnBackClick()
    {
        StartUI.SetActive(true);
        gameObject.SetActive(false);
    }
}