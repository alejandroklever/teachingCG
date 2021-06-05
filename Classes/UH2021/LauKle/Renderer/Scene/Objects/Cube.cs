﻿using System.Linq;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
	public class Cube<V>: SceneObject<V> where V : struct, INormalVertex<V>
	{
		public readonly int roundness;
		public readonly int xSize;
		public readonly int ySize;
		public readonly int zSize;

		public Cube(Transform transform,int xSize, int ySize, int zSize, int roundness = 0): base(transform)
		{
			this.xSize = xSize;
			this.ySize = ySize;
			this.zSize = zSize;
			this.roundness = roundness;

			var meshes = new[]
			{
				// front
				Manifold2<V>.SurfaceDiscrete(xSize, ySize,
					(u, v) => float3(u, v, 0)),
				// right
				Manifold2<V>.SurfaceDiscrete(zSize, ySize,
					(u, v) => float3(xSize, v, u)),
				// back
				Manifold2<V>.SurfaceDiscrete(xSize, ySize,
					(u, v) => float3(xSize - u, v, zSize)),
				// left
				Manifold2<V>.SurfaceDiscrete(zSize, ySize,
					(u, v) => float3(0,  v, zSize - u)),
				// top
				Manifold2<V>.SurfaceDiscrete(xSize, zSize,
					(u, v) => float3(u, ySize, v)),
				// bottom
				Manifold2<V>.SurfaceDiscrete(xSize, zSize,
					(u, v) => float3(xSize - u, 0, v)),
			};
			
			Mesh = meshes.Aggregate((t, x) => t.Concat(x)).Weld();
			
			for (var i = 0; i < Mesh.Vertices.Length; i++)
			{
				var vertex = Mesh.Vertices[i];
				var inner = vertex.Position;

				var (x, y, z) = vertex.Position;

				if (x < roundness)
					inner.x = roundness;
				else if (x > xSize - roundness)
					inner.x = xSize - roundness;

				if (y < roundness)
					inner.y = roundness;
				else if (y > ySize - roundness)
					inner.y = ySize - roundness;

				if (z < roundness)
					inner.z = roundness;
				else if (z > zSize - roundness)
					inner.z = zSize - roundness;

				Mesh.Vertices[i].Normal = normalize(vertex.Position - inner);
				Mesh.Vertices[i].Position = inner + Mesh.Vertices[i].Normal * roundness;
			}
			
			Mesh.ComputeNormals();
			UpdateTranslation();
		}
	}
}