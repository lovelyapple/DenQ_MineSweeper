using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQ.Action;

namespace DenQ.AI
{
    public class AI_DenQ_BombSquard : AIBase
    {
        public void InitializeAI()
        {
            if (isStopedForce) { return; }
			selfData.actionCtrl.RisiterAction(ACTIONTYPE.standby,new Action_Default_Standby());
			selfData.actionCtrl.RisiterAction(ACTIONTYPE.moving,new Action_Default_Move());
			selfData.actionCtrl.RisiterAction(ACTIONTYPE.moving,new Action_Default_Move());
			selfData.actionCtrl.RisiterAction(ACTIONTYPE.dying,new Action_Default_Dying());
			selfData.actionCtrl.RisiterAction(ACTIONTYPE.dead,new Action_Default_Dead());
			selfData.actionCtrl.InitAllActions();
        }

    }
}