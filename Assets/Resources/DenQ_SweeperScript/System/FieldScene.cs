using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
public class FieldScene : MonoBehaviour
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
            ResourcesManager.GetInstance().ReadPath();
            isInited = true;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            var go = RootHolder.effectRootObj;
            go = RootHolder.fieldObjectRootObj;
            go = RootHolder.systemRootObj;
            go = RootHolder.UIRootObj;
        }
    }
}
