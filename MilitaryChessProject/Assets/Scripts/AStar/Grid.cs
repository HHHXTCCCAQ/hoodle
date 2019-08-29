using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//love you into disease, but no medicine can.
//Created By xxx
public class Grid : MonoBehaviour
{

    public class Node
    {

        public Vector2 pos;
        public int x, y;
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
                if (Vector2.Distance(notes[i, j].pos, position) < 0.5f)
                {
                    return notes[i, j];
                }
            }
        }
        return null;
    }
    private void InitNote()
    {
        notes = new Node[w, h];
        GameObject Tpr = Resources.Load<GameObject>("T");
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
                notes[i, j] = notePoint;
                // Instantiate(notePoint, trans) as GameObject;

                if ((i == 1 && j == 8) || (i == 1 && j ==10)
                     || (i == 3 && j == 8) || (i == 3 && j == 10)
                     || (i == 1 && j == 7) || (i == 1 && j == 9) 
                     || (i == 3 && j == 7) || (i == 3 && j == 9))
                {
                    notes[i, j].CanArrive = false;
                    continue;
                }
                  
              
                GameObject go = Instantiate(Tpr, tr);               
                go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
            }
        }
    }
    public void ClearData()
    {
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                notes[i, j].parentNote = null;
                notes[i, j].Tstart = 0;
                notes[i, j].Tend = 0;
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
