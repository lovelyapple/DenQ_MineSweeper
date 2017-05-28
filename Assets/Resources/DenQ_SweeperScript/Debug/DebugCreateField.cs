using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DebugCreateField : EditorWindow
{
	public static int SizeX = 1;
	public static int SizeZ = 1;

    [MenuItem("Debug/CreateField")]
    static void Opend()
    {
        EditorWindow.GetWindow<DebugCreateField>("FieldCreateWindow");
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        {
            GUILayout.BeginVertical();
            {
                GUILayout.Label("DebugFieldCreateWindow");
                if (EditorApplication.isPlaying)
                {

                    if (GUILayout.Button("Create"))
                    {
                        FieldManager.GetInstance().CreateDebugFieldBlock();
                    }
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Field Size X:",GUILayout.Width(100));
						GUILayout.Label(string.Format("{0}",SizeX),GUILayout.Width(100));
					}
					GUILayout.EndHorizontal();
					SizeX = (int)GUILayout.HorizontalScrollbar(SizeX,1,1,51);
					GUILayout.BeginHorizontal();
					{
						GUILayout.Label("Field Size Z:",GUILayout.Width(100));
						GUILayout.Label(string.Format("{0}",SizeZ),GUILayout.Width(100));
					}
					GUILayout.EndHorizontal();					
					SizeZ = (int)GUILayout.HorizontalScrollbar(SizeZ,1,1,51);

                    if (GUILayout.Button("CreateAll"))
                    {
                       FieldManager.GetInstance().CreateDebugFeildAll((uint)SizeX,(uint)SizeZ);
                    }
                    if (GUILayout.Button("Clear"))
                    {
                        FieldManager.GetInstance().ClearField();
                    }
                }
                else
                {
                    GUILayout.Label("only used in playing mode");
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
