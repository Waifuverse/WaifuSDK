#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(CopyFromTemplate))]
public class WaifuBuilder : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CopyFromTemplate myScript = (CopyFromTemplate)target;
        if(GUILayout.Button("Add Components"))
        {
            myScript.CopyComponentsFromTemplate();
        }
    }
}
#endif
