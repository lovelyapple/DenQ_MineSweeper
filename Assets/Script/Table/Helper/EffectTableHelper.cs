using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class EffectTableHelper {
	public static EffectData GetEffectDataById(ulong id)
	{
		var db = DenQDataBase.effectTable;
		EffectData outData;

		if(!db.TryGetValue(id, out outData))
		{
			Logger.SWarn("could not find effect Data " + id);
		}
		return db.ContainsKey(id)? db[id] : null;
	}
}

