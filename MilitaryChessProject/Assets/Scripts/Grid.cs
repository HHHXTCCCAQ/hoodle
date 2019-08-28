﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//love you into disease, but no medicine can.
//Created By xxx
public class Grid : MonoBehaviour
{

    public class Note
    {

        public Vector2 pos;
        public int x, y;
        public bool CanArrive = true;
        public float Tstart;
        public float Tend;
        public Note parentNote;
        public Note(Vector2 pos, int x, int y)
        {
            this.pos = pos;
            this.x = x;
            this.y = y;
        }
    }
    private float w = 5, h = 17;
    public Transform tr;
    private void Awake()
    {
        InitNote();
    }

    private void InitNote()
    {
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
                AStartTest.NotePoint notePoint = new AStartTest.NotePoint(pos, i, j);
                // Instantiate(notePoint, trans) as GameObject;

                //if ((i == 1 && j == dot) || (i == 1 && j == dot + 2)
                //     || (i == 2 && j == dot + 1) || (i == 3 && j == dot) || (i == 3 && j == dot + 2))
                //    continue;
                GameObject go = Instantiate(Tpr, tr);

                go.transform.localPosition = startPoint + new Vector2(i * unitXvalue, j * unitYvalue);
            }
        }
    }
}
