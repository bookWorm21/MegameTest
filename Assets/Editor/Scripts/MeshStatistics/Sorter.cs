using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class Sorter
    {
        private SortField _meshName = new SortField("mesh name", 0);
        private SortField _vertexCount = new SortField("vertex count", 1);
        private SortField _polygonCount = new SortField("polygon count", 2);
        private SortField _countInScene = new SortField("count in scene", 3);
        private SortField _sumVertex = new SortField("vertex count in scene", 4);

        private string[] _changes = new string[] { };

        private int _changeIndex = 4;

        public void Start()
        {
            _changes = new string[]
        {
            _meshName.FieldName,
            _vertexCount.FieldName,
            _polygonCount.FieldName,
            _countInScene.FieldName,
            _sumVertex.FieldName
        };
        }

        public void View()
        {
            EditorGUILayout.Space(4);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sort by", GUILayout.Width(50), GUILayout.MaxWidth(80));
            _changeIndex = EditorGUILayout.Popup(_changeIndex, _changes, GUILayout.MaxWidth(150));
            EditorGUILayout.EndHorizontal();
        }

        public IEnumerable<Model> UseSort(IEnumerable<Model> models)
        {
            if (_changeIndex == _vertexCount.Index)
            {
                models = models.OrderByDescending(m => m.VertexCount);
            }
            else if (_changeIndex == _polygonCount.Index)
            {
                models = models.OrderByDescending(m => m.PolygonCount);
            }
            else if(_changeIndex == _countInScene.Index)
            {
                models = models.OrderByDescending(m => m.CountInScene);
            }
            else if(_changeIndex == _meshName.Index)
            {
                models = models.OrderBy(m => m.MeshName);
            }
            else
            {
                models = models.OrderByDescending(m => m.VertexCountInScene);
            }

            return models;
        }

        class SortField
        {
            public string FieldName;
            public int Index;

            public SortField(string name, int index)
            {
                FieldName = name;
                Index = index;
            }
        }
    }
}