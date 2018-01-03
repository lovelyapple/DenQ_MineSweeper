using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillActionHelper
{
    public static SkillActionBase GetSkillAction(uint code)
    {
        switch (code)
        {
            case 10000000:
                return new SkillAction_Standby();
            case 10000001:
                return new SkillAction_SuisideExplosion();
            case 99999999:
                return new SkillAction_Trigger();
            default: return null;
        }
    }
}
