using System.Collections;
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
    public FieldManager FieldManger = null;
    private static BattleScene _instance = null;
    public enum BATTLE_STATE
    {
        OVERHEAD,
        INIT,
        UPDATE,
        ERRORED,
    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _instance = this;
    }
    public static BattleScene Get()
    {
        return _instance;
    }
    private BATTLE_STATE BattleState = BATTLE_STATE.OVERHEAD;
    // Use this for initialization
    public static BattleScene GetInstance()
    {
        if (_instance == null)
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
            case BATTLE_STATE.OVERHEAD:
                if (ResourcesManager.GetInstance().IsLoadFinished())
                {
                    BattleState = BATTLE_STATE.INIT;
                }
                break;
            case BATTLE_STATE.INIT:
                InitilizeBattleScene();
                BattleState = BATTLE_STATE.UPDATE;
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
        if (BattleSceneObj == null)
        {
            Debug.Log("could not load Battle scene Gameobject");
            return false;
        }
        return true;
    }
    void InitilizeBattleScene()
    {

        FieldRootObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_ROOT, this.gameObject, false);
        if (FieldRootObj == null)
        {
            BattleState = BATTLE_STATE.ERRORED;
            return;
        }
        ItemRootObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.ITEM_ROOT, FieldRootObj, false);
        if (ItemRootObj == null)
        {
            BattleState = BATTLE_STATE.ERRORED;
            return;
        }
        FieldManagerObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_MGR, FieldRootObj, false);
        if (FieldManagerObj == null)
        {
            BattleState = BATTLE_STATE.ERRORED;
            return;
        }
        FieldManger = FieldManagerObj.GetComponent<FieldManager>();
        if (FieldManger == null)
        {
            BattleState = BATTLE_STATE.ERRORED;
            return;
        }

        GameObject fieldObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_FIELD, FieldManagerObj, false);
        if (fieldObj == null)
        {
            BattleState = BATTLE_STATE.ERRORED;
            return;
        }else
        {
            FieldManger.FieldData = fieldObj.GetComponent<Field>();
        }

    }
    public GameObject GetWorldParent(PREFAB_NAME name)
    {
        switch(name)
        {
            case PREFAB_NAME.FIELD_ROOT:
            return FieldRootObj;
            case PREFAB_NAME.ITEM_ROOT:
            return ItemRootObj;
            default :return null;
        }
    }

}
