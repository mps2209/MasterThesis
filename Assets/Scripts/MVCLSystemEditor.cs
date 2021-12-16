using System.Collections;
using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(MVCLSystem))]
    public class MVCLSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            MVCLSystem lSystem = (MVCLSystem)target;
            if (GUILayout.Button("Update Rules"))
            {
                lSystem.UpdateRules();
            }


        }

    }
