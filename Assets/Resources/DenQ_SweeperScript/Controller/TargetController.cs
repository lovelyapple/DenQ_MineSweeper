using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
public class TargetController : MonoBehaviour
{
    public ObjectBaseData selfData = null;
    public ObjectBaseData targetData = null;
    [SerializeField] private float distanceBody = 0.0f;
    public void OnEnable()
    {
        if (selfData == null)
        {
            selfData = GetComponent<ObjectBaseData>();
        }
    }
    public void SetTarget(ObjectBaseData _targetData)
    {
        targetData = _targetData;
        distanceBody = targetData.out_bodySize + selfData.out_bodySize;
    }
    void Update()
    {
        if (targetData == null) { return; }
        if (selfData.IsDead()) { return; }
        if (targetData.IsDead())
        {
            targetData = null;
            distanceBody = 0.0f;
        }
    }
    public ObjectBaseData GetTarget()
    {
        return targetData;
    }
    public float? GetTargetRange()
    {
        if (targetData == null) { return null; }
        var dist = targetData.transform.position - this.transform.position;
        return dist.magnitude;
    }
    public bool IsTouchedTarget()
    {
        return GetTargetRange() == null ? false : GetTargetRange().Value <= distanceBody;
    }

}
