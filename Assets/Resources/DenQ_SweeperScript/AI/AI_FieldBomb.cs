using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class AI_FieldBomb : AIBase
{
    public override void InitialzieAI()
    {
        if (actionController == null || self == null)
        {
            return;
        }

        //とりあえずハードコーディングしちゃう
        actionController.actionDict = new Dictionary<uint, ActionBase>
        {
            {(uint)ActionType.attack, new Action_NormalAttack(self,actionController)}
        };
        actionController.skillInfosDict = new Dictionary<uint, SkillInfo>
        {
            {(uint)ActionType.attack, new SkillInfo(10000000,self)}
        };

        AIMode = (uint)DenQAIModes.StandBy;
    }

}
