using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
public class TargetController : MonoBehaviour
{
    public ObjectBaseData selfData = null;
    public ObjectBaseData targetData = null;
    [SerializeField] private float distanceBody = 0.0f;
    public Action OnCompleteFire = null;
    public void OnEnable()
    {
        /*Initメソッドを統一しよう
        if (selfData == null)
        {
            selfData = GetComponent<ObjectBaseData>();
        }
        */
    }
    public void InitializeCtrl(ObjectBaseData objData)
    {
        if (selfData == null)
        {
            selfData = objData;
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
    public bool IsTargetInFireRange()
    {
        return IsTargetInFireRange(SKILL_KIND.normalAttack01); 
    }
    public bool IsTargetInFireRange(SKILL_KIND kind)
    {
        if (ExistTarget() == false) { return false; }
        var skillData = selfData.actionCtrl.skillCtrl.GetSkillData(kind);
        if (skillData == null) return false;
        var range = GetTargetRange().Value;
        return skillData.attackRange <= range;
    }
    public bool ExistTarget()
    {
        if (targetData == null) { return false; }
        return targetData.IsDead();
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
    public void DamageObject(SkillBaseData skill)
    {
        if (selfData.IsDead()) { return; }
        var baseDamage = skill.skillDamage;
        //TODO  ここでいろんなダメージ方式を描く
        //      とりあえず、普通の攻撃だけ書いとく
        selfData.rec_Hp -= (int)baseDamage;
        OnCompleteFireDefault();
    }
    private void OnCompleteFireDefault()
    {
        if (selfData.rec_Hp <= 0)
        {
            selfData.actionCtrl.PlayAction(ACTIONTYPE.dying);
        }
        if (OnCompleteFire != null)
        {
            OnCompleteFire();
        }
    }
}
