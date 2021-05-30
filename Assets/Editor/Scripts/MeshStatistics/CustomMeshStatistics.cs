using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class CustomMeshStatistics : EditorWindow
    {
        private ModelsViewer _viewer = new ModelsViewer();
        private bool filterEnabled = false;

        [MenuItem("Window/Custom/MeshStatistics")]
        static private void Init()
        {
            CustomMeshStatistics window = (CustomMeshStatistics)GetWindow(typeof(CustomMeshStatistics));
            window.Show();
        }

        private void OnEnable()
        {
            UpdateModels();
            _viewer.ChangedParametrs += OnChangeImportParametrs;
        }

        private void OnDisable()
        {
            _viewer.ChangedParametrs -= OnChangeImportParametrs;
        }

        private void OnGUI()
        {
            filterEnabled = EditorGUILayout.BeginToggleGroup("Filters", filterEnabled);
            if (filterEnabled)
            {

                if (GUILayout.Button("Apply", GUILayout.MaxWidth(100), GUILayout.MaxHeight(50)))
                {

                }
            }
            EditorGUILayout.EndToggleGroup();

            _viewer.View();
        }

        private void UpdateModels()
        {
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
            _viewer.SetModels(ModelsGenerator.GenerateModels(meshFilters));
        }

        private void OnChangeImportParametrs(Model model, bool isReadable, bool generateUv)
        {
            ModelImportSettings settings = new ModelImportSettings(isReadable, generateUv);
            model.ChangeImportSettings(settings);
            ReimportModel.ChangeModelImportSettings(model.AssetsPath, settings);
        }
    }
}

