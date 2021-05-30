using UnityEditor;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public static class ReimportModel
    {
        public static void ChangeModelImportSettings(string path, ModelImportSettings settings)
        {
            ModelImporter modelImporter = (ModelImporter)AssetImporter.GetAtPath(path);
            modelImporter.isReadable = settings.IsReadable;
            modelImporter.generateSecondaryUV = settings.GenerateUv;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
}