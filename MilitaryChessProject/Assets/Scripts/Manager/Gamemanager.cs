using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;
//love you into disease, but no medicine can.
//Created By xxx
public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;
    public GameObject borad1;
    public GameObject borad2;
    public GameObject borad3;
    public Transform trans;
    public List<AStartTest.NotePoint> a = new List<AStartTest.NotePoint>();
    private GameObject chessprefab;
    private List<ChessType> blueChessList = new List<ChessType>();
    private List<ChessType> redChessList = new List<ChessType>();
    
    private void Start()
    {
        Instance = this;
        chessprefab = Resources.Load<GameObject>("Chess/BlankChess");
        InitChess();
        InitChessDict();
    }


    public void InitChess()
    {
        CreatChess(5, 6, borad1, 1);
        CreatChess(5, 6, borad2, 2);   
        //foreach (var item in a)
        //{
        //    Debug.Log(item.Pos);
        //}
        // CreatChess(3, 3, borad3);
    }
    public void CreatChess(int xcount, int ycount, GameObject borad, int dot)
    {
        float broadWidth = borad.GetComponent<RectTransform>().rect.width;
        float broadHight = borad.GetComponent<RectTransform>().rect.height;
        Vector2 startPoint = new Vector2(borad.transform.localPosition.x - broadWidth / 2,
          borad.transform.localPosition.y - broadHight / 2);
        float unitXvalue = broadWidth / (xcount - 1);
        float unitYvalue = broadHight / (ycount - 1);
        for (int i = 0; i < xcount; i++)
        {
            for (int j = 0; j < ycount; j++)
            {
                Vector2 pos= startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
                AStartTest.NotePoint notePoint = new AStartTest.NotePoint(pos,i,j);
               // Instantiate(notePoint, trans) as GameObject;
                a.Add(notePoint);
                if ((i == 1 && j == dot) || (i == 1 && j == dot + 2)
                     || (i == 2 && j == dot + 1) || (i == 3 && j == dot) || (i == 3 && j == dot + 2))
                    continue;
                GameObject go = Instantiate(chessprefab, trans);
                notePoint.chess = go;
                go.GetComponent<BlankChess>().notePoint = notePoint;
                notePoint.chess.transform.localPosition = notePoint.pos;
                notePoint.CanArrive = false;
                //go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
            }
        }
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
                redChessList.Remove(redChessList[randomnumb]);
                go.GetComponent<Chess>().chessType = redChessList[randomnumb];

            }

        }
        Debug.Log(blueChessList.Count + "||" + redChessList.Count);
        return go;
    }
}
