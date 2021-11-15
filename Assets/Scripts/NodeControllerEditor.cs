using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeController))]
public class NodeControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NodeController nodeController = (NodeController)target;
        if(GUILayout.Button("Add Node"))
        {
            nodeController.AddNode();
        }
    }
}