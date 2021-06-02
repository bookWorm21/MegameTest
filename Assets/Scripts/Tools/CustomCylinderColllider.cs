using UnityEngine;

namespace Assets.Scripts.Tools
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshCollider))]
    public class CustomCylinderColllider : MonoBehaviour
    {
        [SerializeField] private float _radius = 3.0f;
        [SerializeField] private float _height = 3.0f;
        [SerializeField] private int _sideFaceCount = 3;

        private MeshCollider _meshCollider;
        private Mesh _mesh;

        public System.Action ChangedMesh;

        private void Awake()
        {
            ChangedMesh = OnChangeMesh;
            _meshCollider = GetComponent<MeshCollider>();
            _mesh = new Mesh();
        }

        private void OnValidate()
        {
            GenerateMesh();
        }

        public void GenerateMesh()
        {
            CheckFields();
            _mesh = ColliderGenerator.GenerateCylinderCollider(_radius, _height, _sideFaceCount);
            ChangedMesh?.Invoke();
        }

        private void OnChangeMesh()
        {
            _meshCollider.sharedMesh = _mesh;
        }

        private void CheckFields()
        {
            if (_sideFaceCount < 3)
            {
                _sideFaceCount = 3;
            }
            else if (_sideFaceCount > 254)
            {
                _sideFaceCount = 254;
            }

            if (_radius < 0)
            {
                _radius = 1.0f;
            }

            if (_height < 0)
            {
                _height = 1.0f;
            }
        }
    }
}