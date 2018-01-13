using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public static class ActionHelper
{
    public static ActionBase GetAction(uint actionCode, FieldObjectData objData,ActionController actionCtrl)
    {
		//codeまだない
        switch (actionCode)
        {
            case 10000000:
                return new Action_NormalAttack(objData,actionCtrl);
            default:
                return null;
        }
    }
}
