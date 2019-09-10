using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;
using static Grid;
//love you into disease, but no medicine can.
//Created By xxx
public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public Transform Canvas;
    public Transform trans;
    [HideInInspector]
    public int selectStep = 0;
    public Grid grid;
    public Node[,] nodes;
    [HideInInspector]
    public int currentplayerType = 1;
    private GameObject chessprefab;
    private List<ChessType> blueChessList = new List<ChessType>();
    private List<ChessType> redChessList = new List<ChessType>();
    private AstarManager astarManager;
    public  ComputerAIAlgorithm aIAlgorithm;
    private Node startnode;
    private Node endnode;
    private ChessType startchessType;
    private ChessType endchessType;
    private PlayerType startpalyerType;
    private PlayerType endpalyerType;
    private void Awake()
    {
        Instance = this;
        chessprefab = Resources.Load<GameObject>("Chess/BlankChess");
        InitChessDict();
        nodes = grid.nodes;
        astarManager = new AstarManager(this, grid);

    }
    public void Update()
    {
        if (currentplayerType==2)
        {
            aIAlgorithm.jude();
            currentplayerType++;
        }

        if (currentplayerType == 3)
        {
            currentplayerType = 1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Vector2.one;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas as RectTransform,
                        Input.mousePosition, Camera.main, out pos);
            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                for (int j = 0; j < nodes.GetLength(1); j++)
                {
                    if (Vector2.Distance(pos, nodes[i, j].pos) <= 41f)
                    {
                        if (nodes[i, j].isCanClick == false)
                            continue;
                        if (selectStep == 1 && nodes[i, j].Chessobj == null)
                        {
                            GetNode(nodes[i, j].pos);                           
                            Debug.Log(nodes[i, j].pos + "+++");
                        }

                    }
                }
            }
        }       
        MoveBehavior();
    }   
    public void InitChessDict()
    {
        int key = 0;
        for (int i = 0; i < 4; i++)
        {
            blueChessList.Add((ChessType)i);
            blueChessList.Add((ChessType)i);
            blueChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            key = key + 3;
        }
        for (int i = 4; i < 9; i++)
        {
            blueChessList.Add((ChessType)i);
            blueChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            key = key + 2;
        }
        for (int i = 9; i < 12; i++)
        {
            blueChessList.Add((ChessType)i);
            redChessList.Add((ChessType)i);
            key++;
        }
    }
    int randomnumb = 0;
    public GameObject DrawChess()
    {
        randomnumb = Random.Range(1, 3);
        GameObject go = null;
        if (randomnumb == 1)
        {
            if (blueChessList.Count <= 0)
            {
                go = DrawChess();
            }
            else
            {
                randomnumb = Random.Range(0, blueChessList.Count);
                go = Resources.Load<GameObject>("Chess/" + blueChessList[randomnumb].ToString() + "_B");
                go.GetComponent<Chess>().chessType = blueChessList[randomnumb];
                go.GetComponent<Chess>().playerType = PlayerType.Blue;
                blueChessList.Remove(blueChessList[randomnumb]);
            }

        }
        else
        {
            if (redChessList.Count <= 0)
            {
                go = DrawChess();
            }
            else
            {
                randomnumb = Random.Range(0, redChessList.Count);
                go = Resources.Load<GameObject>("Chess/" + redChessList[randomnumb].ToString() + "_R");
                go.GetComponent<Chess>().chessType = redChessList[randomnumb];
                go.GetComponent<Chess>().playerType = PlayerType.Red;
                redChessList.Remove(redChessList[randomnumb]);
            }

        }
        return go;
    }

    public Node GetNode(Vector2 pos)
    {
        selectStep++;
        Node node = astarManager.GetNode(pos, selectStep);
        if (selectStep == 2)
        {
            endnode = node;
            Debug.Log(astarManager.FindPath());
            selectStep = 0;
        }
        else
        {
            startnode = node;
            Debug.Log(startnode + "---" + endnode);
        }
        return node;
    }

    public void MoveBehavior()
    {
        if (startnode != null && endnode != null)
        {
            startchessType = startnode.Chessobj.GetComponent<Chess>().chessType;
            startpalyerType = startnode.Chessobj.GetComponent<Chess>().playerType;
            if (endnode.Chessobj != null)
            {
                endchessType = endnode.Chessobj.GetComponent<Chess>().chessType;
                endpalyerType = endnode.Chessobj.GetComponent<Chess>().playerType;
            }
            else
            {
                endchessType = ChessType.Null;
                endpalyerType = PlayerType.Null;
            }

            if (startpalyerType.Equals(endpalyerType))
            {
                startnode = endnode = null;
            }
            else
            {
                if (startnode.pointType == PointType.RailWay && endnode.pointType == PointType.RailWay)
                {
                    Debug.Log(RailWayIsCanArrive());
                    if (startchessType == ChessType.Sapper || RailWayIsCanArrive())
                    {
                        JudgeFeasibility();
                    }
                }
                else if (endnode.pointType == PointType.LineCamp || endnode.pointType == PointType.BaseCamp)
                {
                    if (endchessType == ChessType.Null)
                    {
                        //TODO  查看两个点是否在移动范围内
                        if (AtherIsArrive())
                        {
                            //Debug.Log("Move to");
                            JudgeFeasibility();
                        }
                        else
                        {

                            Debug.Log("con  not  move");
                        }
                    }
                    else
                    {

                        Debug.Log("con  not  move");

                    }
                }
                else
                {
                    if (AtherIsArrive())
                    {
                        //Debug.Log("Move to");
                        JudgeFeasibility();
                    }
                    else
                    {

                        Debug.Log("con  not  move");
                    }
                }
                startnode = endnode = null;
            }


        }
    }
    /// <summary>
    /// 判断是否可行
    /// </summary>
    public void JudgeFeasibility()
    {
        if (startchessType == ChessType.Sapper)
        {
            switch (endchessType)
            {
                case ChessType.Mine:
                    DestroyEndChess();
                    break;
                case ChessType.Sapper:
                    DestroyBothChess();
                    break;
                case ChessType.Bomb:
                    DestroyBothChess();
                    break;
                case ChessType.ArmyFlag:
                    DestroyEndChess();
                    break;
                case ChessType.Null:

                    MoveToEndNode();
                    break;
                default:
                    Debug.Log("cont move");
                    break;
            }

        }
        else if (startchessType == ChessType.Bomb)
        {
            switch (endchessType)
            {
                case ChessType.Null:
                    MoveToEndNode();
                    break;
                default:
                    DestroyBothChess();
                    break;
            }
        }
        else if (startchessType == ChessType.Mine || startchessType == ChessType.ArmyFlag)
        {

            Debug.Log("can not move");
        }
        else
        {
            switch (endchessType)
            {
                case ChessType.Mine:

                    Debug.Log("can not move");
                    break;
                case ChessType.Bomb:
                    DestroyBothChess();
                    break;
                case ChessType.ArmyFlag:
 
                    Debug.Log("can not move");
                    break;
                case ChessType.Null:

                    MoveToEndNode();
                    break;
                default:
                    JudgeChess(startchessType, endchessType);
                    break;
            }
        }
    }
    /// <summary>
    /// 判断铁路之间是否能够到达
    /// </summary>
    /// <returns></returns>
    private bool RailWayIsCanArrive()
    {
        int index;
        if (startnode.x == endnode.x)
        {
            index = 0;
            for (int i = Mathf.Min(startnode.y, endnode.y) + 1; i < Mathf.Max(startnode.y, endnode.y); i++)
            {
                if (nodes[startnode.x, i].Chessobj != null)
                {
                    index++;
                }
            }
            if (index == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (startnode.y == endnode.y)
        {
            index = 0;
            for (int i = Mathf.Min(startnode.x, endnode.x) + 1; i < Mathf.Max(startnode.x, endnode.x); i++)
            {
                if (nodes[i, startnode.y].Chessobj != null)
                {
                    index++;
                }
            }
            if (index == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 判断不同道路是否到达
    /// </summary>
    /// <returns></returns>
    private bool AtherIsArrive()
    {
        if (Mathf.Abs(startnode.x - endnode.x) > 1 || Mathf.Abs(startnode.y - endnode.y) > 1)
        {
            return false;

        }
        else
        {
            if (startnode.pointType == PointType.LineCamp || endnode.pointType == PointType.LineCamp)
            {
                if (Mathf.Abs(startnode.x - endnode.x) <= 1 && Mathf.Abs(startnode.y - endnode.y) <= 1)
                {
                    return true;
                }
            }
            else
            {
                if ((Mathf.Abs(startnode.x - endnode.x) <= 1 && Mathf.Abs(startnode.y - endnode.y) == 0) ||
                    (Mathf.Abs(startnode.x - endnode.x) == 0 && Mathf.Abs(startnode.y - endnode.y) <= 1))

                {
                    return true;
                }
            }
        }
        return false;
    }

    private void MoveToEndNode()
    {
        endnode.CanArrive = false;
        endnode.Chessobj = startnode.Chessobj;
        endnode.Chessobj.transform.localPosition = endnode.pos;
        startnode.Chessobj = null;
        startnode.CanArrive = true;
        currentplayerType++;
    }
    /// <summary>
    /// 判断两个棋子大小，并作出相应操作
    /// </summary>
    private void JudgeChess(ChessType startchessType, ChessType endchessType)
    {
        if ((int)startchessType > (int)endchessType)
        {
            DestroyEndChess();
        }
        else if ((int)startchessType == (int)endchessType)
        {
            DestroyBothChess();
        }
        else
        {
        
            Debug.Log("Can not move");
        }
    }
    private void DestroyEndChess()
    {
        Destroy(endnode.Chessobj);
    
        MoveToEndNode();
    }
    private void DestroyBothChess()
    {
   
        currentplayerType++;
        endnode.CanArrive = true;
        startnode.CanArrive = true;
        Destroy(endnode.Chessobj);
        Destroy(startnode.Chessobj);
        endnode.Chessobj = null;
        startnode.Chessobj = null;
    }
    private void GameResult()
    {

    }
}
