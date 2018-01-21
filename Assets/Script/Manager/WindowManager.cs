using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DenQ;
namespace Resource
{
    public enum WindowIndex
    {
        FieldMenu,
    }
    public class WindowManager : ManagerBase<WindowManager>
    {
        static Dictionary<WindowIndex, string> _windowDirectory = new Dictionary<WindowIndex, string>()
        {
            {WindowIndex.FieldMenu,"FieldMenu"},
        };
        public static Dictionary<WindowIndex, WindowBase> _windowDict;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            SetInstance(this);
            _windowDict = new Dictionary<WindowIndex, WindowBase>();

            //サイズ分を確保
            foreach (WindowIndex index in Enum.GetValues(typeof(WindowIndex)))
            {
                _windowDict.Add(index, null);
            }
        }
        public static void CreateOpenWindow(WindowIndex index,
                                            Action<WindowBase> onOpened = null,
                                            WindowIndex? previousWindow = null)
        {
            WindowBase _base;

            if (_windowDict.TryGetValue(index, out _base))
            {
                _base.Open();
            }

            _base = CreateWindow(index);

            if (_base == null) return;

            _base.Open();

            if (onOpened != null)
            {
                onOpened(_base);
            }

        }
        static WindowBase CreateWindow(WindowIndex index)
        {
            var go = ResourcesManager.GetInstance().LoadWindowObject(_windowDirectory[index]);

            if (go == null)
            {
                Debug.LogError("could not find prefab obj" + ((WindowIndex)index).ToString());
                return null;
            }

            var targetolder = RootHolder.UIRootObj;

            if (targetolder == null) return null;

            go.transform.SetParent(targetolder.transform);

            WindowBase wndBase = (Instantiate(go) as GameObject).GetComponent<WindowBase>();

            if (wndBase == null)
            {
                Debug.LogError("could not find windowbase component in " + ((WindowIndex)index).ToString());
                return null;
            }

            return wndBase;
        }
    }
}
