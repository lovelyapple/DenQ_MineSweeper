using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
public class ActionController : MonoBehaviour
{
    public ObjectBaseData selfData = null;

    //Actions
    private Dictionary<ACTIONTYPE, ActionBase> actionList = new Dictionary<ACTIONTYPE, ActionBase>();
    public ActionBase currentlyAction = null;
    public ACTIONTYPE backBuffState = ACTIONTYPE.none;

    //Controllers
    public MoveController moveCtrl = null;
    public TargetController targetCtrl = null;
    public SkillController skillCtrl = null;

    void Awake()
    {
        actionList.Clear();
    }
    void Update()
    {
        if (currentlyAction != null)
        {
            currentlyAction.UpdateAction();
        }
    }
    public void InitializeCtrl(ObjectBaseData selfObjData)
    {
        selfData = selfObjData;
        /*  コントローラーの取得と初期化
         *  まとめでここで管理すると、objData軽くなる
         *  各Ctrlがたくさん共通点があれば、汎化しても良いが、現時点まあいいか
         */
        moveCtrl = gameObject.GetComponent<MoveController>();
        if (moveCtrl == null)
        {
            moveCtrl = gameObject.AddComponent<MoveController>();
        }
        moveCtrl.InitializeCtrl(selfData);
        targetCtrl = gameObject.GetComponent<TargetController>();
        if (targetCtrl == null)
        {
            targetCtrl = gameObject.AddComponent<TargetController>();
        }
        targetCtrl.InitializeCtrl(selfData);
        skillCtrl = gameObject.GetComponent<SkillController>();
        if (skillCtrl == null)
        {
            skillCtrl = gameObject.AddComponent<SkillController>();
        }
        skillCtrl.InitializeCtrl(selfData);
    }
    //多分使わないですが、一応書いとく
    public void RigisterActions(List<ActionBase> actions, Action<List<ActionBase>> onError)
    {
        var outList = new List<ActionBase>();
        foreach (var act in actions)
        {
            if (!RigisterAction(act.actionType, act))
            {
                outList.Add(act);
            }
        }
        if (onError != null)
        {
            onError(outList);
        }
    }
    public bool RigisterAction(ACTIONTYPE type, ActionBase actionBase)
    {
        if (actionList.ContainsKey(type))
        {
            DenQLogger.SErrorId(selfData.objectId, string.Format("{0}アクションデータロード失敗" + type.ToString()));
            return false;
        }
        actionList.Add(type, actionBase); return true;
    }
    public void InitAllActions()
    {
        foreach (var type in actionList.Keys)
        {
            actionList[type].selfData = selfData;
        }
    }
    //TODOこれから、複雑になるかな？
    public void PlayAction(ACTIONTYPE type)
    {
        if (actionList.ContainsKey(type))
        {
            currentlyAction = actionList[type];
        }
    }
    public ACTIONTYPE GetCurrentActionType()
    {
        if (currentlyAction != null)
        {
            return currentlyAction.actionType;
        }
        return ACTIONTYPE.none;
    }
}
