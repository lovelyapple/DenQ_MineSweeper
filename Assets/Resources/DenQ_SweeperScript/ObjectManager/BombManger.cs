using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;

public class BombManger : MangerBase<BombManger>
{
    private static List<FieldBomb> BombList = null;
    void Awake()
    {
        SetInstance(this);
    }
    public void CreateBomb(FieldBlock blockData)
    {
        GameObject bombTemp = ResourcesHelper.CreatePrefabinstance((uint)PREFABU_NAME.FiedlBomb,
        false, BattleScene.GetInstance().BombRootObj,blockData.transform.position);
        FieldBomb bombData = bombTemp.GetComponent<FieldBomb>();
        bombData.InitializeFieldBomb(blockData.Pos.posX, blockData.Pos.posZ, BOMB_TYPE.DELAY);
    }
}
