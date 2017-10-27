using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class BombTableHelper {
	public static BombData GetBombDataByID(ulong id)
	{
		var list = BombTableImporter.GetBombData();
		return list.ContainsKey(id)? list[id] : null;
	}
	public static List<BombData> GetBombAllDatas()
	{
		var dict = BombTableImporter.GetBombData();
		return dict.Select(t => t.Value).ToList();
	}
}

