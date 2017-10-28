using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class SkillTableHelper {
	public static SkillData GetSkillDataById(ulong id)
	{
		var db = DenQDataBase.SkillTable;
		return db.ContainsKey(id)? db[id] : null;
	}
}

