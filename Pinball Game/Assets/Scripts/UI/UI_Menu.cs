using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class UI_Menu : MonoBehaviour
{

    private Button but_StartGame;
    private Button but_Setting;
    private Button but_Quit;
    [SerializeField]
    private GameObject UI_Setting;
    // Use this for initialization
    void Start()
    {
        but_StartGame = transform.Find("but_StartGame/").GetComponent<Button>();
        but_Setting = transform.Find("but_Setting/").GetComponent<Button>();
        but_Quit = transform.Find("but_Quit/").GetComponent<Button>();

        but_StartGame.onClick.AddListener(StartGame);
        but_Quit.onClick.AddListener(QuitGame);
        but_Setting.onClick.AddListener(Setting);
    }

    void StartGame()
    {

    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    void Setting()
    {
        UI_Setting.SetActive(true);
        this.gameObject.SetActive(false);
    }
}