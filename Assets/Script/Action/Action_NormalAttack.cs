using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public class Action_NormalAttack : ActionBase
{
    enum Attack_State
    {
        accessing,
        firing,
    }
    public Action_NormalAttack(FieldObjectData selfdata, ActionController actionCtrl)
    : base(selfdata, actionCtrl)
    {

    }
    SkillInfo skill;
    uint attackState;
    public override uint actionType { get { return (uint)ActionType.attack; } }
    public override string actionName { get { return "Action_NormalAttack"; } }
    public override IEnumerator PlayAction()
    {
        switch ((Attack_State)attackState)
        {
            case Attack_State.accessing:
                Accessing();
                break;
            case Attack_State.firing:

                break;
        }
        yield return null;

        if (actionController != null)
        {
            actionController.CheckNextAction(this);
        }
    }

    void Accessing()
    {
        if (self.TargetController.CanFireSkill())
        {
            skill = actionController.GetNormalAttackSkillInfo();

            if (skill != null)
            {
                attackState = (uint)Attack_State.firing;
                actionController.UseSkill(skill);
            }
        }
    }
}
