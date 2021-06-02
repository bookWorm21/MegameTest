using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public static class ModelsGenerator
    {
        public static Model[] GenerateModels(MeshFilter[] meshFilters)
        {
            var groupingModels = meshFilters.Where(f => f.sharedMesh != null).GroupBy(f => f.sharedMesh).
                Select(g => new { Mesh = g.Key, Count = g.Count() });

            Model[] models = new Model[groupingModels.Count()];
            int i = 0;

            foreach (var group in groupingModels)
            {
                Mesh mesh = group.Mesh;

                string path = AssetDatabase.GetAssetPath(mesh);
                ModelImporter importer = (ModelImporter)AssetImporter.GetAtPath(path);
                bool generateUv = false;

                if (importer != null)
                {
                    generateUv = importer.generateSecondaryUV;
                }

                models[i] = new Model(mesh, 
                                      mesh.name,
                                      path,
                                      mesh.vertexCount,
                                      mesh.triangles.Length / 3,
                                      group.Count,
                                      group.Count * mesh.vertexCount,
                                      mesh.GetInstanceID(),
                                      new ModelImportSettings(mesh.isReadable, generateUv));

                i++;
            }

            return models;
        }
    }
}