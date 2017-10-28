using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
public class DamageController
{
    public FieldObjectData self { get; private set; }
    public DamageController(FieldObjectData objData)
    {
        self = objData;
    }
	public void TakeDamage(SkillData skillData)
	{
		if(self.isDead)
		{
			return;
		}

		self.recHp -= skillData.baseDamage;
	}
}
