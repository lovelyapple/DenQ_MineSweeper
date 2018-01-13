using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
public class SkillAction_Standby : SkillActionBase
{
    float standByTimeMax;
    float standByTime;
    protected override bool isActing { get { return standByTime <= 0f; } }
    public override bool SetupData(uint code, FieldObjectData selfData)
    {
        if (!base.SetupData(code, selfData))
        {
            return false;
        }

        standByTimeMax = (float)skillActionData.param01;

        onStart = ()=>
        {
            standByTime = standByTimeMax;
        };

        onRuning = ()=>
        {
            standByTime -= Time.deltaTime;
        };

        onFininshed = ()=>
        {
            standByTime = 0f;
        };

        return true;
    }
}
