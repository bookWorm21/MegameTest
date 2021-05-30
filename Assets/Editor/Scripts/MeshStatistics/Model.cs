using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public class Model
    {
        public Mesh Mesh { get; private set; }

        public string MeshName { get; private set; }

        public string AssetsPath { get; private set; }

        public int VertexCount { get; private set; }

        public int PolygonCount { get; private set; }

        public int CountInScene { get; private set; }

        public int VertexCountInScene { get; private set; }

        public int InstanceId { get; private set; }

        public ModelImportSettings ImportSettings { get; private set; }

        public Model(Mesh mesh, string meshName, string assetsPath,
            int vertexCount, int polygonCount, int countInScene, int vertexInScene, int instanceId,
            ModelImportSettings settings)
        {
            Mesh = mesh;
            MeshName = meshName;
            AssetsPath = assetsPath;
            VertexCount = vertexCount;
            PolygonCount = polygonCount;
            CountInScene = countInScene;
            VertexCountInScene = vertexInScene;
            InstanceId = instanceId;
            ImportSettings = settings;
        }

        public void ChangeImportSettings(ModelImportSettings settings)
        {
            ImportSettings = settings;
        }
    }
}