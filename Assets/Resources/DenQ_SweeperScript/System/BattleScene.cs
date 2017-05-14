﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class BattleScene : MonoBehaviour
{
    //以下の変数は全てPrivateにする予定
    public static GameObject BattleSceneObj = null;
    public GameObject FieldRootObj = null;
    public GameObject BombRootObj = null;
    public GameObject ItemRootObj = null;

    public GameObject FieldManagerObj = null;
    public FieldMgr FieldManger = null;
    public GameObject ResourcesHolderObj = null;
    
    private static BattleScene _instance = null;
    public enum BATTLE_STATE
    {
        INIT,
        UPDATE,
        ERRORED,
    }
    private BATTLE_STATE BattleState = BATTLE_STATE.INIT;
    // Use this for initialization
    public static BattleScene GetInstance()
    {
        if(_instance == null)
        _instance = BattleSceneObj.GetComponent<BattleScene>();
        return _instance;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (BattleState)
        {
            case BATTLE_STATE.INIT:
            BattleSceneObjGet();
                if (InitilizeBattleScene())
                {
                    BattleState = BATTLE_STATE.UPDATE;
                }
                else
                {
                    BattleState = BATTLE_STATE.ERRORED;
                }
                break;
            case BATTLE_STATE.UPDATE:
                break;
            case BATTLE_STATE.ERRORED:
                break;
        }
    }
    //必要のデータをロードする、基本の初期化
    public static bool BattleSceneObjGet()
    {
        BattleSceneObj = GameObject.FindWithTag("BattleScene");
        if(BattleSceneObj == null)
        {
            Debug.Log("could not load Battle scene Gameobject");
            return false;
        }
        return true;
    }
    private bool InitilizeBattleScene()
    {

        //プレハブを読み取る為に最初に実行
        ResourcesHolderObj = ResourcesHelper.CreateResourcesInstance(
            FilePath.GetGodPrefabPath(GOD_PREFAB_NAME.RESOURCES_HOLDER),
            BattleSceneObj);
        if(ResourcesHolderObj == null){return false;}
        ResourcesHolderObj.transform.position = new Vector3(0,0,0);

        //各オブジェクトルートの作成
        FieldRootObj = ResourcesHelper.CreateResourcesInstance(
            FilePath.GetGodPrefabPath(GOD_PREFAB_NAME.FIELD_ROOT),
            BattleSceneObj);
        if (FieldRootObj == null) {return false;}
        FieldRootObj.transform.position = new Vector3(0,0,0);

        BombRootObj = ResourcesHelper.CreateResourcesInstance(
            FilePath.GetGodPrefabPath(GOD_PREFAB_NAME.BOMB_ROOT),
         BattleSceneObj);
        if (BombRootObj == null) {return false;}
        BombRootObj.transform.position = new Vector3(0,0,0); 

        ItemRootObj = ResourcesHelper.CreateResourcesInstance(
            FilePath.GetGodPrefabPath(GOD_PREFAB_NAME.ITEM_ROOT),
            BattleSceneObj);
        if (ItemRootObj == null) {return false;}
        ItemRootObj.transform.position = new Vector3(0,0,0);            

        //各オブジェクトのマネージャー作成
        FieldManagerObj = ResourcesHelper.CreateResourcesInstance(
            FilePath.GetGodPrefabPath(GOD_PREFAB_NAME.FIELD_MGR),
            FieldRootObj);
        if (FieldManagerObj == null) { return false; }
        FieldManagerObj.transform.position = new Vector3(0,0,0);
        FieldManger = FieldManagerObj.GetComponent<FieldMgr>();
        return true;
    }
}
