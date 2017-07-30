using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
/*スキルの基本、通常攻撃を含め、全ての移動以外の動作がスキルとなる
 * 
 *
 */
public enum SKILL_TYPE      //具体的な動作
{
    fastOnce,               //すぐかつ一回のみ         
}
public enum SKILL_KIND      //分類
{
    normalAttack01,         //MOBの01番目攻撃
}
/*
 *　属性追加？？
 *
 */
public class SkillBaseData
{
    public SkillBaseData() { }
    public SkillBaseData(SKILL_KIND _kind, SKILL_TYPE _type, int _damage, float _coolTime, float _attackRange, uint id = 0)
    {
        skillId = id;
        skillDamage = _damage;
        skillType = _type;
        skillKind = _kind;
        skillCoolTime = _coolTime;
        this.attackRange = _attackRange;
    }
    public uint skillId;
    public SKILL_KIND   skillKind = SKILL_KIND.normalAttack01;
    public SKILL_TYPE   skillType = SKILL_TYPE.fastOnce;
    public List<SkillBaseData>      multiSkills = null;     //複数のスキルの連動かどうか    
    public float        skillDamage = 0.0f;                 //一回付きダメージ
    public float        skillCoolTime = 60.0f;              //CT
    public float        attackRange = 10.0f;                //スキルの攻撃範囲
    //public float        skillOverHeadTime = 0.0f;         //スキルの前処理時間（チャージなど）別になくても、チャージスキルをつくればいい
    public float        activeTime = 10.0f;                 //スキルの動作時間
    private float       _coolTime = 60.0f;                  //CTカウント

    public void UpdateSkill()
    {
        if (_coolTime > 0)
        {
            _coolTime -= Time.deltaTime;
        }
        else
        {
            _coolTime = 0.0f;
        }
    }
    public bool IsSkillReady()
    {
        return _coolTime <= 0;
    }
    public void ResetSkillTime()
    {
        _coolTime = skillCoolTime;
    }
    public float GetActiveTime()
    {
        return activeTime;
    }
    public void Fire(ObjectBaseData selfData, ObjectBaseData targetData)
    {
        if (targetData.IsDead()) { return; }
        switch (skillType)
        {
            case SKILL_TYPE.fastOnce:
                targetData.actionCtrl.targetCtrl.DamageObject(this);
                break;
        }
    }
}
