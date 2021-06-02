using UnityEditor;

namespace Assets.Editor.Scripts.MeshStatistics
{
    public static class ReimportModel
    {
        public static bool TryChangeModelImportSettings(string path, ModelImportSettings settings)
        {
            ModelImporter modelImporter = (ModelImporter)AssetImporter.GetAtPath(path);
            if (modelImporter != null)
            {
                modelImporter.isReadable = settings.IsReadable;
                modelImporter.generateSecondaryUV = settings.GenerateUv;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                return true;
            }
            return false;
        }
    }
}