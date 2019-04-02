using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class ObjPool : MonoBehaviour
{
    // Use this for initialization
    public static ObjPool _instance;
    private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, GameObject> _prefab = new Dictionary<string, GameObject>();
    void Awake()
    {
        _instance = this;
    }
    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>
    /// 在对象池获取对象
    /// </summary>
    /// <param name="objName">对象名称</param>
    /// <returns></returns>
    public GameObject GetObj(string objName)
    {
        GameObject result = null;
        if (_pool.ContainsKey(objName))
        {
            if (_pool[objName].Count > 0)
            {
                result = _pool[objName][0];
                result.SetActive(true);
                _pool[objName].Remove(result);
                return result;
            }
        }
        GameObject prefab = null;
        if (_prefab.ContainsKey(objName))
        {
            prefab = _prefab[objName];
        }
        else
        {
            prefab = Resources.Load<GameObject>("Prefabs/" + objName);
            _prefab.Add(objName, prefab);
        }
        result = Instantiate(prefab);
        result.name = objName;
        return result;
    }
    /// <summary>
    /// 回收到对象池
    /// </summary>
    /// <param name="obj"></param>
    public void RecycleObj(GameObject obj)
    {
        obj.SetActive(false);
        if (_pool.ContainsKey(obj.name))
        {
            _pool[obj.name].Add(obj);
        }
        else
        {
            _pool.Add(obj.name, new List<GameObject> { obj });
        }
    }
}
