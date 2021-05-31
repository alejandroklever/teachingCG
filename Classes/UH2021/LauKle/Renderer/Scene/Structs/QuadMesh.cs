using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene.Structs
{
    public class QuadMesh<V> : Mesh<V> where V : struct, IVertex<V>
    {
        public int[,] Faces { get; private set; }

        public QuadMesh(V[] vertices, int[] indices, int[,] faces, Topology topology = Topology.Triangles) : base(
            vertices, indices, topology)
        {
            Faces = faces;
        }

        public new QuadMesh<V> Clone()
        {
            var vertices = Vertices.Clone() as V[];
            var indexes = Indices.Clone() as int[];
            var faces = Faces.Clone() as int[,];
            return new QuadMesh<V>(vertices, indexes, faces, Topology);
        }
    }
}