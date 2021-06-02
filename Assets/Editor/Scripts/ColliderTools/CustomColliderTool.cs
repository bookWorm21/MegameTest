using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Tools;

namespace Assets.Editor.Scripts.ColliderTools
{
    public class CustomColliderTool : EditorWindow
    {
        [SerializeField] private CustomCylinderColllider _template;

        private static CustomColliderTool instance;

        [MenuItem("GameObject/3D Object / Custom / Cylinder")]
        private static void Init()
        {
            instance = CreateInstance<CustomColliderTool>();
            CustomCylinderColllider cylinder = Instantiate(instance._template);
            cylinder.GenerateMesh();
        }
    }
}