using UnityEngine;
using UnityEngine.UI;
using static Grid;
//love you into disease, but no medicine can.
//Created By xxx
public class BlankChess : MonoBehaviour
{
    // Use this for initialization
    private Button drowbut;
    void Start()
    {
        drowbut = transform.GetComponent<Button>();
        drowbut.onClick.AddListener(DrowChess);
    }
    private void DrowChess()
    {
        Gamemanager.Instance.selectStep = 0;
        Gamemanager.Instance.currentplayerType++;
        GameObject go = Gamemanager.Instance.DrawChess();
        go = Instantiate(go, transform.parent);
        go.transform.localPosition = transform.localPosition;
        Node node = Gamemanager.Instance.grid.GetItem(transform.localPosition);
        node.Chessobj = go;
        node.chessType = go.GetComponent<Chess>().chessType;
        node.playerType = go.GetComponent<Chess>().playerType;
        if (go.GetComponent<Chess>().playerType==Config.PlayerType.Blue)
        {
            
            Gamemanager.Instance.aIAlgorithm.AINodes.Add(node);
           
        }   
        Destroy(this.gameObject);
    }
}
