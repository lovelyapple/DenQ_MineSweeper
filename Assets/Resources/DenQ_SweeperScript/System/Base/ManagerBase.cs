using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DenQ
{
    ///インスタンスをもつマネージャーベース
    ///Fieldにおくもの
    public class ManagerBase<T> : MonoBehaviour where T : class
    {
        private static T _instance = null;
        public static void SetInstance(T instance)
        {
            _instance = instance;
        }
        public  static T GetInstance()
        {
            return _instance;
        }
        protected void RemoveInstance()
        {
            _instance = null;
        }
    }
}
///主にUIが使うMgr
public class SingleToneBase<T> where T : class
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    private static T _instance = null;
    public static void SetInstance(T instance)
    {
        _instance = instance;
    }
    public void RemoveInstance()
    {
        _instance = null;
    }
    public static T Get()
    {
        return _instance;
    }
}