using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MVCInputController))]
public class MVCInputControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MVCInputController mvcInputController = (MVCInputController)target;
        if (GUILayout.Button("Reset"))
        {
            mvcInputController.ResetTree();
        }


    }

}