using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;
using static Config;
using System.Threading;
//love you into disease, but no medicine can.
//Created By xxx
public class ComputerAIAlgorithm : MonoBehaviour
{
    public List<Node> PlayerNodes = new List<Node>();
    public List<Node> AINodes = new List<Node>();
    public Dictionary<int, int> scoreDict = new Dictionary<int, int>();
    public Dictionary<int, Node[]> actionDict = new Dictionary<int, Node[]>();
    public Dictionary<int, Node[]> nodeDict = new Dictionary<int, Node[]>();

    public List<Node[]> nodelist = new List<Node[]>();
    public List<Node[]> actionlist = new List<Node[]>();
    public List<int> score = new List<int>();
    private Gamemanager gamemanager;
    private Grid grid;
    private Node startnode;
    private Node endnode;
    private Node[,] nodes;
    private ChessType startchessType;
    private ChessType endchessType;
    private PlayerType startpalyerType;
    private PlayerType endpalyerType;
    private AIBehavior aiBehavior;
    private bool action = false;
    private Thread ai;
    private bool getData;
    private bool getchessType;
    public void jude()
    {
        ai = new Thread(JudgeScore);
        ai.Start();
    }
    private void Start()
    {
        getData = false;
        getchessType = false;
        gamemanager = Gamemanager.Instance;
        grid = gamemanager.grid;
        nodes = gamemanager.nodes;
        aiBehavior = AIBehavior.Null;
    }
    ChessType chessType;
    int currentnodeindex = 0;
    private void Update()
    {
        //if (isdestroy)
        //{
        //   // isdestroy = false;
        //    //Destroy(endnode.Chessobj);
        //}
        //if (getData)
        //{
        //    getData = false;
        //    chessType = AINodes[currentnodeindex].Chessobj.GetComponent<Chess>().chessType;
        //}
        //if (getchessType)
        //{
        //    getchessType = false;
        //    //startchessType = startnode.Chessobj.GetComponent<Chess>().chessType;
        //    startpalyerType = startnode.Chessobj.GetComponent<Chess>().playerType;
        //    if (endnode.Chessobj != null)
        //    {
        //        //endchessType = endnode.Chessobj.GetComponent<Chess>().chessType;
        //        endpalyerType = endnode.Chessobj.GetComponent<Chess>().playerType;
        //    }
        //    else
        //    {
        //        //endchessType = ChessType.Null;
        //        endpalyerType = PlayerType.Null;
        //    }
        //}
        if (actionlist.Count > 0 && index > 0)
        {
            index = 0;
            Debug.Log("Action");
            int cc = Random.Range(0, actionlist.Count);
            startnode = actionlist[cc][0];
            endnode = actionlist[cc][1];
            action = true;
            actionlist.Clear();
            score.Clear();
            nodelist.Clear();
            BehaviorAction();
            Debug.Log(aiBehavior);
            aiBehavior = AIBehavior.Null;
            // MoveBehavior();
        }
        if (aiBehavior != AIBehavior.Null)
        {
            Debug.Log("add score");
            BehaviorAction();
            aiBehavior = AIBehavior.Null;
        }
    }
    public void JudgeScore()
    {
        index = 0;
        Debug.Log(AINodes.Count);
        for (int i = 0; i < AINodes.Count; i++)
        {
            //currentnodeindex = i;
            chessType = AINodes[i].chessType;
            //getData = true;
            if (chessType == ChessType.ArmyFlag || chessType == ChessType.Mine)
                continue;
            Ts(AINodes[i]);
        }
        int score1;
        if (score.Count > 0)
        {
            score1 = score[0];
        }
        else
        {
            score1 = 0;
        }
        for (int i = 0; i < score.Count; i++)
        {
            if (score1 < score[i])
            {
                score1 = score[i];

            }
        }
        int actionCount = 0;

        for (int i = 0; i < score.Count; i++)
        {
           // Debug.Log(score1);
            if (score1 == score[i])
            {
                // actionlist.Add(nodelist[i]);
                int count = 0;

                foreach (var item in actionlist)
                {
                    if (nodelist[i] == item)
                    {
                        count++;
                    }

                }
                if (count == 0)
                {
                    actionlist.Add(nodelist[i]);
                    actionCount++;
                }

            }
        }
        index = 1; 
        Debug.Log("close");
        Debug.Log(score.Count);
        Debug.Log(actionlist.Count);
        ai.Abort();
    }

    private void Ts(Node node)
    {
        List<Node> nodes = new List<Node>();
        startnode = node;
        foreach (var item in grid.ArroundNote(node))
        {
            if (!item.CanArrive)
                continue;
            nodes.Add(item);
        }
        List<Node> currentnodes = grid.CrossNode(node);
        if (node.pointType == PointType.RailWay)
        {
            for (int i = 0; i < currentnodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (currentnodes[i] != nodes[j])
                    {
                        nodes.Add(currentnodes[i]);
                    }
                }
            }
        }
        foreach (var tpnode in nodes)
        {
            if (tpnode.Chessobj != null)
            {

                if (tpnode.chessType == ChessType.BlackChess)
                {
                    continue;
                }
            }
            endnode = tpnode;
            MoveBehavior();
        }
    }
    private int index = 0;
    private void Judge()
    {

    }

    public void MoveBehavior()
    {
       // Debug.Log(startnode.chessType + "----" + endnode.chessType);
        if (startnode != null && endnode != null)
        {
            Debug.Log(startnode.chessType + "----" + endnode.chessType);
            startchessType = startnode.chessType;
            endchessType = endnode.chessType;
            getchessType = true;
            startpalyerType = startnode.playerType;
            endpalyerType = endnode.playerType;
            Debug.Log(startnode.playerType + "----" + endnode.playerType);
            if (startpalyerType.Equals(endpalyerType))
            {
                startnode = endnode = null;
            }
            else
            {
                if (startnode.pointType == PointType.RailWay && endnode.pointType == PointType.RailWay)
                {
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
                            JudgeFeasibility();
                        }
                        else
                        {
                            aiBehavior = AIBehavior.Null;
                            AddScore(aiBehavior);
                            Debug.Log("con  not  move");
                        }
                    }
                    else
                    {
                        aiBehavior = AIBehavior.Null;
                        AddScore(aiBehavior);
                        Debug.Log("con  not  move");
                    }
                }
                else
                {
                    if (AtherIsArrive())
                    {
                        JudgeFeasibility();
                    }
                    else
                    {
                        aiBehavior = AIBehavior.Null;
                        AddScore(aiBehavior);
                        Debug.Log("con  not  move");
                    }
                }
                // startnode = endnode = null;
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
                    aiBehavior = AIBehavior.EatChess;
                    AddScore(aiBehavior);
                    //DestroyEndChess();
                    break;
                case ChessType.Sapper:
                    aiBehavior = AIBehavior.EatEach;
                    AddScore(aiBehavior);
                    //DestroyBothChess();
                    break;
                case ChessType.Bomb:
                    aiBehavior = AIBehavior.EatEach;
                    AddScore(aiBehavior);
                    // DestroyBothChess();
                    break;
                case ChessType.ArmyFlag:
                    aiBehavior = AIBehavior.EatChess;
                    AddScore(aiBehavior);
                    //DestroyEndChess();
                    break;
                case ChessType.Null:
                    aiBehavior = AIBehavior.Move;
                    AddScore(aiBehavior);
                    //MoveToEndNode();
                    break;
                default:
                    aiBehavior = AIBehavior.Null;
                    AddScore(aiBehavior);
                    Debug.Log("cont move");
                    break;
            }

        }
        else if (startchessType == ChessType.Bomb)
        {
            switch (endchessType)
            {
                case ChessType.Null:
                    aiBehavior = AIBehavior.Move;
                    AddScore(aiBehavior);
                    //MoveToEndNode();
                    break;
                default:
                    aiBehavior = AIBehavior.EatEach;
                    AddScore(aiBehavior);
                    //DestroyBothChess();
                    break;
            }
        }
        else if (startchessType == ChessType.Mine || startchessType == ChessType.ArmyFlag)
        {
            aiBehavior = AIBehavior.Null;
            AddScore(aiBehavior);
            Debug.Log("can not move");
        }
        else
        {
            switch (endchessType)
            {
                case ChessType.Mine:
                    aiBehavior = AIBehavior.Null;
                    AddScore(aiBehavior);
                    Debug.Log("can not move");
                    break;
                case ChessType.Bomb:
                    aiBehavior = AIBehavior.EatEach;
                    AddScore(aiBehavior);
                    //DestroyBothChess();
                    break;
                case ChessType.ArmyFlag:
                    aiBehavior = AIBehavior.Null;
                    AddScore(aiBehavior);
                    Debug.Log("can not move");
                    break;
                case ChessType.Null:
                    aiBehavior = AIBehavior.Move;
                    AddScore(aiBehavior);
                    //MoveToEndNode();
                    break;
                default:
                    JudgeChess(startchessType, endchessType);
                    break;
            }
        }
       // Debug.Log(aiBehavior);
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
        if (action)
        {
            AINodes.Remove(startnode);
            AINodes.Add(endnode);        
            endnode.CanArrive = false;
            endnode.Chessobj = startnode.Chessobj;
            startnode.Chessobj.transform.localPosition = endnode.pos;
            //endnode.Chessobj.transform.localPosition = endnode.pos;
            endnode.chessType = startnode.chessType;
            startnode.Chessobj = null;
            startnode.CanArrive = true;
            startnode.chessType = ChessType.Null;
            startnode.playerType = PlayerType.Null;
            endnode.playerType = PlayerType.Blue;
            startnode = endnode = null;
            action = false;
        }
        else
        {
            //score.Add(0);
            //Node[] cnodes = new Node[2];
            //cnodes[0] = startnode;
            //cnodes[1] = endnode;
            //nodelist.Add(cnodes);
            //index++;
        }



    }
    /// <summary>
    /// 判断两个棋子大小，并作出相应操作
    /// </summary>
    private void JudgeChess(ChessType startchessType, ChessType endchessType)
    {
        if ((int)startchessType > (int)endchessType)
        {
            aiBehavior = AIBehavior.EatChess;
            AddScore(aiBehavior);
            //DestroyEndChess();
        }
        else if ((int)startchessType == (int)endchessType)
        {
            aiBehavior = AIBehavior.EatEach;
            AddScore(aiBehavior);
            //DestroyBothChess();
        }
        else
        {
            aiBehavior = AIBehavior.Null;
            AddScore(aiBehavior);
            Debug.Log("Can not move");
        }
    }
    private void DestroyEndChess()
    {
       // aiBehavior = AIBehavior.EatChess;
        if (action)
        {
            Destroy(endnode.Chessobj);
            //isdestroy = true;
            MoveToEndNode();
        }
        else
        {
            //if (endnode.Chessobj != null)
            //{
            //    Debug.Log(122);
            //    // score.Add((int)endnode.Chessobj.GetComponent<Chess>().chessType);
            //    score.Add((int)endnode.chessType);
            //}
            //else
            //{
            //    score.Add(0);
            //}
            //Node[] cnodes = new Node[2];
            //cnodes[0] = startnode;
            //cnodes[1] = endnode;
            //nodelist.Add(cnodes);
        }

    }
    private void DestroyBothChess()
    {
        if (action)
        {
            AINodes.Remove(startnode);
            Debug.Log(AINodes.Count);
            endnode.CanArrive = true;
            startnode.CanArrive = true;
            Destroy(endnode.Chessobj);
            Destroy(startnode.Chessobj);
            endnode.Chessobj = null;
            startnode.Chessobj = null;
            startnode.chessType = ChessType.Null;
            endnode.chessType = ChessType.Null;
            startnode.playerType = PlayerType.Null;
            endnode.playerType = PlayerType.Null;
            startnode = endnode = null;
            action = false;
        }
        else
        {
            //if (endnode.Chessobj != null)
            //{
            //    score.Add(1);
            //}
            //else
            //{
            //    score.Add(0);

            //}
            //Node[] cnodes = new Node[2];
            //cnodes[0] = startnode;
            //cnodes[1] = endnode;
            //nodelist.Add(cnodes);
        }
    }

   public void  AddScore(AIBehavior behavior)
    {
        Node[] cnodes = new Node[2];
        switch (behavior)
        {
            case AIBehavior.EatChess:
                score.Add((int)endchessType);
                //Node[] cnodes = new Node[2];
                cnodes[0] = startnode;
                cnodes[1] = endnode;
                nodelist.Add(cnodes);
                break;
            case AIBehavior.EatEach:
                score.Add((int)endchessType);
                //Node[] cnodes = new Node[2];
                cnodes[0] = startnode;
                cnodes[1] = endnode;
                nodelist.Add(cnodes);
                break;
            case AIBehavior.Move:
                score.Add(2);
                //Node[] cnodes = new Node[2];
                cnodes[0] = startnode;
                cnodes[1] = endnode;
                nodelist.Add(cnodes);
                break;
            case AIBehavior.Null:
                //score.Add(0);
                //Node[] cnodes = new Node[2];
                //cnodes[0] = startnode;
                //cnodes[1] = endnode;
                //nodelist.Add(cnodes);
                break;
            default:
                break;
        }
        startnode = endnode = null;
        Debug.Log(score.Count);
    }

    public void BehaviorAction()
    {
        switch (aiBehavior)
        {
            case AIBehavior.EatChess:
                DestroyEndChess();
                break;
            case AIBehavior.EatEach:
                DestroyBothChess();
                break;
            case AIBehavior.Move:
                MoveToEndNode();
                break;
            case AIBehavior.Null:
                break;
            default:
                break;
        }
    }
}
