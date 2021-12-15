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
        if (GUILayout.Button("UpdateBranchRotation"))
        {
            branchRenderer.UpdateBranchRotation();
        }
        if (GUILayout.Button("Reset Rotation"))
        {
            branchRenderer.ResetBranchRotation();
        }
    }

}