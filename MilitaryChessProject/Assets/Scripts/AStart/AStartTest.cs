using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//love you into disease, but no medicine can.
//Created By xxx
public class AStartTest : MonoBehaviour
{

    public class NotePoint
    {
        public Vector2 pos;
        public GameObject chess;
        public int x, y;
        public bool CanArrive = true;
        public float Tstart;
        public float Tend;
        public NotePoint parentNote;
        public NotePoint(Vector2 pos,int x,int y)
        {
            this.pos = pos;
            this.x = x;
            this.y = y;
        }
    }
}
