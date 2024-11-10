using System.Collections.Generic;
using UnityEngine;
using VertexData = System.Tuple<UnityEngine.Vector3, UnityEngine.Vector3, UnityEngine.Vector2>;

public static class MeshUtils
{
    public enum BlockSide
    {
        BOTTOM,
        TOP,
        LEFT,
        RIGHT,
        FRONT,
        BACK
    }

    public enum BlockType
    {
        GRASSTOP,
        GRASSSIDE,
        DIRT,
        WATER,
        STONE,
        SAND
    }

    public static Vector2[,] blockUVs =
    {
        /*GRASSTOP*/
        {
            new(0.125f, 0.375f), new(0.1875f, 0.375f),
            new(0.125f, 0.4375f), new(0.1875f, 0.4375f)
        },
        /*GRASSSIDE*/
        {
            new(0.1875f, 0.9375f), new(0.25f, 0.9375f),
            new(0.1875f, 1.0f), new(0.25f, 1.0f)
        },
        /*DIRT*/
        {
            new(0.125f, 0.9375f), new(0.1875f, 0.9375f),
            new(0.125f, 1.0f), new(0.1875f, 1.0f)
        },
        /*WATER*/
        {
            new(0.875f, 0.125f), new(0.9375f, 0.125f),
            new(0.875f, 0.1875f), new(0.9375f, 0.1875f)
        },
        /*STONE*/
        {
            new(0, 0.875f), new(0.0625f, 0.875f),
            new(0, 0.9375f), new(0.0625f, 0.9375f)
        },
        /*SAND*/
        {
            new(0.125f, 0.875f), new(0.1875f, 0.875f),
            new(0.125f, 0.9375f), new(0.1875f, 0.9375f)
        }
    };


    public static Mesh MergeMeshes(Mesh[] meshes)
    {
        var mesh = new Mesh();

        var pointsOrder = new Dictionary<VertexData, int>();
        var pointsHash = new HashSet<VertexData>();
        var tris = new List<int>();

        var pIndex = 0;
        for (var i = 0; i < meshes.Length; i++) //loop through each mesh
        {
            if (meshes[i] == null) continue;
            for (var j = 0; j < meshes[i].vertices.Length; j++) //loop through each vertex of the current mesh
            {
                var v = meshes[i].vertices[j];
                var n = meshes[i].normals[j];
                var u = meshes[i].uv[j];
                var p = new VertexData(v, n, u);
                if (!pointsHash.Contains(p))
                {
                    pointsOrder.Add(p, pIndex);
                    pointsHash.Add(p);

                    pIndex++;
                }
            }

            for (var t = 0; t < meshes[i].triangles.Length; t++)
            {
                var triPoint = meshes[i].triangles[t];
                var v = meshes[i].vertices[triPoint];
                var n = meshes[i].normals[triPoint];
                var u = meshes[i].uv[triPoint];
                var p = new VertexData(v, n, u);

                int index;
                pointsOrder.TryGetValue(p, out index);
                tris.Add(index);
            }

            meshes[i] = null;
        }

        ExtractArrays(pointsOrder, mesh);
        mesh.triangles = tris.ToArray();
        mesh.RecalculateBounds();
        return mesh;
    }

    public static void ExtractArrays(Dictionary<VertexData, int> list, Mesh mesh)
    {
        var verts = new List<Vector3>();
        var norms = new List<Vector3>();
        var uvs = new List<Vector2>();

        foreach (var v in list.Keys)
        {
            verts.Add(v.Item1);
            norms.Add(v.Item2);
            uvs.Add(v.Item3);
        }

        mesh.vertices = verts.ToArray();
        mesh.normals = norms.ToArray();
        mesh.uv = uvs.ToArray();
    }
}