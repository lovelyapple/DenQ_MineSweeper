using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class FieldBombTableHelper {
	public static FieldBombData GetBombDataByID(ulong id)
	{
		var list = FieldBombTableImporter.GetBombData();
		return list.ContainsKey(id)? list[id] : null;
	}
	public static List<FieldBombData> GetBombAllDatas()
	{
		var dict = FieldBombTableImporter.GetBombData();
		return dict.Select(t => t.Value).ToList();
	}
}

