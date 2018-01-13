using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class SkillInfo
{
    public SkillData skillData;
    FieldObjectData self;
    List<SkillActionBase> skillActionList;
    public SkillInfo(uint skillCode, FieldObjectData selfData)
    {
        SetUpSkillData(skillCode, self);
    }
    public void SetUpSkillData(uint skillCode, FieldObjectData selfData)
    {
        self = selfData;
        skillData = SkillTableHelper.GetSkillDataById(skillCode);
        skillActionList = new List<SkillActionBase>();
        var skillActionGroupList = SkillActionGroupTableHelper.GetSkillActionGroupTableData(skillCode);

        foreach (var skillActionGroup in skillActionGroupList)
        {
            var skillAction = SkillActionHelper.GetSkillAction(skillActionGroup.skillActionCode);

            if (skillAction != null)
            {
                skillAction.SetupData(skillActionGroup.skillActionCode, self);
                skillActionList.Add(skillAction);
            }
        }
    }
    public virtual IEnumerator FireSkill(Action OnFinished = null)
    {
        foreach (var skAction in skillActionList)
        {
            yield return skAction.RunSkllAction();
        }

        if(OnFinished != null)
        {
            OnFinished();
        }
    }
}
