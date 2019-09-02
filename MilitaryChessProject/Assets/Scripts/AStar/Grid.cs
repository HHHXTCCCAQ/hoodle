using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;
//love you into disease, but no medicine can.
//Created By xxx
public class Grid : MonoBehaviour
{

    public class Node
    {
        public GameObject Chessobj;
        public PointType pointType;
        public ChessType chessType;
        public Vector2 pos;
        public int x, y;
        public bool isCanClick = true;
        public bool CanArrive = true;
        public int Tstart;
        public int Tend;
        public Node parentNote;
        public Node(Vector2 pos, int x, int y)
        {
            this.pos = pos;
            this.x = x;
            this.y = y;
        }
        
    }
    private int w = 5, h = 17;
    public Transform tr;
    public Node[,] notes;
    private void Awake()
    {
        InitNote();
        //getItem(new Vector3(-9.55f, -185.2569f, 0));
    }
    public Node GetItem(Vector2 position)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (notes[i, j] != null)
                {
                    if (Vector2.Distance(notes[i, j].pos, position) < 0.5f)
                    {
                        return notes[i, j];
                    }
                }

            }
        }
        return null;
    }
    private void InitNote()
    {
        notes = new Node[w, h];
        GameObject Tpr = Resources.Load<GameObject>("Chess/BlankChess");
        float broadWidth = transform.GetComponent<RectTransform>().rect.width;
        float broadHight = transform.GetComponent<RectTransform>().rect.height;
        Vector2 startPoint = new Vector2(transform.localPosition.x - broadWidth / 2,
        transform.localPosition.y - broadHight / 2);
        float unitXvalue = broadWidth / (w - 1);
        float unitYvalue = broadHight / (h - 1);
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                Vector2 pos = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                Node notePoint = new Node(pos, i, j);
                notePoint.chessType = ChessType.Null;
                notes[i, j] = notePoint;
                
                if (j == 6 || j == 7 || j == 8 || j == 9 || j == 10)
                {
                    notes[i, j].isCanClick = false;
                    if ((i == 1 && j == 8) || (i == 1 && j == 10)
                      || (i == 3 && j == 8) || (i == 3 && j == 10)
                      || (i == 1 && j == 7) || (i == 1 && j == 9)
                      || (i == 3 && j == 7) || (i == 3 && j == 9))
                    {
                        notes[i, j].CanArrive = false;
                        continue;
                    }
                }
                else if ((i == 1 && j == 2) || (i == 1 && j == 4)
                    || (i == 2 && j == 3) || (i == 3 && j == 2) || (i == 3 && j == 4)
                    || (i == 1 && j == 12) || (i == 1 && j == 14)
                    || (i == 2 && j == 13) || (i == 3 && j == 12) || (i == 3 && j == 14))
                {
                    notes[i, j].pointType = PointType.LineCamp;

                }
                else if ((i == 1 && j == 0) || (i == 3 && j == 0) || (i == 1 && j == 16) || (i == 3 && j == 16))
                {
                    notes[i, j].pointType = PointType.BaseCamp;
                    GameObject go = Instantiate(Tpr, tr);
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                }
                else if (j == 1 || j == 5 || j == 11 || j == 15 || (i == 0 && 0 < j && j < 6) || (i == 0 && 10 < j && j < 16)
                    || (i == 4 && 0 < j && j < 6) || (i == 4 && 10 < j && j < 16))
                {
                    notes[i, j].pointType = PointType.RailWay;
                    GameObject go = Instantiate(Tpr, tr);
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                    notes[i,j].Chessobj = go;
                }
                else
                {
                    notes[i, j].pointType = PointType.HighWay;
                    GameObject go = Instantiate(Tpr, tr);
                    notes[i, j].Chessobj = go;
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                }
                
                Debug.Log(notes[i, j].pointType);
            }
        }
    }
    public void ClearData()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (notes[i, j] != null)
                {
                    notes[i, j].parentNote = null;
                    notes[i, j].Tstart = 0;
                    notes[i, j].Tend = 0;
                }

            }
        }
    }
    public List<Node> ArroundNote(Node node)
    {
        List<Node> list = new List<Node>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // 如果是自己，则跳过
                if (i == 0 && j == 0)
                    continue;
                int x = node.x + i;
                int y = node.y + j;
                // 判断是否越界，如果没有，加到列表中
                if (x < w && x >= 0 && y < h && y >= 0)
                    list.Add(notes[x, y]);
            }
        }
        return list;
    }

}
