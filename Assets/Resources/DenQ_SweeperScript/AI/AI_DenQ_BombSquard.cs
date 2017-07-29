using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQ.Action;

namespace DenQ.AI
{
    public class AI_DenQ_BombSquard : AIBase
    {
        public void InitializeAIDefault()
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
            selfData.actionCtrl.RigisterAction(ACTIONTYPE.dead, new Action_Default_Dead());
            selfData.actionCtrl.InitAllActions();

        }
        public virtual void PlayAction(ACTIONTYPE type)
        {
            if (selfData == null) { return; }
            selfData.actionCtrl.PlayAction(type);
        }
        //TODO そろそろアップデータ書こうか
    }
}