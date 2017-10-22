﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
using DenQModel;
public class FieldManager : ManagerBase<FieldManager>
{
    //public Vector3 satrtPos = new Vector3();
    
    //private static bool isCreatingMap = false;
    // Use this for initialization
    void Awake()
    {
        SetInstance(this);
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void CreateField(ulong fieldCode)
    {
        FieldModel.Get().SetUp(fieldCode);
        FieldModel.Get().CreateField();
    }
    public void ClearField()
    {
    }
}