using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public static class ModelsGenerator
    {
        public static Model[] GenerateModels(MeshFilter[] meshFilters)
        {
            var groupingModels = meshFilters.GroupBy(f => f.sharedMesh).
                Select(g => new { Mesh = g.Key, Count = g.Count() });

            Model[] models = new Model[groupingModels.Count()];
            int i = 0;

            foreach (var group in groupingModels)
            {
                Mesh mesh = group.Mesh;

                string path = AssetDatabase.GetAssetPath(mesh);
                ModelImporter importer = (ModelImporter)AssetImporter.GetAtPath(path);

                models[i] = new Model(mesh, 
                                      group.Mesh.name,
                                      path,
                                      mesh.vertexCount,
                                      mesh.triangles.Length / 3,
                                      group.Count,
                                      group.Count * mesh.vertexCount,
                                      mesh.GetInstanceID(),
                                      new ModelImportSettings(mesh.isReadable, importer.generateSecondaryUV));

                i++;
            }

            models = models.OrderByDescending(m => m.VertexCountInScene).ToArray();
            return models;
        }
    }
}