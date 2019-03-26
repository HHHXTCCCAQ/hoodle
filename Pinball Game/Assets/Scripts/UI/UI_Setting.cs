using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class UI_Setting : MonoBehaviour
{

    private Button but_Back;
    [SerializeField]
    private GameObject UI_Menu;
    // Use this for initialization
    void Start()
    {
        but_Back = transform.Find("but_Back/").GetComponent<Button>();
        but_Back.onClick.AddListener(OnBackClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBackClick()
    {
        this.gameObject.SetActive(false);
        UI_Menu.SetActive(true);
    }
}