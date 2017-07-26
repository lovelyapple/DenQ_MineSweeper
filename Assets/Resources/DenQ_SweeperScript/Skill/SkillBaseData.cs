using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
/*スキルの基本、通常攻撃を含め、全ての移動以外の動作がスキルとなる
 * 
 *
 */
public enum SKILL_TYPE
{
    fastOnce,               //具体的な動作
}
public enum SKILL_KIND
{
    normalAttack01,         //分類
}
public class SkillBaseData
{
    public SkillBaseData() { }
    public SkillBaseData(SKILL_KIND kind, SKILL_TYPE type, int damage, float coolTime, uint id = 0)
    {
        skillId = id;
        skillDamage = damage;
        skillType = type;
        skillKind = kind;
        skillCoolTime = coolTime;
    }
    public uint skillId;
    public SKILL_KIND skillKind = SKILL_KIND.normalAttack01;
    public SKILL_TYPE skillType = SKILL_TYPE.fastOnce;
    public float skillDamage = 0.0f;
    public float skillCoolTime = 60.0f;
    public float activeTime = 10.0f;
    private float _coolTime = 60.0f;

    public void UpdateSkill()
    {
        if (activeTime > 0)
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

                //TODO targetController に Damageメソッドを追加
                //targetData.actionCtrl.targetCtrl
                break;
        }
    }
}
