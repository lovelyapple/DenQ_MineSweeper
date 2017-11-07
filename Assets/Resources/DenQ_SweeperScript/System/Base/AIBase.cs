using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class AIBase : MonoBehaviour
{
    public FieldObjectData self { get; private set; }
    public ActionController actionCtrl { get; private set; }
    void Awake()
    {
        CatchSelfInfos();
    }
    public bool CatchSelfInfos()
    {
        self = gameObject.GetComponent<FieldObjectData>();

        if (self == null)
        {
            Logger.GError("could not find FieldObject");
            return false;
        }

        actionCtrl = gameObject.GetComponent<ActionController>();

        if (actionCtrl == null)
        {
            Logger.GError("could not find actionController!");
            return false;
        }

        return true;
    }

}
