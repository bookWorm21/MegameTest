using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Assets.Editor.Scripts.MeshStatistics.FilterFields;


namespace Assets.Editor.Scripts.MeshStatistics
{ 
    public class Filter
    {
        private StringField _subStringField = new StringField("Search by name");

        private ValueField _countVertexField = new ValueField("Vertex count");
        private ValueField _countPolygonField = new ValueField("Polygon count");
        private ValueField _countInSceneField = new ValueField("Count in scene");
        private ValueField _sumVertexField = new ValueField("Sum vertex");

        private BoolField _readableField = new BoolField("Readable Filter");
        private BoolField _uvGenerateField = new BoolField("UV lightmap Filter");

        public bool FilterEnabled { get; private set; }

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

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndToggleGroup();
        }

        public Model[] UseFilter(Model[] models)
        {
            if (FilterEnabled)
            {
                Model[] filteredModels = models;
                int minValue;
                int maxValue;
                bool enabled;

                if (_subStringField.UseFilter)
                {
                    string subString = _subStringField.SubString;
                    filteredModels = filteredModels.Where(m => m.MeshName.Contains(subString)).ToArray();
                }

                if (_countVertexField.UseFilter)
                {
                    minValue = _countVertexField.MinValue;
                    maxValue = _countVertexField.MaxValue;
                    filteredModels = filteredModels.Where(m => m.VertexCount <= maxValue && 
                    m.VertexCount >= minValue).ToArray();
                }

                if(_countPolygonField.UseFilter)
                {
                    minValue = _countPolygonField.MinValue;
                    maxValue = _countPolygonField.MaxValue;
                    filteredModels = filteredModels.Where(m => m.PolygonCount <= maxValue && 
                    m.PolygonCount >= minValue).ToArray();
                }


                if (_countInSceneField.UseFilter)
                {
                    minValue = _countInSceneField.MinValue;
                    maxValue = _countInSceneField.MaxValue;

                    filteredModels = filteredModels.Where(m => m.CountInScene <= maxValue && 
                    m.CountInScene >= minValue).ToArray();
                }

                if(_sumVertexField.UseFilter)
                {
                    minValue = _sumVertexField.MinValue;
                    maxValue = _sumVertexField.MaxValue;
                    filteredModels = filteredModels.Where(m => m.VertexCountInScene <= maxValue && 
                    m.VertexCountInScene >= minValue).ToArray();
                }

                if (_readableField.UseFilter)
                {
                    enabled = _readableField.ChangeIndex != 1 ? true : false;
                    filteredModels = filteredModels.Where(m => m.ImportSettings.IsReadable == enabled).ToArray();
                }

                if (_uvGenerateField.UseFilter)
                {
                    enabled = _uvGenerateField.ChangeIndex != 1;
                    filteredModels = filteredModels.Where(m => m.ImportSettings.GenerateUv == enabled).ToArray();
                }

                return filteredModels.ToArray();
            }
            else
            {
                return models;
            }
        }
    }
}