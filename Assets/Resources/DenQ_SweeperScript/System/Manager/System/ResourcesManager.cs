﻿using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
///ここでリソースのロードを行う媒体
public class ResourcesManager : ManagerBase <ResourcesManager>
{
    const string fieldObjectPrefabPath = "/Resources/DenQ_SweeperPrefab";
    const int prefabIdxCnt = 7;
    static string resourcesPath = "/Resources/";
    static uint resourcePathCharaCnt = 0;
    ///masterCode を　キーとしたPrefabのDic
    Dictionary<string, PrefabContainer> prefabPathDict = new Dictionary<string, PrefabContainer>();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        SetInstance(this);
    }
    void OnEnable()
    {
        ReadPath();
    }
    public void ReadPath()
    {
        resourcesPath = Application.dataPath + resourcesPath;
        resourcePathCharaCnt = (uint)resourcesPath.Length;
        if (prefabPathDict == null || prefabPathDict.Count <= 0)
        {
            prefabPathDict = new Dictionary<string, PrefabContainer>();
            var dir = new DirectoryInfo(Application.dataPath + fieldObjectPrefabPath);
            Debug.Log(dir);
            FileInfo[] infos = dir.GetFiles("*.prefab", SearchOption.AllDirectories);
            prefabPathDict = infos.ToDictionary(info => info.Name, info => new PrefabContainer(info,(int)resourcePathCharaCnt,prefabIdxCnt));
        }
    }
    ///FieldItemTableからNameを取り出し、インスタンスを作る
    public GameObject CreateFieldObjectInstance(ulong itemCode, Transform parent, Vector3 pos,bool saveCache = true)
    {
        var fieldItemData = FieldItemTableHelper.GetFieldItemData(itemCode);
        if (fieldItemData == null)
        {
            Logger.GWarn("could not find itemCode + " + itemCode);
            return null;
        }
        return CreateFieldObjectInstance(fieldItemData.name, parent, pos,saveCache);
    }
    ///PrefabPathDictionから名前のcontainerを探し、Prefabがなければ作り、なければLoad
    public GameObject CreateFieldObjectInstance(string name, Transform parent, Vector3 pos,bool saveCache = true)
    {
        var container = new PrefabContainer();
        name += ".prefab";

        if (!prefabPathDict.TryGetValue(name, out container))
        {
            Logger.GWarn("could not find the prefab in Dictionary name : " + name);
            return null;
        }

        if (container.prefab == null)
        {
            container.prefab = (GameObject)Resources.Load(container.loadPath);
            
        }

        if (container.prefab == null)
        {
            Logger.SWarn("could not load resources in file : " + container.filePath);
            return null;
        }
        else
        {
           return  (GameObject)Instantiate(container.prefab,pos,Quaternion.identity, parent);
        }
    }
    ///実験的に中身のチェック、基本実行しない
    void PreLoadPrefab()
    {
        foreach (var name in prefabPathDict.Keys)
        {
            var container = prefabPathDict[name];
            var go = (GameObject)Resources.Load(container.loadPath);
            if (go == null)
            {
                Logger.SWarn("could not load resources in file : " + container.filePath);
            }
        }
    }
}
///Prefabの基本情報を保持するクラス
public class PrefabContainer
{
    public PrefabContainer() { }
    public PrefabContainer(FileInfo fileInfo,int headIndxCnt,int lastIdxCnt)
    {
        this.filePath = fileInfo.FullName;
        this.fileName = fileInfo.Name;
        this.loadPath = filePath.Remove(this.filePath.Length - lastIdxCnt, lastIdxCnt);
        this.loadPath = this.loadPath.Remove(0,headIndxCnt);
        return;
    }
    public string fileName;
    public string filePath = null;
    ///Prefabをロードする時に使うパス
    public string loadPath { get; private set; }
    public GameObject prefab = null;
}