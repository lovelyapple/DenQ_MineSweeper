using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQ.Action;

namespace DenQ.AI
{
    public class AI_DenQ_BombSquard : AIBase
    {
        public override void InitializeAI()
        {
            if (isStopedForce) { return; }
            selfData.InitActionCtrl();
            if (selfData.actionCtrl == null)
            {
                DenQLogger.SErrorId(selfData.objectId, "AI 初期化失敗,ActionCtrlがない");
                return;
            }
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.standby, new Action_Default_Standby());
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.moving, new Action_Default_Move());
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.dying, new Action_Default_Dying());
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.attacking, new Action_Default_Attack());
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.attackMoving, new Action_Default_AttackMove());
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.dead, new Action_Default_Dead());
            selfData.actionCtrl.InitAllActions();

        }

        //TODO そろそろアップデータ書こうか
        public override void UpdateAI()
        {
            switch (aiState)
            {
                case AIState.standby:
                    break;
                case AIState.movingToArea:
                    break;
                case AIState.moveingToTarget:
                    break;
                case AIState.attacking:
                    if (!selfData.actionCtrl.targetCtrl.IsTargetInFireRange())
                    {
                        selfData.actionCtrl.PlayAction(ACTIONTYPE.attackMoving);
                    }else
                    {
                        selfData.actionCtrl.PlayAction(ACTIONTYPE.attacking);
                    }
                    break;
                case AIState.dying:
                    break;
                case AIState.dead:
                    break;
            }
        }
    }
}