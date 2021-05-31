using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class CustomMeshStatistics : EditorWindow
    {
        private Model[] _models;
        private ModelsViewer _viewer = new ModelsViewer();
        private Filter _filter = new Filter();

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
            if (GUILayout.Button("Update Meshes", GUILayout.MaxWidth(100), GUILayout.MaxHeight(50)))
            {
                UpdateModels();
            }

            _filter.View();
            EditorGUILayout.Space(2);
            if (GUILayout.Button("Apply filters", GUILayout.MaxWidth(100), GUILayout.MaxHeight(50)))
            {
                SetModelsInViewer();
            }

            EditorGUILayout.Space(4);
            _viewer.View();
        }

        private void UpdateModels()
        {
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
            _models = ModelsGenerator.GenerateModels(meshFilters);
            SetModelsInViewer();
        }

        private void SetModelsInViewer()
        {
            Model[] models = new Model[_models.Length];
            System.Array.Copy(_models, models, _models.Length);
            models = _filter.UseFilter(models);
            _viewer.SetModels(models);
        }

        private void OnChangeImportParametrs(Model model, bool isReadable, bool generateUv)
        {
            ModelImportSettings settings = new ModelImportSettings(isReadable, generateUv);
            model.ChangeImportSettings(settings);
            ReimportModel.ChangeModelImportSettings(model.AssetsPath, settings);
        }
    }
}

