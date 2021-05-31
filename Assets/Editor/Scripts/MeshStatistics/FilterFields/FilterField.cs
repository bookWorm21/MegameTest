using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scripts.MeshStatistics.FilterFields
{
    public abstract class FilterField
    {
        protected string _fieldName;

        public bool UseFilter { get; protected set; }

        public abstract void View();

        public FilterField(string fieldName)
        {
            _fieldName = fieldName;
        }
    }

    public class StringField : FilterField
    {
        public string SubString { get; private set; }

        public StringField(string fieldName) : base(fieldName) { }

        public override void View()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_fieldName, GUILayout.Width(100), GUILayout.MaxWidth(150));
            UseFilter = EditorGUILayout.Toggle(UseFilter);
            EditorGUILayout.EndHorizontal();
            if (UseFilter)
            {
                SubString = EditorGUILayout.TextField(SubString, GUILayout.MaxWidth(300));
            }
        }
    }

    public class BoolField : FilterField
    {
        private string[] _changes = new string[] { "enable", "disable" };

        public int ChangeIndex { get; private set; }

        public BoolField(string fieldName) : base(fieldName) { }

        public override void View()
        {
            EditorGUILayout.Space(7);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_fieldName, GUILayout.Width(100), GUILayout.MaxWidth(140));
            UseFilter = EditorGUILayout.Toggle(UseFilter);
            EditorGUILayout.EndHorizontal();
            if (UseFilter)
            {
                ChangeIndex = EditorGUILayout.Popup(ChangeIndex, _changes, GUILayout.MaxWidth(150));
            }
        }
    }

    public class ValueField : FilterField
    {
        private Color _mistakeInValue = new Color(140, 0, 0);

        private bool _toogleActive = false;

        public int MaxValue { get; private set; }

        public int MinValue { get; private set; }

        public bool RightValues { get; private set; }

        public ValueField(string fieldName) : base(fieldName) 
        {
            RightValues = true;
        }

        public override void View()
        {
            EditorGUILayout.Space(7);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_fieldName, GUILayout.Width(100), GUILayout.MaxWidth(100));
            _toogleActive = EditorGUILayout.Toggle(_toogleActive);
            EditorGUILayout.EndHorizontal();
            if (_toogleActive)
            {
                EditorGUILayout.BeginHorizontal();

                Color savedColor = GUI.backgroundColor;
                if(RightValues == false)
                {
                    GUI.backgroundColor = _mistakeInValue;
                }

                EditorGUILayout.LabelField("From:", GUILayout.Width(30), GUILayout.MaxWidth(40));
                MinValue = EditorGUILayout.IntField(MinValue,
                    GUILayout.Width(60), GUILayout.Width(100));
                EditorGUILayout.LabelField("To:", GUILayout.Width(30), GUILayout.MaxWidth(40));
                MaxValue = EditorGUILayout.IntField(MaxValue,
                    GUILayout.Width(60), GUILayout.Width(100));

                RightValues = MaxValue >= MinValue;

                GUI.backgroundColor = savedColor;
                EditorGUILayout.EndHorizontal();
            }

            UseFilter = _toogleActive && RightValues;
        }
    }
}