using UnityEngine;
using UnityEngine.UI;
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
        GameObject go = Gamemanager.Instance.DrawChess();
        go = Instantiate(go,transform.parent);
        go.transform.localPosition = transform.localPosition;
        Destroy(this.gameObject);
    }
}
