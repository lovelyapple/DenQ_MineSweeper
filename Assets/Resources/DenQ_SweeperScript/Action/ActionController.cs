using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
public class ActionController
{
    public ObjectBaseData selfData = null;

    private Dictionary<ACTIONTYPE, ActionBase> actionList = new Dictionary<ACTIONTYPE, ActionBase>();
	public ActionBase currentlyAction = null;
	public ACTIONTYPE backBuffState = ACTIONTYPE.none;

	public MoveController moveCtrl = null;
	public TargetController targetCtrl = null;
    void Awake()
    {
        actionList.Clear();
    }
	void Update()
	{
		if(currentlyAction != null)
		{
			currentlyAction.UpdateAction();
		}
	}
    public void RisiterAction(ACTIONTYPE type, ActionBase actionBase)
    {
        if (actionList.ContainsKey(type)) { return; }
		actionList.Add(type,actionBase);
    }
	public void InitAllActions()
	{
		foreach(var type in actionList.Keys)
		{
			actionList[type].selfData = selfData;
		}
	}
}
