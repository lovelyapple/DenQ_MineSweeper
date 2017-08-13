using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class BombTableHelper {
	public static BombData GetBombDataByID(ulong id)
	{
		var list = BombTableImporter.GetBombData();
		return list.ContainsKey(id)? list[id] : null;
	}
}

