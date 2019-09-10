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
        public PlayerType playerType;
        public Vector2 pos;
        public int x, y;
        public bool isCanClick = true;
        public bool CanArrive = true;//是否为障碍
        public int Tstart;//到起始点的消耗
        public int Tend;
        public Node parentNote;
        public Node(Vector2 pos, int x, int y)
        {
            this.pos = pos;
            this.x = x;
            this.y = y;
        }
        
    }
    private int w = 5, h = 14;
    public Transform tr;
    public Node[,] nodes;
    private void Awake()
    {
        InitNote();
        //getItem(new Vector3(-9.55f, -185.2569f, 0));
    }
    /// <summary>
    /// 获取position位置的Node
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Node GetItem(Vector2 position)
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (nodes[i, j] != null)
                {
                    if (Vector2.Distance(nodes[i, j].pos, position) < 0.5f)
                    {
                        return nodes[i, j];
                    }
                }

            }
        }
        return null;
    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    private void InitNote()
    {
        nodes = new Node[w, h];
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
                nodes[i, j] = notePoint;
                
                if (j == 6 || j == 7 )
                {
                    nodes[i, j].isCanClick = false;
                    if ((i == 1 && j == 6) || (i == 1 && j == 7)
                      || (i == 3 && j == 6) || (i == 3 && j == 7))
                      //|| (i == 1 && j == 7) || (i == 1 && j == 9)
                      //|| (i == 3 && j == 7) || (i == 3 && j == 9))
                    {
                        nodes[i, j].CanArrive = false;
                        continue;
                    }
                }
                else if ((i == 1 && j == 2) || (i == 1 && j == 4)
                    || (i == 2 && j == 3) || (i == 3 && j == 2) || (i == 3 && j == 4)
                    || (i == 1 && j == 9) || (i == 1 && j == 11)
                    || (i == 2 && j == 10) || (i == 3 && j == 9) || (i == 3 && j == 11))
                {
                    nodes[i, j].pointType = PointType.LineCamp;

                }
                else if ((i == 1 && j == 0) || (i == 3 && j == 0) || (i == 1 && j == 13) || (i == 3 && j == 13))
                {
                    nodes[i, j].pointType = PointType.BaseCamp;
                    nodes[i, j].chessType = ChessType.BlackChess;
                    GameObject go = Instantiate(Tpr, tr);
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                }
                else if (j == 1 || j == 5 || j == 8 || j == 12 || (i == 0 && 0 < j && j < 6) || (i == 0 && 8 < j && j < 12)
                    || (i == 4 && 0 < j && j < 6) || (i == 4 && 8 < j && j < 12))
                {
                    nodes[i, j].pointType = PointType.RailWay;
                    nodes[i, j].chessType = ChessType.BlackChess;
                    GameObject go = Instantiate(Tpr, tr);
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                    nodes[i,j].Chessobj = go;
                }
                else
                {
                    nodes[i, j].pointType = PointType.HighWay;
                    nodes[i, j].chessType = ChessType.BlackChess;
                    GameObject go = Instantiate(Tpr, tr);
                    nodes[i, j].Chessobj = go;
                    go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                }
                
                //Debug.Log(notes[i, j].pointType);
            }
        }
    }
    public void ClearData()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (nodes[i, j] != null)
                {
                    nodes[i, j].parentNote = null;
                    nodes[i, j].Tstart = 0;
                    nodes[i, j].Tend = 0;
                }

            }
        }
    }
    /// <summary>
    /// 获取当前节点周围的node
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
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
                    list.Add(nodes[x, y]);
            }
        }
        return list;
    }
    /// <summary>
    /// 返回当前节点能移动到的节点
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<Node> CrossNode(Node node)
    {
        List<Node> list = new List<Node>();

        for (int i = node.x+1; i < w; i++)
        {
            if (nodes[i, node.y].isCanClick != true)
                continue;
            if (nodes[i,node.y].Chessobj!=null|| nodes[i, node.y].pointType!=PointType.RailWay)
            {
                list.Add(nodes[i, node.y]);
                break;
            }
            list.Add(nodes[i, node.y]);
        }
        for (int i = node.x-1; i > 0; i--)
        {
            if (nodes[i, node.y].isCanClick != true)
                continue;
            if (nodes[i, node.y].Chessobj != null|| nodes[i, node.y].pointType != PointType.RailWay)
            {
                list.Add(nodes[i, node.y]);
                break;
            }
            list.Add(nodes[i, node.y]);
        }
        for (int i = node.y+1; i < h; i++)
        {
            if (nodes[node.x, i].isCanClick != true)
                continue;
            if (nodes[node.x, i].Chessobj != null|| nodes[node.x, i].pointType != PointType.RailWay)
            {
                list.Add(nodes[node.x, i]);
                break;
            }
            list.Add(nodes[node.x, i]);
        }
        for (int i = node.y-1; i > 0; i--)
        {
            if (nodes[node.x, i].isCanClick != true)
                continue;
            if (nodes[node.x, i].Chessobj != null || nodes[node.x, i].pointType != PointType.RailWay)
            {
                list.Add(nodes[node.x, i]);
                break;
            }
            list.Add(nodes[node.x, i]);
        }  
            return list;
    }

}
