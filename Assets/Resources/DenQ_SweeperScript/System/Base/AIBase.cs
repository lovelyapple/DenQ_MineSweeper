using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;

public class AIBase : MonoBehaviour
{
    public enum DenQAIModes
    {
        StandBy,
        Attacking,
    }
    public uint AIMode = 0;
    [SerializeField] FieldObjectData _self;
    public FieldObjectData self
    {
        get
        {
            if (_self == null)
            {
                _self = gameObject.GetComponent<FieldObjectData>();

                if (_self == null)
                {
                    Logger.GError("could not find gameObject!");
                }
            }

            return _self;
        }
    }
    [SerializeField] ActionController _actionCtrl;
    public ActionController actionController
    {
        get
        {
            if (_actionCtrl == null)
            {
                _actionCtrl = gameObject.GetComponent<ActionController>();

                if (_actionCtrl == null)
                {
                    Logger.GError("coudl not find actionCtrl!");
                }
            }
            return _actionCtrl;
        }
    }
    public virtual void InitialzieAI()
    {

    }
}
