using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class SkillBase
{
    public SkillData skillData;
    public EffectData effectData;
    FieldObjectData self;
    public void SetUpSkillData(uint skillCode, FieldObjectData selfData)
    {
        self = selfData;
        skillData = SkillTableHelper.GetSkillDataById(skillCode);

        if (skillData.effectCode != 0)
        {
            effectData = EffectTableHelper.GetEffectDataById(skillData.effectCode);
        }
    }
    public virtual void FireSkill()
    {
        if (effectData != null)
        {
            FieldEffectManager.GetInstance().CreateFieldEffect(effectData, self.gameObject.transform.position);
        }
    }
}
