using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public enum ActionType
{
    attack,
}
public class ActionBase
{
    public virtual uint actionType { get { return 0; } }
    public virtual string actionName { get { return string.Empty; } }
    protected FieldObjectData self;

    public ActionBase(FieldObjectData selfData)
    {
        self = selfData;
    }
    public virtual void PlayAction() { }
    public override string ToString()
    {
		return "Action Class : " + actionName;
    }
}
