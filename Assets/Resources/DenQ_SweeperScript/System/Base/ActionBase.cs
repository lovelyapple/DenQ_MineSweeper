using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;

public class ActionBase
{
    public virtual uint actionType { get { return 0; } }
    public virtual string actionName { get { return string.Empty; } }
    protected FieldObjectData self;
    protected ActionController actionController;
    ///一回のみ実行アクションが終了後のコールバック
    public event Action<ActionBase> actionFinishedCallBack = null; 

    public ActionBase(FieldObjectData selfData,ActionController actionController)
    {
        self = selfData;
        this.actionController = actionController;
    }
    public virtual IEnumerator PlayAction() { yield return null; }
    public override string ToString()
    {
        return "Action Class : " + actionName;
    }
}
