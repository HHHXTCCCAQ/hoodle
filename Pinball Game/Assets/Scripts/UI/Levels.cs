using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class Levels : MonoBehaviour
{

    private Button but_Level_1;
    private Button but_Level_2;
    private Button but_Level_3;
    private Button but_Level_4;
    private Button but_Level_5;
    private Button but_Level_6;
    private Button but_Level_7;
    private Button but_Level_8;
    private Button but_Level_9;
    private Button but_Level_10;
    private Button but_Back;
    // Use this for initialization
    void Start()
    {
        but_Level_1 = transform.Find("but_Level_1/").GetComponent<Button>();
        but_Level_2 = transform.Find("but_Level_2/").GetComponent<Button>();
        but_Level_3 = transform.Find("but_Level_3/").GetComponent<Button>();
        but_Level_4 = transform.Find("but_Level_4/").GetComponent<Button>();
        but_Level_5 = transform.Find("but_Level_5/").GetComponent<Button>();
        but_Level_6 = transform.Find("but_Level_6/").GetComponent<Button>();
        but_Level_7 = transform.Find("but_Level_7/").GetComponent<Button>();
        but_Level_8 = transform.Find("but_Level_8/").GetComponent<Button>();
        but_Level_9 = transform.Find("but_Level_9/").GetComponent<Button>();
        but_Level_10 = transform.Find("but_Level_10/").GetComponent<Button>();
        but_Back = transform.Find("but_Back/").GetComponent<Button>();

        but_Level_1.onClick.AddListener(delegate { butlevelClick(2); });
        but_Level_2.onClick.AddListener(delegate { butlevelClick(3); });
        but_Level_3.onClick.AddListener(delegate { butlevelClick(4); });
        but_Level_4.onClick.AddListener(delegate { butlevelClick(5); });
        but_Level_5.onClick.AddListener(delegate { butlevelClick(6); });
        but_Level_6.onClick.AddListener(delegate { butlevelClick(7); });
        but_Level_7.onClick.AddListener(delegate { butlevelClick(8); });
        but_Level_8.onClick.AddListener(delegate { butlevelClick(9); });
        but_Level_9.onClick.AddListener(delegate { butlevelClick(10); });
        but_Level_10.onClick.AddListener(delegate { butlevelClick(11); });
        but_Back.onClick.AddListener(Back);
    }


    void Back()
    {
        SceneManager.LoadScene(0);
    }
    private int maxlevel = 2;
    void butlevelClick(int index)
    {
        if (index - 1 > maxlevel)
            Debug.Log("当前关卡需要全部通过前面关卡");
        else
            SceneManager.LoadScene(index);
    }
}