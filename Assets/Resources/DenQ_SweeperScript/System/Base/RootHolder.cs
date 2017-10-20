using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
///主にシスオブジェクトインスタンスのインターフェースを保持する
public static class RootHolder
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

                if (_systemRootObj == null)
                {
                    Logger.SError("could not fine SystemRoot!");
                }
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

                if (_fieldObjectRootObj == null)
                {
                    Logger.SError("could not fine _fieldObjectRootObj!");
                }
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

                if (_effectRootObj == null)
                {
                    Logger.SError("could not fine _effectRootObj!");
                }
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

                if (_UIRootObj == null)
                {
                    Logger.SError("could not fine _UIRootObj!");
                }
            }

            return _UIRootObj;
        }
    }

}
