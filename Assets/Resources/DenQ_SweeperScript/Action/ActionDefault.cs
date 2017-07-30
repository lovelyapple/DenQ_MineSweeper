using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*デフォルトアクションの内容
 *
 *
 */
namespace DenQ.Action
{
    public class Action_Default_Standby : ActionBase
    {
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.standby; }
    }

    public class Action_Default_Move : ActionBase
    {
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.moving; }
        public override void UpdateAction()
        {
            selfData.actionCtrl.moveCtrl.UpdateMoveController();
            if (selfData.actionCtrl.targetCtrl.IsTouchedTarget())
            {
                selfData.actionCtrl.PlayAction(ACTIONTYPE.standby);
            }
        }
    }
    public class Action_Default_Dying : ActionBase
    {
        void Awake()
        {
            actionTime = 10.0f;
        }
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.dying; }
        public override void UpdateAction()
        {
            if (actingTime < actionTime)
            {
                actingTime += Time.deltaTime;
            }
            else
            {
                selfData.actionCtrl.PlayAction(ACTIONTYPE.dead);
            }
        }
    }
    public class Action_Default_Dead : ActionBase
    {
        void Awake()
        {
            actionTime = 2.0f;
        }
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.dead; }
        public override void UpdateAction()
        {
            if (actingTime < actionTime)
            {
                actingTime += Time.deltaTime;
            }
            else
            {
                GameObjectsManager.GetInstance().RemoveObjectById(selfData.objectId);
                selfData.DestroyThis();
            }
        }
    }


    public class Action_Default_Attack : ActionBase
    {
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.attacking; }
        public SKILL_KIND fireRequestSkill = SKILL_KIND.normalAttack01;
        public override void UpdateAction()
        {
            //実際にモーションがある場合、actionTimeも設定しないといけないけど、今ないので、いいっか
            if (!selfData.actionCtrl.targetCtrl.ExistTarget())
            {
                selfData.actionCtrl.PlayAction(ACTIONTYPE.standby);
                return;
            }
            if (!selfData.actionCtrl.targetCtrl.IsTargetInFireRange()) { return; }
            selfData.actionCtrl.skillCtrl.FireSkill(fireRequestSkill);
        }
    }
    public class Action_Default_AttackMove : ActionBase
    {
        protected override ACTIONTYPE GetActionType() { return ACTIONTYPE.attackMoving; }

        public override void UpdateAction()
        {
            selfData.actionCtrl.moveCtrl.UpdateMoveController();
            if (selfData.actionCtrl.targetCtrl.IsTargetInFireRange())
            {
                selfData.actionCtrl.PlayAction(ACTIONTYPE.standby);
            }
        }
    }

}