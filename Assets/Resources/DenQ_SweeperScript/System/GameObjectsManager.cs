using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;

public class GameObjectsManager : MangerBase<GameObjectsManager>
{

    //0スタート
    private static long objectIndex = -1;
    void Awake()
    {
        SetInstance(this);
        ResetIdx();
    }
    public long RigistObjectId()
    {
        objectIndex++;
        return objectIndex;
    }
    public void ResetIdx()
    {
        objectIndex = -1;
    }
}
