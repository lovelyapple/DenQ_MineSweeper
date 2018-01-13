using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public class TargetInfo
{
    public FieldObjectData objectData;
    public float? rangeToSelf;
    public void AnalyseRangeToSelf(FieldObjectData self)
    {
        if (objectData == null || objectData.isDead)
        {
            rangeToSelf = null;
            return;
        }

		rangeToSelf = Vector3.Distance(self.gameObject.transform.position,objectData.gameObject.transform.position);
    }
}
// ターゲット検索、距離計算などで使うもの
public class TargetController
{
    FieldObjectData self;
    List<TargetInfo> targetList;
    public TargetController(FieldObjectData self)
    {
        this.self = self;
    }
    void RefreshTarget()
    {
        if (targetList == null)
        {
            targetList = new List<TargetInfo>();
            return;
        }

        targetList.RemoveAll(t => t.objectData == null || t.objectData.isDead);

        foreach (var info in targetList)
        {
			info.AnalyseRangeToSelf(self);
        }
    }
	public bool CanFireSkill()
	{
		//TODOターゲットコントローラーを使ったターゲットの判定
		return true;
	}
}
