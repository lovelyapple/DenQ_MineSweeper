using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using DenQ.Mgr;
public enum PREFAB_NAME
{
    a, ab, c,
}
///ここでリソースのロードを行う媒体
public class ResourcesManager : MangerBase<ResourcesManager>
{
    const string fieldObjectPrefabPath = "/Resources/DenQ_SweeperPrefab";
    const int prefabIdxCnt = 7;
    static string resourcesPath = "/Resources/";
    static uint resourcePathCharaCnt = 0;
    ///masterCode を　キーとしたPrefabのDic
    Dictionary<string, PrefabContainer> prefabPathList = new Dictionary<string, PrefabContainer>();
    void OnEnable()
    {
        Instantiate();
    }
    void Instantiate()
    {
        resourcesPath = Application.dataPath + resourcesPath;
        resourcePathCharaCnt = (uint)resourcesPath.Length;
        if (prefabPathList == null || prefabPathList.Count <= 0)
        {
            prefabPathList = new Dictionary<string, PrefabContainer>();
            var dir = new DirectoryInfo(Application.dataPath + fieldObjectPrefabPath);
            Debug.Log(dir);
            FileInfo[] infos = dir.GetFiles("*.prefab", SearchOption.AllDirectories);
            prefabPathList = infos.ToDictionary(info => info.Name, info => new PrefabContainer(info,(int)resourcePathCharaCnt,prefabIdxCnt));
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PreLoadPrefab();
            //CreateGameObject(null, 1000, "FieldBlock");
        }
    }
    ///FieldItemTableからNameを取り出し、インスタンスを作る
    public GameObject CreateFieldObjectInstance(ulong itemCode, Transform parent, bool saveCache = true)
    {
        var fieldItemData = FieldItemTableHelper.GetFieldItemData(itemCode);
        if (fieldItemData == null) return null;
        return CreateFieldObjectInstance(fieldItemData.name, parent, saveCache);
    }
    ///PrefabPathDictionから名前のcontainerを探し、Prefabがなければ作り、なければLoad
    public GameObject CreateFieldObjectInstance(string name, Transform parent, bool saveCache = true)
    {
        var container = new PrefabContainer();
        if (!prefabPathList.TryGetValue(name, out container))
        {
            DenQLogger.GWarn("could not find the prefab in Dictionary name : " + name);
            return null;
        }
        if (container.prefab != null)
        {
            return container.prefab;
        }
        var go = (GameObject)Resources.Load(container.filePath);
        if (go == null)
        {
            DenQLogger.SWarn("could not load resources in file : " + container.filePath);
            return null;
        }
        if (saveCache)
        {
            container.prefab = go;
            return (GameObject)Instantiate(container.prefab, parent);
        }
        else
        {
            return (GameObject)Instantiate(go, parent);
        }
    }
    ///実験的に中身のチェック、基本実行しない
    void PreLoadPrefab()
    {
        foreach (var name in prefabPathList.Keys)
        {
            var container = prefabPathList[name];
            var go = (GameObject)Resources.Load(container.loadPath);
            if (go == null)
            {
                DenQLogger.SWarn("could not load resources in file : " + container.filePath);
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