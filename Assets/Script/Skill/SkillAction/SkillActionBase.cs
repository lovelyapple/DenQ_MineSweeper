using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
public class SkillActionBase
{
    public SkillActionData skillActionData;
    public FieldObjectData self;
    protected EffectData effectData;
    protected Action onStart;
	protected Action onRuning;
	Action _onRuning;
    protected Action onFininshed;
    protected virtual bool isActing { get { return true; } }

    public virtual bool SetupData(uint code, FieldObjectData selfData)
    {
        skillActionData = SkillActionTableHelper.GetSkillActionDataById(code);
        self = selfData;
        if (skillActionData.code != 0)
        {
            effectData = EffectTableHelper.GetEffectDataById(skillActionData.effectCode);

            if (effectData == null)
            {
                return false;
            }
        }

        if (skillActionData == null)
        {
            return false;
        }

        return true;
    }

    public IEnumerator RunSkllAction()
    {
		_onRuning = null;

        if (onStart != null)
        {
            onStart();
        }

		if(onRuning != null)
		{
			_onRuning = onRuning;
			onRuning = null;
		}

        while (isActing)
        {
			_onRuning = null;
            yield return null;
        }

        if (onFininshed != null)
        {
            onFininshed();
        }
    }
}
