using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class SkillActionTableHelper {
	public static SkillActionData GetSkillActionDataById(uint id)
	{
		var db = DenQDataBase.skillActionTable;
		return db.ContainsKey(id)? db[id] : null;
	}
}

