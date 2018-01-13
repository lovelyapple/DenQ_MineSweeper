using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class SkillAction_SuisideExplosion :SkillActionBase
{
    public override bool SetupData(uint code,FieldObjectData selfData)
    {
        if(!base.SetupData(code,selfData))
        {
            return false;
        }

        onStart = ()=>
        {
            FieldEffectManager.GetInstance().CreateFieldEffect(effectData,self.gameObject.transform.position);
        };

        onFininshed = ()=>
        {
            self.KillSelf();
        };

        return true;
    }
}
