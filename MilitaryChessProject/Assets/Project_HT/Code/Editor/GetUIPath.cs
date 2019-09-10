using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using Application = UnityEngine.Application;
using Button = UnityEngine.UI.Button;
using MenuItem = UnityEditor.MenuItem;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao

/* 注意事项：  UI命名规则
 Button: but_xxx;
 Text: txt_xxx;
 Image: img_xxx;
 */
public class GetUIPath : Editor
{

    public static Dictionary<string, string> typMap = new Dictionary<string, string>()
    {
        {"but",typeof(Button).Name },
        {"txt",typeof(Text).Name },
        {"img",typeof(Image).Name }
    };

    private static List<UIInfo> uinfo = new List<UIInfo>();

    private static string Eg_str =
        "using System.Collections;" +
        "\r\nusing System.Collections.Generic;" +
        "\r\nusing UnityEngine;\r\tusing UnityEngine.UI;" +
        "\r\n//Wait for me, I don\'t want to let you down\r\n" +
        "//love you into disease, but no medicine can.\r\n" +
        "//Created By HeXiaoTao\r\n" +
        "public class @Name : MonoBehaviour {\r\n\r\n\t" +
        "@File1// Use this for initialization\r\n\tvoid Start () {@Body1}\r\n\t\r\n\t" +
        "// Update is called once per frame\r\n\tvoid Update () {\r\n\t\t\r\n\t}\r\n" +
        "}";
    static void WriteScript()
    {

        for (int i = 0; i < uinfo.Count; i++)
        {
            File1 += uinfo[i].File1 + ";\r\n";
            Body1 += uinfo[i].Body1 + ";\r\n";
            Debug.Log(uinfo[i].File1 + "_____________" + uinfo[i].Body1 + "______________" + uinfo[i].Body2);
        }
        Debug.Log(File1 + "---------------" + Body1);
        Eg_str = Eg_str.Replace("@File1", File1);
        Eg_str = Eg_str.Replace("@Body1", Body1);
        //储存文档
        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.FileName = "TestName.cs";
        //string path = Environment.CurrentDirectory.Replace("/", @"\");
        if (saveFile.ShowDialog() == DialogResult.OK)
        {
            string[] name = saveFile.FileName.Split('\\');
            string nameStr = name[name.Length - 1].Replace(".cs", "");
            Eg_str = Eg_str.Replace("@Name", nameStr);
            File.WriteAllText(saveFile.FileName, Eg_str,System.Text.Encoding.UTF8);
        }
    }
    /// <summary>
    /// 获取物体的路径
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    static string GetgameObjectPath(Transform go)
    {
        string path = "";
        while (go!= Selection.gameObjects[0].transform)
        {
            path = path.Insert(0, "/");
            path = path.Insert(0, go.name);
            go = go.parent;
        }
        return path;
    }
    /// <summary>
    /// 获取物体的基本信息(名字，校准key，路径)，并储存
    /// </summary>
    /// <param name="tf"></param>
    static void GetChildinfo(Transform tf)
    {
        Debug.Log(tf.name);

        foreach (Transform tfChild in tf)
        {

            string contrastKey = tfChild.name.Substring(0, 3);
            if (typMap.ContainsKey(contrastKey))
            {
                Debug.Log(tfChild.name + "------" + contrastKey + "------" + GetgameObjectPath(tfChild));
                UIInfo uinf = new UIInfo(tfChild.name, contrastKey, GetgameObjectPath(tfChild));
                uinfo.Add(uinf);
            }
            if (tfChild.childCount >= 0)
            {
                GetChildinfo(tfChild);
            }
        }



    }

    private static string File1 = "";
    private static string Body1 = "";
    [MenuItem("MyTools/GetScript")]
    static void CreatScript()
    {
        GameObject[] select = Selection.gameObjects;
        if (select.Length == 1)
        {
            Transform selectGo = select[0].transform;
            GetChildinfo(selectGo);
            WriteScript();
        }
        else
        {
            EditorUtility.DisplayDialog("警告", "你只能选择一个GameObject", "确定");
        }


    }
}
