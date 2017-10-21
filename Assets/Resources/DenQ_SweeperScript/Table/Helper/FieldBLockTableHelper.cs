using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DenQData;
public static class FieldBlockTableHelper {
	public static FieldBlockData GetFieldBLockDataByID(ulong id)
	{
		var list = DenQDataBase.FieldBlockTable;
		return list.ContainsKey(id)? list[id] : null;
	}
	public static List<FieldBlockData> GetBombAllDatas()
	{
		var dict = DenQDataBase.FieldBlockTable;
		return dict.Select(t => t.Value).ToList();
	}
}

