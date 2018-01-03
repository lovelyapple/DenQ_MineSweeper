using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActionType
{
    standBy,
    attack,
    dying,
    dead,
}
public class ActionController : MonoBehaviour
{
    ///所持しているアクション
    public Dictionary<uint, ActionBase> actionDict;
    ///所持しているスキル
    public Dictionary<uint, SkillInfo> skillInfosDict;
    ///現在実行しているアクション
    public ActionBase actionCurrently { get; private set; }
    public string actionNameCurrently;
    Coroutine skillCorutine;
    public void Update()
    {
        actionNameCurrently = actionCurrently == null ? " Doing Noth " : actionCurrently.ToString();

    }
    public void RigistAction(uint actionType, ActionBase actionInfo)
    {
        if (actionDict == null)
        {
            actionDict = new Dictionary<uint, ActionBase>();
        }

        if (!actionDict.ContainsKey(actionType))
        {
            actionDict.Add(actionType, actionInfo);
        }
        else
        {
            if (actionDict[actionType] != null)
            {
                Logger.GWarn("すでに存在しているアクションをうわ描きしようとしている ActionType " + ((ActionType)actionType).ToString());
            }

            actionDict[actionType] = actionInfo;
        }
    }
    ///新しいアクションの再生リクエスト
    public void RequestAction(uint actionType, ActionBase actionInfo = null)
    {
        if (!ChangeAction(actionType, actionInfo))
        {
            return;
        }
        //TODOnew
        //どこかで、ループをかけないと行けない
    }
    ///新しいアクションを再生可能かチェック
    public bool ChangeAction(uint actionType, ActionBase actionInfo = null)
    {
        if (!actionDict.ContainsKey(actionType))
        {
            if (actionInfo == null)
            {
                Logger.GWarn("doesnt action actionTYpe " + ((ActionType)actionType).ToString());
                return false;
            }
            else
            {
                RigistAction(actionType, actionInfo);
            }
        }

        if (actionCurrently == null)
        {
            actionCurrently = actionDict[actionType];
            return true;
        }
        else
        {
            if (actionCurrently.actionType >= actionType)
            {
                return false;
            }
        }

        return false;
    }
    ///次のはどうすべきかを判断するもの
    ///それぞれのAIに各自で判断する
    public virtual void CheckNextAction(ActionBase actionInfo)
    {
        //今何もしない
    }
    public uint ReturnActionLevel(uint actionType)
    {
        switch ((ActionType)actionType)
        {
            case ActionType.standBy: return 0;
            case ActionType.attack: return 1;
            case ActionType.dying: return 100;
            case ActionType.dead: return 101;
            default:
                return 0;
        }
    }
    public uint ReturnActionLevel(ActionBase actionInfo)
    {
        var actionType = actionInfo.actionType;

        return ReturnActionLevel(actionType);
    }
    public SkillInfo GetNormalAttackSkillInfo()
    {
        if (skillInfosDict.ContainsKey((uint)ActionType.attack))
        {
            return skillInfosDict[(uint)ActionType.attack];
        }
        return null;
    }
    public void UseSkill(SkillInfo info)
    {
        skillCorutine = StartCoroutine(info.FireSkill(()=>
        {
            skillCorutine = null;
        }));
    }
    //TODO FieldObjectStat と　AI　の実装
}
