using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DenQ.Mgr
{
    public class MangerBase<T> : MonoBehaviour where T:
    class
    {
        private static T _instance = null;
        public static void SetInstance(T instance)
        {
            _instance = instance;
        }
        public static T GetInstance()
        {
            return _instance;
        }
        protected void RemoveInstance()
        {
            _instance = null;
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}