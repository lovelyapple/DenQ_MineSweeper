using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public class ItemBase
{
    public ulong itemCode;		///ItemTableの中にもつ一意性のコード
	public uint itemType;		///いらないかも
}

public static class DenQDataBaseHelper
{
	///itemDataの取得、全てのアイテムデータはここからとるべき
    public static ItemBase GetItemData(ulong itemCode)
    {
        var typeToken = GetFieldItemToken(itemCode);
        switch ((FIELD_ITEM_TOKEN)typeToken)
        {
            case FIELD_ITEM_TOKEN.FIELD_ITEM_BLOCK:
                return null;
            case FIELD_ITEM_TOKEN.FIELD_ITEM_BOMB:
				return BombTableHelper.GetBombDataByID(itemCode);
				default:
				return null;
        }
    }
    public static uint GetFieldItemToken(ulong code)
    {
        return (uint)(code / 10000);
    }
    public static bool IsBomb(ulong code)
    {
        return GetFieldItemToken(code) == (uint)FIELD_ITEM_TOKEN.FIELD_ITEM_BOMB;
    }
    public static bool IsBlock(ulong code)
    {
        return GetFieldItemToken(code) == (uint)FIELD_ITEM_TOKEN.FIELD_ITEM_BLOCK;
    }
}
