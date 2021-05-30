using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class ModelImportSettings
    {
        public bool IsReadable { get; private set; }
        public bool GenerateUv { get; private set; }

        public ModelImportSettings(bool isReadable, bool generateUv)
        {
            IsReadable = isReadable;
            GenerateUv = generateUv;
        }
    }
}