using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;

public static class DenQDataBaseHelper
{
	///itemDataの取得、全てのアイテムデータはここからとるべき
    public static uint GetFieldItemToken(ulong code)
    {
        return (uint)(code % 10000000 / 1000);
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
