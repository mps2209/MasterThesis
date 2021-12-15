using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BranchRenderer))]
public class BranchRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BranchRenderer branchRenderer = (BranchRenderer)target;
        if (GUILayout.Button("RerenderTree"))
        {
            branchRenderer.RenderTree();
        }
    }

}