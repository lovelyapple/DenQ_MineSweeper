using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
public class SkillController :MonoBehaviour
{
    public Dictionary<SKILL_KIND, SkillBaseData> skillDatas = new Dictionary<SKILL_KIND, SkillBaseData>();
    public float activeTime = 0.0f;
    public ObjectBaseData selfData = null;
    public void InitializeCtrl(ObjectBaseData objData)
    {
        selfData = objData;
        skillDatas.Clear();
        activeTime = 0.0f;
    }
    public void RigisterSkills(SKILL_KIND kind, SkillBaseData data)
    {
        if (skillDatas.ContainsKey(kind)) { return; }
        skillDatas.Add(kind, data);
    }
    public void UpdateSkillCtrl()
    {
        if (skillDatas.Count <= 0) { return; }
        foreach (var data in skillDatas)
        {
            data.Value.UpdateSkill();
        }
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            activeTime = 0;
        }
    }
    public void FireSkill(SKILL_KIND kind)
    {
        if (!CanFireSkill(kind)) { return; }
        var data = skillDatas[kind];
        activeTime = data.activeTime;
        data.ResetSkillTime();
        data.Fire(selfData, selfData.actionCtrl.targetCtrl.GetTarget());
    }
    public bool CanFireSkill(SKILL_KIND kind)
    {
        if (selfData == null ||
            !skillDatas.ContainsKey(kind) ||
            activeTime > 0) { return false; }
        return skillDatas[kind].IsSkillReady();
    }
}
public class NormalAttack_Debug : SkillBaseData
{
    void Awake()
    {
        this.skillId = 0;
        this.skillDamage = 10.0f;
        this.skillKind = SKILL_KIND.normalAttack01;
        this.skillType = SKILL_TYPE.fastOnce;
        this.skillCoolTime = 60.0f;
    }
}