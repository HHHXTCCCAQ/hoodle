using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//love you into disease, but no medicine can.
//Created By xxx
public class UIT : MonoBehaviour
{
    // Use this for initialization
    private Button but;
    void Start()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(butClick);
    }
    public void butClick()
    {
        Gamemanager.Instance.GetNode(transform.localPosition);
    }
}
