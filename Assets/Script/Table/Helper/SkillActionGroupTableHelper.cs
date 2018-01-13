using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DenQData;
public static class SkillActionGroupTableHelper 
{
	public static List<SkillActionGroupData> GetSkillActionGroupTableData(uint skillCode)
	{
		var db = DenQDataBase.skillActionGroupTable;
		return db.Where(x => x.skillCode == skillCode).ToList();
	}
}

