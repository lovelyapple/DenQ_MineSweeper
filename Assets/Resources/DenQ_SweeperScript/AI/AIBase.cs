using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQ.Action;

public enum AIState
{
	standby,
	movingToArea,
	attacking,
	dying,
	dead,
}
public class AIBase : MonoBehaviour {
	public ObjectBaseData selfData = null;
	[SerializeField] protected bool isStopedForce = false;
	void OnEnable()
	{
		if(selfData == null)
		{
			selfData = gameObject.GetComponent<ObjectBaseData>();
			if(selfData == null)
			{
				DenQLogger.GError("could not find objectData");
				isStopedForce = true;
				return;
			}
			if(selfData.actionCtrl == null)
			{
				selfData.InitActionCtrl();
			}
		}
	}
	public virtual void UpdateAI() { }

}
