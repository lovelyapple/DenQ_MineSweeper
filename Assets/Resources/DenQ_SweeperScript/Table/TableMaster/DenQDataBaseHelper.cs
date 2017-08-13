using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
	public ulong itemBaseCode;		///ItemTableの中にもつ一意性のコード
	public ulong itemCode;			///個別のTableの中にもつ詳細コード
	public uint itemType;
}
namespace DenQData
{
    public static class DenQDataBaseHelper
    {
		public static ItemBase GetItemItemData(ulong _itemBaseCode)
		{
			uint itemCategory = (uint)(_itemBaseCode/10000);
			uint itemCode = (uint)(_itemBaseCode % 10000);
			switch(itemCategory)
			{
				case (uint)ObjectType.Field_Bomb:
			         return BombTableHelper.GetBombDataByID(itemCode);
			}
			return null;
		}
    }
}