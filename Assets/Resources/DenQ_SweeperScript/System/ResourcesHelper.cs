using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesHelper
{
    [SerializeField]
    static GameObject _systemRootObj = null;
    [SerializeField]
    static GameObject _fieldObjectRootObj = null;
    [SerializeField]
    static GameObject _effectRootObj = null;
    [SerializeField]
    static GameObject _UIRootObj = null;
    public static GameObject systemRootObj
    {
        private set { }
        get
        {
            if (_systemRootObj == null)
            {
                _systemRootObj = GameObject.Find("SystemRoot");
            }
            return _systemRootObj;
        }
    }
    public static GameObject fieldObjectRootObj
    {
        private set { }
        get
        {
            if (_fieldObjectRootObj == null)
            {
                _fieldObjectRootObj = GameObject.Find("FieldObjectRoot");
            }
            return _fieldObjectRootObj;
        }
    }
    public static GameObject effectRootObj
    {
        private set { }
        get
        {
            if (_effectRootObj == null)
            {
                _effectRootObj = GameObject.Find("EffectRoot");
            }
            return _effectRootObj;
        }
    }
    public static GameObject UIRootObj
    {
        private set { }
        get
        {
            if (_UIRootObj == null)
            {
                _UIRootObj = GameObject.Find("UIRoot");
            }
            return _UIRootObj;
        }
    }
    
}
