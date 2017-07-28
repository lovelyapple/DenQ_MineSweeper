using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;

public enum ACTIONTYPE
{
    none = 0,
    //defalut type
    standby = 1,
    moving = 2,
    attacking = 3,
    dying = 99,
    dead = 100,
}

public class ActionBase
{
    protected virtual ACTIONTYPE GetActionType() { return ACTIONTYPE.none; }
    public ACTIONTYPE actionType { get { return GetActionType(); } }
    public ObjectBaseData selfData = null;
    public float actionTime = -1.0f;    //再生時間指定
    public float actingTime = 0.0f;     //実際の再生経過時間
    public virtual void UpdateAction() { }
}
