using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DenQData;
public static class FieldBlockTableHelper {
	public static FieldBlockData GetFieldBLockDataByID(ulong id)
	{
		var list = DenQDataBase.fieldBlockTable;
		return list.ContainsKey(id)? list[id] : null;
	}
	public static List<FieldBlockData> GetBombAllDatas()
	{
		var dict = DenQDataBase.fieldBlockTable;
		return dict.Select(t => t.Value).ToList();
	}
}

