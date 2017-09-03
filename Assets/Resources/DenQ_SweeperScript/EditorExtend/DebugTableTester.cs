using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DebugTableTester : EditorWindow
{
    [MenuItem("Debug/TableHelper")]
    static void Opend()
    {
        EditorWindow.GetWindow<DebugTableTester>("TableHelper");
    }
    class MemberInfoLabel
    {
        public string memberName = "";
        public string memberType = "";
        public string memberValue = "";
    }
    class ClassData
    {
        public List<MemberInfoLabel> memberInfoLabels = null;
        public bool isEmpty { set { } get { return memberInfoLabels == null || memberInfoLabels.Count <= 0; } }
        public void ShowDetail()
        {
            if (isEmpty) return;

            foreach (var info in memberInfoLabels)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("name:" + info.memberName);
                GUILayout.Label("type:" + info.memberType);
                GUILayout.Label("value:" + info.memberValue);
                GUILayout.EndHorizontal();
            }
        }
    }
    List<ClassData> classDatas = new List<ClassData>();
    bool lensing = false;
    Vector2 scrollPosition = Vector2.zero;
    // Use this for initialization
    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        {
            GUILayout.Label("DebugTalbeHelper");
            if (GUILayout.Button("read"))
            {
                TableManager.Init();
                SystemManager.GetInstance().ReadTable();
            }
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        {
            GUILayout.Label("Table Watcher");

            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("BombData");
                if (GUILayout.Button("Open"))
                {
                    lensing = true;
                    var datas = BombTableHelper.GetBombAllDatas();
                    List<object> objs = new List<object>();
                    foreach (var data in datas)
                    {
                        objs.Add(data);
                    }
                    ClassLensSetup(typeof(BombData), objs);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal("box");
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Detail Area");
            if (lensing)
            {
                ShowLensingDetail();
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndHorizontal();
    }
    void ShowLensingDetail()
    {
        if (classDatas == null || classDatas.Count <= 0) return;
        foreach (var data in classDatas)
        {
            if (data.isEmpty) continue;

            GUILayout.BeginVertical("box");
            {
                data.ShowDetail();
            }
            GUILayout.EndVertical();
        }
    }
    void ClassLensSetup(Type t, List<object> targets)
    {
        classDatas.Clear();
        classDatas = new List<ClassData>();

        MemberInfo[] members = t.GetMembers(
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.DeclaredOnly);

        foreach (var target in targets)
        {
            var classData = new ClassData();
            classData.memberInfoLabels = new List<MemberInfoLabel>();
            foreach (var member in members)
            {
                var memberInfoLabel = new MemberInfoLabel();
                memberInfoLabel.memberName = member.Name;
                memberInfoLabel.memberType = member.MemberType.ToString();
                memberInfoLabel.memberValue = ReflectionUtils.GetMemberValue(member, target).ToString();
                classData.memberInfoLabels.Add(memberInfoLabel);
            }
            classDatas.Add(classData);
        }
    }
}
public static class ReflectionUtils
{
    public static object GetMemberValue(MemberInfo member, object target)
    {
        switch (member.MemberType)
        {
            case MemberTypes.Field:
                return ((FieldInfo)member).GetValue(target);
            case MemberTypes.Property:
                try
                {
                    return ((PropertyInfo)member).GetValue(target, null);
                }
                catch (TargetParameterCountException e)
                {
                    throw new ArgumentException(" MemberInfo hase index parameters", "member", e);
                }
            default:
                //throw new ArgumentException("MemberInfo is not of type FieldInfo or PropertyInfo", "member");
                return "out of data";
        }
    }
}

