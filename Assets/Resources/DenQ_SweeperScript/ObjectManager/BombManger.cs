using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;

public class BombManger : MangerBase<BombManger>
{
    //TODO Listの使用を検討
//    private static List<FieldBomb> BombList = null;
    void Awake()
    {
        SetInstance(this);
    }
    public void CreateBomb(FieldBlock blockData,int fieldCode)
    {
        GameObject bombTemp = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_BOMB,PREFAB_NAME.ITEM_ROOT,false);

        AI_FieldBomb_Base bombData = bombTemp.GetComponent<AI_FieldBomb_Base>();
        bombData.InitializeFieldBomb(blockData.fieldPos.posX, blockData.fieldPos.posZ, FIELD_ITEM.BOMB_DELAY,fieldCode);
    }
}
