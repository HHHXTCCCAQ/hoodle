using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class UI_Interface : MonoBehaviour
{

    private Button but_Pause;
    private Button but_Continue;
    private Button but_BackToStartScene;
    private Button but_ReStart;
    private GameObject PauseInterFace;
    private Button but_BackToStartScene1;

    // Use this for initialization
    void Start()
    {
        but_Pause = transform.Find("but_Pause/").GetComponent<Button>();
        but_Continue = transform.Find("PauseInterface/but_Continue/").GetComponent<Button>();
        but_BackToStartScene = transform.Find("PauseInterface/but_BackToStartScene/").GetComponent<Button>();
        but_BackToStartScene1 = transform.Find("GameOver/but_BackToStartScene/").GetComponent<Button>();
        but_ReStart = transform.Find("GameOver/but_ReStart/").GetComponent<Button>();
        PauseInterFace = transform.Find("PauseInterface/").gameObject;
        but_Pause.onClick.AddListener(Pause);
        but_Continue.onClick.AddListener(Continue);
        but_ReStart.onClick.AddListener(ResetGame);
        but_BackToStartScene.onClick.AddListener(BackToStartScene);
        but_BackToStartScene1.onClick.AddListener(BackToStartScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Pause()
    {
        if (Config.IsGameOver)
            return;
        if (!PauseInterFace.activeSelf)
            PauseInterFace.SetActive(true);
        Time.timeScale = 0;
        Config.IsPause = true;
    }
    void Continue()
    {
        if (PauseInterFace.activeSelf)
            PauseInterFace.SetActive(false);
        Time.timeScale = 1;
        Config.IsPause = false;
    }
    private void ResetGame()
    {
        Config.IsGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void BackToStartScene()
    {
        SceneManager.LoadScene(1);
    }
}