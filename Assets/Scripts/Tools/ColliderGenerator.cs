using UnityEngine;

namespace Assets.Scripts.Tools
{
    public static class ColliderGenerator
    {
        public static Mesh GenerateCylinderCollider(float radius, float height, int sideFaceCount)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[sideFaceCount * 2];
            int[] vertixesIndexes = new int[3 * ((sideFaceCount - 2) * 2 + sideFaceCount * 2)];
            Vector3[] normals = new Vector3[vertices.Length];

            int currentVertexIndex = 0;
            int trianglesInTopFace = sideFaceCount - 2;
            int currentTriangles = 0;
            int delta = 1;

            float angle = 0;
            float delta_angle = Mathf.PI * 2 / sideFaceCount;

            for (int i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i] = new Vector3(radius * Mathf.Cos(angle), height / 2.0f, radius * Mathf.Sin(angle));
                angle += delta_angle;
            }

            angle = 0;
            for (int i = vertices.Length / 2; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(radius * Mathf.Cos(angle), -height / 2.0f, radius * Mathf.Sin(angle));
                angle += delta_angle;
            }

            while (currentTriangles < trianglesInTopFace)
            {
                GenerateTrianglesTop(0, vertices.Length / 2, delta, ref currentTriangles, ref currentVertexIndex, vertixesIndexes);
                delta *= 2;
            }

            delta = 1;
            currentTriangles = 0;
            while (currentTriangles < trianglesInTopFace)
            {
                GenerateTrianglesBottom(vertices.Length / 2, vertices.Length, delta, ref currentTriangles, ref currentVertexIndex, vertixesIndexes);
                delta *= 2;
            }

            int diffBetweenTopAndBottom = vertices.Length / 2;
            for (int i = 0; i < sideFaceCount - 1; i++)
            {
                vertixesIndexes[currentVertexIndex++] = i;
                vertixesIndexes[currentVertexIndex++] = i + 1 + diffBetweenTopAndBottom;
                vertixesIndexes[currentVertexIndex++] = i + diffBetweenTopAndBottom;

                vertixesIndexes[currentVertexIndex++] = i + 1;
                vertixesIndexes[currentVertexIndex++] = i + 1 + diffBetweenTopAndBottom;
                vertixesIndexes[currentVertexIndex++] = i;
            }

            vertixesIndexes[currentVertexIndex++] = vertices.Length / 2;
            vertixesIndexes[currentVertexIndex++] = vertices.Length / 2 - 1;
            vertixesIndexes[currentVertexIndex++] = 0;

            vertixesIndexes[currentVertexIndex++] = vertices.Length / 2 - 1;
            vertixesIndexes[currentVertexIndex++] = vertices.Length / 2;
            vertixesIndexes[currentVertexIndex++] = vertices.Length - 1;

            for (int i = 0; i < normals.Length / 2; i++)
            {
                normals[i] = Vector3.up;
            }

            for (int i = normals.Length / 2; i < normals.Length; i++)
            {
                normals[i] = Vector3.down;
            }

            mesh.name = "Custrom cylinder";
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = vertixesIndexes;

            mesh.RecalculateNormals();

            return mesh;
        }

        private static void GenerateTrianglesBottom(int start, int end, int delta, ref int currentTriangles, ref int currentIndex, int[] indexes)
        {
            int internalCount = 0;
            bool haveNoFullTriangle = false;
            for (int i = start; i < end; i += delta)
            {
                haveNoFullTriangle = true;
                indexes[currentIndex] = i;
                if ((internalCount + 1) % 3 == 0)
                {
                    haveNoFullTriangle = false;
                    if (i < end - delta)
                    {
                        internalCount++;
                        currentIndex++;
                        indexes[currentIndex] = i;
                    }

                    currentTriangles++;
                }
                internalCount++;
                currentIndex++;
            }

            if (haveNoFullTriangle)
            {
                indexes[currentIndex] = start;
                currentIndex++;
                currentTriangles++;
            }
        }

        private static void GenerateTrianglesTop(int start, int end, int delta, ref int currentTriangles, ref int currentIndex, int[] indexes)
        {
            int internalCount = 0;
            bool haveNoFullTriangle = false;
            for (int i = end - 1; i >= start; i -= delta)
            {
                haveNoFullTriangle = true;
                indexes[currentIndex] = i;
                if ((internalCount + 1) % 3 == 0)
                {
                    haveNoFullTriangle = false;
                    if (i >= start + delta)
                    {
                        internalCount++;
                        currentIndex++;
                        indexes[currentIndex] = i;
                    }

                    currentTriangles++;
                }
                internalCount++;
                currentIndex++;
            }

            if (haveNoFullTriangle)
            {
                indexes[currentIndex] = end - 1;
                currentIndex++;
                currentTriangles++;
            }
        }
    }
}