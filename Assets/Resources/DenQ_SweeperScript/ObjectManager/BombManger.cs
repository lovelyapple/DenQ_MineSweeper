using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;

public class BombManger : MangerBase<BombManger>
{
    private static List<FieldBomb> BombList = null;
    static void Awake()
    {
        BombList = new List<FieldBomb>();
    }
    public static void CreateBomb(BOMB_TYPE bomtType, FieldPos fPos)
    {
        GameObject bombTemp = ResourcesHelper.CreatePrefabinstance((uint)PREFABU_NAME.FiedlBomb,
        false, BattleScene.GetInstance().BombRootObj);
        FieldBomb bombData = bombTemp.GetComponent<FieldBomb>();
        bombData.InitializeFieldBomb(fPos.posX, fPos.posZ, BOMB_TYPE.DELAY);
    }
}
