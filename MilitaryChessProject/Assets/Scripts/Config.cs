using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//love you into disease, but no medicine can.
//Created By xxx
public class Config
{
    //玩家类型
    public enum PlayerType
    {
        Red,
        Blue
    }
   
    public enum ChessType
    {
        Mine = 0,//地雷
        Sapper = 1,//工兵
        PlatoonLeader = 2,//排长
        CompanyCommander = 3,//连长
        Bomb = 4,//炸弹
        Abteilungkommandeur = 5,//营长
        RegimentalCommander = 6,//团长
        BrigadeCommander = 7,//旅长
        DivisionCommander = 8,//师长
        ArmyCommander = 9,//军长
        Commander = 10,//司令
        ArmyFlag = 11,//军旗


    }
    public enum RoadType
    {
        HighWay,//公路
        RailWay//铁路
    }
   
}
