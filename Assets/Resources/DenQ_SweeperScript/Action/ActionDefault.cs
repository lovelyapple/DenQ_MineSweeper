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
        public override ACTIONTYPE actionType() { return ACTIONTYPE.standby; }
    }

    public class Action_Default_Move : ActionBase
    {
        public override ACTIONTYPE actionType() { return ACTIONTYPE.moving; }
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
        public override ACTIONTYPE actionType() { return ACTIONTYPE.dying; }
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
        public override ACTIONTYPE actionType() { return ACTIONTYPE.dead; }
        public override void UpdateAction()
        {
            if (actingTime < actionTime)
            {
                actingTime += Time.deltaTime;
            }
            else
            {
                selfData.DestroyThis();
            }
        }
    }


    public class Action_Default_Attack : ActionBase
    {
        public override ACTIONTYPE actionType() { return ACTIONTYPE.attacking; }
		public override void UpdateAction()
		{
			if(!selfData.actionCtrl.targetCtrl.ExistTarget())
			{
				selfData.actionCtrl.PlayAction(ACTIONTYPE.standby);
				return;
			}
			if(selfData.time_Attack <= 0)
			{
				selfData.time_Attack = 0;
				if(selfData.actionCtrl.targetCtrl.IsTargetInFireRange())
				{
					//TODO attackManager
					selfData.ResetAttackTime();
				}
			}else
			{
				selfData.time_Attack -= Time.deltaTime;
			}
		}
    }
}