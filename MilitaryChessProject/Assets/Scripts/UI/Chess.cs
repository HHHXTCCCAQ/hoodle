using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Config;
using static Grid;
//love you into disease, but no medicine can.
//Created By xxx
public class Chess : MonoBehaviour {
    public ChessType chessType;
    public PlayerType playerType;
    // Use this for initialization
    public Button but;
	void Start () {
        but = GetComponent<Button>();
        but.onClick.AddListener(OnButClick);
    }
	
	void OnButClick()
    {
        Node node = Gamemanager.Instance.GetNode(transform.localPosition);
        Debug.Log(node.CanArrive + "-" + node.Chessobj + node.chessType+node.pointType);
    }
}
