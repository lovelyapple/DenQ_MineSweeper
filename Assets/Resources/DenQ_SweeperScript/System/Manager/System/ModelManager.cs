using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQModel
{
    public class ModelManager
    {

        static ModelManager _instance;
        ModelManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ModelManager();
            }
            return _instance;
        }
        static Dictionary<string, ModelBase> _modelDict = new Dictionary<string, ModelBase>();
        public static T GetModel<T>(string key) where T :ModelBase,new()
        {
            T outM = new T();
            if(!_modelDict.ContainsKey(key))
            {
                outM = new T();
                _modelDict.Add(key,outM);
            }

            return (T)_modelDict[key];
        }
        public ModelBase GetModel(String key)
        {
            return _modelDict[key];
        }
    }
}