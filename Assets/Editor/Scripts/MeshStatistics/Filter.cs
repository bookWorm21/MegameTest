using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Assets.Editor.Scripts.MeshStatistics.FilterFields;


namespace Assets.Editor.Scripts.MeshStatistics
{ 
    public class Filter
    {
        private Sorter _sorter = new Sorter();

        private StringField _subStringField = new StringField("Search by name");

        private ValueField _countVertexField = new ValueField("Vertex count");
        private ValueField _countPolygonField = new ValueField("Polygon count");
        private ValueField _countInSceneField = new ValueField("Count in scene");
        private ValueField _sumVertexField = new ValueField("Sum vertex");

        private BoolField _readableField = new BoolField("Readable Filter");
        private BoolField _uvGenerateField = new BoolField("UV lightmap Filter");

        public bool FilterEnabled { get; private set; }

        public event System.Action ClickedApply;

        public void Start()
        {
            _sorter.Start();
        }

        public void View()
        {
            FilterEnabled = EditorGUILayout.BeginToggleGroup("Filters", FilterEnabled);
            if (FilterEnabled)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space(3);

                _subStringField.View();

                _countVertexField.View();
                _countPolygonField.View();
                _countInSceneField.View();
                _sumVertexField.View();

                _readableField.View();
                _uvGenerateField.View();

                _sorter.View();

                EditorGUILayout.Space(2);
                if (GUILayout.Button("Apply filters", GUILayout.MaxWidth(100), GUILayout.MaxHeight(50)))
                {
                    ClickedApply?.Invoke();
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndToggleGroup();
        }

        public IEnumerable<Model> UseFilter(IEnumerable<Model> filteredModels)
        {
            if (FilterEnabled)
            {
                if (_subStringField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.MeshName.Contains(_subStringField.SubString));
                }

                if (_countVertexField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.VertexCount <= _countVertexField.MaxValue && 
                    m.VertexCount >= _countVertexField.MinValue);
                }

                if(_countPolygonField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.PolygonCount <= _countPolygonField.MaxValue && 
                    m.PolygonCount >= _countPolygonField.MinValue);
                }

                if (_countInSceneField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.CountInScene <= _countInSceneField.MaxValue &&
                    m.CountInScene >= _countInSceneField.MinValue);
                }

                if(_sumVertexField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.VertexCountInScene <= _sumVertexField.MaxValue && 
                    m.VertexCountInScene >= _sumVertexField.MinValue);
                }

                if (_readableField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.ImportSettings.IsReadable 
                    == (_readableField.ChangeIndex != 1));
                }

                if (_uvGenerateField.UseFilter)
                {
                    filteredModels = filteredModels.Where(m => m.ImportSettings.GenerateUv 
                    == (_uvGenerateField.ChangeIndex != 1));
                }
            }

            return _sorter.UseSort(filteredModels);
        }
    }
}