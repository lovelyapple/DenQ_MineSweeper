using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
public class BattleScene : MonoBehaviour
{
    void Awake()
    {
        //TableManager.GetInstance().ReadTable();
        //ResourcesManager.GetInstance().ReadPath();
    }
    bool isInited = false;
    void Update()
    {
        if(!isInited)
        {
            TableManager.GetInstance().ReadTable();
            isInited = true;
        }
    }
}
