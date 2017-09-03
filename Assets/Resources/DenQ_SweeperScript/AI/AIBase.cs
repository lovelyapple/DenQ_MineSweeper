using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQ.Action;

public enum AIState
{
    standby,
    movingToArea,
    moveingToTarget,
    attacking,
    dying,
    dead,
    none,

}
public class AIBase : MonoBehaviour
{
    public ObjectBaseData selfData = null;
    public AIState aiState = AIState.standby;
    [SerializeField] protected bool isStopedForce = false;
    void OnEnable()
    {
        if (selfData == null)
        {
			//現段階、ObjDataはコード内で追加禁止、Prefabで追加のみ
            selfData = gameObject.GetComponent<ObjectBaseData>();
            if (selfData == null)
            {
                DenQLogger.GError("could not find objectData");
                isStopedForce = true;
                return;
            }
            if (selfData.actionCtrl == null)
            {
                selfData.InitActionCtrl();
            }
        }
    }
	public virtual void InitializeAI(){}
    public virtual void UpdateAI() { }
    public virtual void PlayAction(ACTIONTYPE type)
    {
        if (selfData == null) { return; }
        selfData.actionCtrl.PlayAction(type);
    }

	public void SearchEnemyAsRange()
	{
		
	}
}