using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class ModelsViewer
    {
        private Model[] _models;
        private ModelView[] _views;
        private Vector2 _scrollPosition = Vector2.zero;
        private int _columnNumber = 8;

        public event System.Action<Model, bool, bool> ChangedParametrs;

        public void SetModels(Model[] models)
        {
            _models = models;
            _views = new ModelView[models.Length];
            for(int i = 0; i < _views.Length; i++)
            {
                _views[i] = new ModelView();
                _views[i].Number = (i + 1).ToString();
                _views[i].MeshName = _models[i].MeshName;
                _views[i].VertexCount = _models[i].VertexCount.ToString();
                _views[i].PolygonCount = _models[i].PolygonCount.ToString();
                _views[i].VertexCountInScene = _models[i].VertexCountInScene.ToString();
                _views[i].CountInScene = _models[i].CountInScene.ToString();
                _views[i].IsReadable = _models[i].ImportSettings.IsReadable;
                _views[i].GenerateUv = _models[i].ImportSettings.GenerateUv;
            }
        }

        public void View()
        {
            float wight = (float)Screen.width / (float)_columnNumber;

            EditorGUILayout.BeginVertical();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, true,
                 GUILayout.MinHeight(Screen.height * 0.2f),
                 GUILayout.MinWidth(Screen.width * 0.5f),
                 GUILayout.MaxHeight(Screen.height),
                 GUILayout.MaxWidth(Screen.width));

            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(50));
            EditorGUILayout.LabelField("Mesh name", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(200));
            EditorGUILayout.LabelField("Vertex count", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("Polygon count", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("Count in scene", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(100));
            EditorGUILayout.LabelField("Vertex count in scene", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(150));
            EditorGUILayout.LabelField("Readable", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(80));
            EditorGUILayout.LabelField("UV lightmap", EditorStyles.boldLabel, GUILayout.Width(wight), GUILayout.MaxWidth(80));
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < _views.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(_views[i].Number, GUILayout.Width(wight), GUILayout.MaxWidth(50));
                EditorGUILayout.LabelField(_views[i].MeshName, GUILayout.Width(wight), GUILayout.MaxWidth(200));
                EditorGUILayout.LabelField(_views[i].VertexCount.ToString(), GUILayout.Width(wight), GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField(_views[i].PolygonCount.ToString(), GUILayout.Width(wight), GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField(_views[i].CountInScene.ToString(), GUILayout.Width(wight), GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField(_views[i].VertexCountInScene.ToString(), GUILayout.Width(wight), GUILayout.MaxWidth(150));
                _views[i].IsReadable = EditorGUILayout.Toggle(_views[i].IsReadable, GUILayout.Width(wight), GUILayout.MaxWidth(80));
                _views[i].GenerateUv = EditorGUILayout.Toggle(_views[i].GenerateUv, GUILayout.Width(wight), GUILayout.MaxWidth(80));

                if(_views[i].IsReadable != _models[i].ImportSettings.IsReadable ||
                    _views[i].GenerateUv != _models[i].ImportSettings.GenerateUv)
                {
                    ChangedParametrs?.Invoke(_models[i], _views[i].IsReadable, _views[i].GenerateUv);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();
        }

        class ModelView
        {
            public string Number;
            public string MeshName;
            public string VertexCount;
            public string PolygonCount;
            public string CountInScene;
            public string VertexCountInScene;
            public bool IsReadable;
            public bool GenerateUv;
        }
    }
}