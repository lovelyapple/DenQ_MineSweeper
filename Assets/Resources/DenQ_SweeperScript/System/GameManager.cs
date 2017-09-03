﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ.Mgr;

public class GameManager : MangerBase<GameManager>
{

    bool isGameReady = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        isGameReady = false;
        SetInstance(this);
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadResources());
    }
    IEnumerator LoadResources()
    {
        DenQLogger.SDebug("Begin Load Table");
        StartCoroutine(TableManager.ImportTableAll());
        while (!TableManager.IsFinished()) yield return null;

        DenQLogger.SDebug("Begin Load Resources");
        ResourcesManager.GetInstance().InitResourceManager();
        while (!ResourcesManager.GetInstance().IsLoadFinished()) yield return null;
        isGameReady = true;
    }
    public bool IsGameReady()
    {
        return isGameReady;
    }
    // Update is called once per frame
    void Update()
    {

    }
}