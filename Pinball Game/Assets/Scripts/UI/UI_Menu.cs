using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class UI_Menu : MonoBehaviour
{

    private Button but_StartGame;
    private Button but_Quit;
    [SerializeField]
    private GameObject UI_Setting;
    // Use this for initialization
    void Start()
    {
        but_StartGame = transform.Find("but_StartGame/").GetComponent<Button>();
        but_Quit = transform.Find("but_Quit/").GetComponent<Button>();

        but_StartGame.onClick.AddListener(StartGame);
        but_Quit.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}