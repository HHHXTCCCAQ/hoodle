using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;
//love you into disease, but no medicine can.
//Created By xxx
public class AstarManager
{


    public Grid grid;
    public Node startNode;
    public Node endNode;
    private Gamemanager gamemanager;
    public AstarManager(Gamemanager gamemanager, Grid grid)
    {
        this.gamemanager = gamemanager;
        this.grid = grid;
    }
    public Node GetNode(Vector2 pos, int selectStep)
    {
        Node node = grid.GetItem(pos);
        if (selectStep == 1)
        {
            startNode = node;
        }
        else if (selectStep == 2)
        {
            endNode = node;
        }
        return node;
    }

    int GetDistanceNodes(Node a, Node b)
    {

        int cntX = Mathf.Abs(a.x - b.x);
        int cntY = Mathf.Abs(a.y - b.y);
        // 判断到底是那个轴相差的距离更远 ， 实际上，为了简化计算，我们将代价*10变成了整数。
        if (cntX > cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }
    }

    public bool FindPath()
    {
        List<Node> opList = new List<Node>();
        List<Node> closeList = new List<Node>();
        grid.ClearData();
        if (startNode != null)
        {
            opList.Add(startNode);

            while (opList.Count > 0)
            {
                Node currentNode = opList[0];

                for (int i = 0; i < opList.Count; i++)
                {
                    if (opList[i].Tend < currentNode.Tend && opList[i].Tstart < currentNode.Tstart)
                    {
                        currentNode = opList[i];
                    }
                }
                opList.Remove(currentNode);
                closeList.Add(currentNode);
                if (currentNode == endNode)
                {
                    return true;
                }
                foreach (var item in grid.ArroundNote(currentNode))
                {
                    if (!item.isCanClick || closeList.Contains(item))
                        continue;
                    int Cost = currentNode.Tstart + GetDistanceNodes(currentNode, item);
                    if (Cost < item.Tstart || !opList.Contains(item))
                    {
                        item.Tstart = Cost;
                        item.Tend = GetDistanceNodes(item, endNode);
                        item.parentNote = currentNode;
                        if (!opList.Contains(item))
                        {
                            opList.Add(item);
                        }
                    }
                }

            }


        }
        return false;

    }
}
