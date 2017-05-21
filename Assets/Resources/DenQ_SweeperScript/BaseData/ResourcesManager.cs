using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using DenQ.Mgr;
public enum PREFAB_NAME
{
    //------------マネージャー系
    RESOURCES_HOLDER = 0,
    FIELD_ROOT,
    ITEM_ROOT,
    //BOMB_ROOT,//現段階は使わない
    FIELD_MGR,
    FIELD_FIELD,

    //------------普通のプレハブ

    FIELD_BLOCK,
    FIELD_BOMB,
    FIELD_ITEM,//暫定

    PREFAB_MAX,
};
public class ResourcesManager : MangerBase<ResourcesManager>
{
    public bool LoadCompleted = false;
    public int count = 0;
    public Dictionary<PREFAB_NAME, ResourceInfo> PrefabHolders = new Dictionary<PREFAB_NAME, ResourceInfo>()
    {
            //{PREFAB_NAME.RESOURCES_HOLDER,
            //new ResourceInfo(PREFAB_NAME.RESOURCES_HOLDER,"DenQ_SweeperPrefab/ManagerPrefab/ResourceHolder")},
            {PREFAB_NAME.FIELD_ROOT,
            new ResourceInfo(PREFAB_NAME.FIELD_ROOT,"DenQ_SweeperPrefab/RootPrefab/FieldRoot")},
            {PREFAB_NAME.ITEM_ROOT,
            new ResourceInfo(PREFAB_NAME.ITEM_ROOT,"DenQ_SweeperPrefab/RootPrefab/ItemRoot")},
            //{PREFAB_NAME.BOMB_ROOT,
            //new ResourceInfo(PREFAB_NAME.BOMB_ROOT,"DDenQ_SweeperPrefab/RootPrefab/BombRoot")},
            {PREFAB_NAME.FIELD_MGR,
            new ResourceInfo(PREFAB_NAME.FIELD_MGR,"DenQ_SweeperPrefab/ManagerPrefab/FieldManager")},
            {PREFAB_NAME.FIELD_FIELD,
            new ResourceInfo(PREFAB_NAME.FIELD_FIELD,"DenQ_SweeperPrefab/FieldObejctRootPrefab/Field")},

            {PREFAB_NAME.FIELD_BLOCK,
            new ResourceInfo(PREFAB_NAME.FIELD_BLOCK,"DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBlock")},
            {PREFAB_NAME.FIELD_BOMB,
            new ResourceInfo(PREFAB_NAME.FIELD_BOMB,"DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBomb")},
    };
    void Awake()
    {
        SetInstance(this);
        StartCoroutine(MakeResourcesPrefab());
    }
    public IEnumerator MakeResourcesPrefab()
    {
        LoadCompleted = false;
        count = PrefabHolders.Count;
        List<PREFAB_NAME> fialedNames = new List<PREFAB_NAME>();
        foreach (PREFAB_NAME name in PrefabHolders.Keys)
        {
            StartCoroutine(PrefabHolders[name].LoadPrefab(() =>
            {
                count--;
            }, () =>
            {
                count--;
                fialedNames.Add(name);
            }));
            yield return null;
        }

        while (count > 0)
        {
            yield return null;
        }

        if (fialedNames.Count > 0)
        {
            Debug.Log(string.Format("{0:d} objects Load Fialed", fialedNames.Count));
        }
        else
        {
            LoadCompleted = true;
        }
        yield return null;
    }
    public bool IsLoadFinished()
    {
        return LoadCompleted;
    }
    //TODO ロードをコルーチンにし、削除処理を行う
    public GameObject CreateInstance(PREFAB_NAME name, GameObject parent, bool DeletePrefab)
    {
        ResourceInfo _prefabInfo;
        if (PrefabHolders.TryGetValue(name, out _prefabInfo))
        {
            return _prefabInfo.CreateInstance(parent);
        }
        else
        {
            return null;
        }
    }
    public GameObject CreateInstance(PREFAB_NAME name, PREFAB_NAME parentName, bool DeletePrefab)
    {
        ResourceInfo _prefabInfo;
        if (PrefabHolders.TryGetValue(name, out _prefabInfo))
        {
            if(BattleScene.Get() != null)
            return _prefabInfo.CreateInstance(BattleScene.Get().GetWorldParent(parentName));
        }
        else
        {
            return null;
        }
        return null;
    }
    public Dictionary<PREFAB_NAME, bool> GetExistList()
    {
        Dictionary<PREFAB_NAME, bool> tempList = new Dictionary<PREFAB_NAME, bool>();
        foreach (PREFAB_NAME name in PrefabHolders.Keys)
        {
            bool exist = false;
            if (PrefabHolders[name].ExistGameObject())
            {
                exist = true;
            }
            tempList.Add(name, exist);
        }
        return tempList;
    }
    //=================Debug処理=================//
}

//プレハブの実態を保持するクラス
//たくさん使うプレハブをここに登録すると、事前ロードするので、
//使うときに直接インスタンスを生成できる
public class ResourceInfo : Transform
{
    public PREFAB_NAME _name;
    public string _prefabPath = null;
    public GameObject _prefabObject = null;
    public ResourceInfo()
    { }
    public ResourceInfo(PREFAB_NAME name, string path)
    {
        _name = name;
        _prefabPath = path;
    }

    //Unity内はFrameWork 3.5に相当するものを使っているので、マルチタスクのように見せかけて、
    //コルーチンを実行しているだけ。
    //一応、ネット上自作Taskの方法はあるけど、中身は結局コルーチンで回しているし、
    //実装するのは大変な作業だし、割愛。
    public IEnumerator LoadPrefab(Action onComplete, Action onError)
    {
        if (_prefabPath == null)
        {
            Debug.Log("There is no path in" + _name.ToString());
            if (onError != null)
            {
                onError();
            }
            yield break;
        }
        //本当は、ここをTaskにしたい(できるわけない)
        if (_prefabObject == null)
            _prefabObject = (GameObject)Resources.Load(_prefabPath);

        if (_prefabObject == null)
        {
            Debug.Log("Failed to load file" + _name.ToString());
            if (onError != null)
                onError();
        }
        else
        {
            if (onComplete != null)
                onComplete();
        }

        yield return null;
    }

    public GameObject CreateInstance()
    {
        //ここでGameObjectのワーク作っちゃうのダメ、
        //空のオブジェクトが無駄に吐き出される
        if (_prefabObject != null)
            return DenQHelper.InstialHelper(_prefabObject);
        return null;
    }
    public GameObject CreateInstance(GameObject parent)
    {
        if (_prefabObject != null)
            return DenQHelper.InstialHelper(_prefabObject, parent);
        return null;
    }
    public bool ExistGameObject()
    {
        return _prefabObject != null;
    }
}