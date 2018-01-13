using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;

public enum FieldObjectStats
{
    dying,
    dead,
}
public class FieldObjectStateController
{
    public FieldObjectData self { get; private set; }
    uint fieldObjState;
    public FieldObjectStateController(FieldObjectData selfObjData)
    {
        this.self = selfObjData;
    }
    void Update()
    {
        switch ((FieldObjectStats)fieldObjState)
        {
            case FieldObjectStats.dying:
                fieldObjState = (uint)FieldObjectStats.dead;
                break;
            case FieldObjectStats.dead:
                self.DestroyObj();
                break;
        }
    }
	public void KillThis()
	{
		this.fieldObjState = (uint)FieldObjectStats.dying;
	}
}
