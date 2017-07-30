using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
using DenQ.Mgr;

public class GameObjectsManager : MangerBase<GameObjectsManager>
{

    //0スタート
    private static long objectIndex = -1;
    private static Dictionary<long,ObjectBaseData> objDic = new Dictionary<long, ObjectBaseData>();
    void Awake()
    {
        SetInstance(this);
        ResetIdx();
        objDic.Clear();
    }
    public long RigistObjectId(ObjectBaseData objData)
    {
        objectIndex++;
        objDic.Add(objectIndex,objData);
        return objectIndex;
    }
    public void RemoveObjectById(long id)
    {
        if(objDic.ContainsKey(id)) objDic[id] = null;//Idは保留しとく
    }
    public ObjectBaseData GetObjectBaseDataByID(long id)
    {
        if(objDic.ContainsKey(id))
        {
            return objDic[id];
        }
        DenQLogger.GError(string.Format("could not find object ID {0}",id));
        return null;
    }
    public void ResetIdx()
    {
        objectIndex = -1;
    }
}
