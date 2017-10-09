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
        var typeToken = itemCode / 10000;
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
}
