using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
public class Action_SelfExplosion : ActionBase
{

    public override uint actionType { get { return (uint)ActionType.attack; } }
    public override string actionName { get { return "Action_SelfExplosion"; } }
    public Skill_Explosion skillInfo;
    public Action_SelfExplosion(FieldObjectData selfData) : base(selfData)
    {
        this.self = selfData;
    }
    public void SetUpSkill(uint skillCode)
    {
        skillInfo = new Skill_Explosion();
        skillInfo.SetUpSkillData(skillCode, self);
    }
    public override void PlayAction()
    {
        skillInfo.FireSkill();
        self.KillSelf();
    }
}
