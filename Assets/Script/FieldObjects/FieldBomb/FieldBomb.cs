using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class FieldBomb : FieldObjectData
{
    public FieldBombData bombData { get; private set; }
    public void SetupBomb(FieldBombData data)
    {
        this.bombData = data;
    }
    public void SetupBomb(ulong bombId)
    {
        this.bombData = FieldBombTableHelper.GetBombDataByID(bombId);
    }
	
}
