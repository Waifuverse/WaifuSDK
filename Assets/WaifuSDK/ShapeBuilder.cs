#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(VoiceBlendShapeFinder))]
public class ShapeBuilder : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        VoiceBlendShapeFinder myScript = (VoiceBlendShapeFinder)target;
        if(GUILayout.Button("Set BlendShapes"))
        {
            myScript.FindMatchingBlendShapesButton();
        }
    }
}
#endif