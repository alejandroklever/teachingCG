using System;
using System.Collections.Generic;
using System.Linq;
using GMath;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
	public class Cube<V> : SceneObject<V> where V : struct, INormalVertex<V>
	{
		public readonly int roundness;
		public readonly int xSize;
		public readonly int ySize;
		public readonly int zSize;

		public Cube(Transform transform, int xSize, int ySize, int zSize, int roundness = 0) : base(transform)
		{
			this.xSize = xSize;
			this.ySize = ySize;
			this.zSize = zSize;
			this.roundness = roundness;

			
				// // front
				// MyManifold<V>.SurfaceDiscrete(xSize, ySize,
				// 	(u, v) => float3(u, v, 0)),
				// right
				var right = MinimizeTrianglesYZ(MyManifold<V>.SurfaceDiscrete(zSize, ySize,
					(u, v) => float3(xSize, v, u)));
				// back
				var back = MinimizeTrianglesXY(MyManifold<V>.SurfaceDiscrete(xSize, ySize,
					(u, v) => float3(xSize - u, v, zSize)));
				// left
				// MyManifold<V>.SurfaceDiscrete(zSize, ySize,
				// 	(u, v) => float3(0, v, zSize - u)),
				// top
				var top = MinimizeTrianglesXZ(
					MyManifold<V>.SurfaceDiscrete(xSize, zSize,
						(u, v) => float3(u, ySize, v)
					)
				);
				var bottom = top.Transform(
					Transforms.DesiredTransform(
						translation: float3(0,  1, 0)
					)
				);
				// bottom
				// MyManifold<V>.SurfaceDiscrete(xSize, zSize,
				// 	(u, v) => float3(xSize - u, 0, v)),
			

			Mesh = MeshTools.Join(right, back, top, bottom).Weld(); // meshes.Aggregate((t, x) => t.Concat(x)).Weld();

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

				var normal = normalize(vertex.Position - inner);
				Mesh.Vertices[i].Position = inner + normal * roundness;
			}

			UpdateTranslation();
		}

		private Mesh<V> MinimizeTrianglesXZ(Mesh<V> mesh)
		{
			var tris = 0;
			var points = new List<float3>();
			var indices = new List<int>();
			for (var i = 0; i < mesh.Indices.Length / 3; i++)
			{
				var p1 = mesh.Vertices[mesh.Indices[i * 3 + 0]].Position;
				var p2 = mesh.Vertices[mesh.Indices[i * 3 + 1]].Position;
				var p3 = mesh.Vertices[mesh.Indices[i * 3 + 2]].Position;

				if (IsTriangleInQuadXZ(p1, p2, p3, roundness, xSize - roundness, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadXZ(p1, p2, p3, roundness, xSize - roundness, 0, roundness))
					continue;

				if (IsTriangleInQuadXZ(p1, p2, p3, 0, roundness, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadXZ(p1, p2, p3, xSize - roundness, xSize, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadXZ(p1, p2, p3, roundness, xSize - roundness, zSize - roundness, zSize))
					continue;

				points.AddRange(new[] {p1, p2, p3});
				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2});
				tris += 3;
			}

			for (int i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, ySize, i),
					float3(roundness, ySize, i + 1),
					float3(xSize - roundness, ySize, i),
					float3(xSize - roundness, ySize, i + 1)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(i, ySize, roundness),
					float3(i, ySize, zSize - roundness),
					float3(i + 1, ySize, roundness),
					float3(i + 1, ySize, zSize - roundness)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = zSize - roundness; i < zSize; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, ySize, i),
					float3(roundness, ySize, i + 1),
					float3(xSize - roundness, ySize, i),
					float3(xSize - roundness, ySize, i + 1)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = xSize - roundness; i < xSize; i++)
			{
				points.AddRange(new[]
				{
					float3(i, ySize, roundness),
					float3(i, ySize, zSize - roundness),
					float3(i + 1, ySize, roundness),
					float3(i + 1, ySize, zSize - roundness)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(roundness, ySize, roundness),
				float3(roundness, ySize, zSize - roundness),
				float3(xSize - roundness, ySize, roundness),
				float3(xSize - roundness, ySize, zSize - roundness)
			});
			indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private Mesh<V> MinimizeTrianglesYZ(Mesh<V> mesh)
		{
			var tris = 0;
			var points = new List<float3>();
			var indices = new List<int>();
			for (var i = 0; i < mesh.Indices.Length / 3; i++)
			{
				var p1 = mesh.Vertices[mesh.Indices[i * 3 + 0]].Position;
				var p2 = mesh.Vertices[mesh.Indices[i * 3 + 1]].Position;
				var p3 = mesh.Vertices[mesh.Indices[i * 3 + 2]].Position;

				if (IsTriangleInQuadYZ(p1, p2, p3, roundness, ySize - roundness, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadYZ(p1, p2, p3, roundness, ySize - roundness, 0, roundness))
					continue;

				if (IsTriangleInQuadYZ(p1, p2, p3, 0, roundness, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadYZ(p1, p2, p3, ySize - roundness, ySize, roundness, zSize - roundness))
					continue;

				if (IsTriangleInQuadYZ(p1, p2, p3, roundness, ySize - roundness, zSize - roundness, zSize))
					continue;

				points.AddRange(new[] {p1, p2, p3});
				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2});
				tris += 3;
			}

			for (int i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(xSize, roundness, i),
					float3(xSize, ySize - roundness, i),
					float3(xSize, roundness, i + 1),
					float3(xSize, ySize - roundness, i + 1)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(xSize, i, roundness),
					float3(xSize, i + 1, roundness),
					float3(xSize, i, zSize - roundness),
					float3(xSize, i + 1, zSize - roundness)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = zSize - roundness; i < zSize; i++)
			{
				points.AddRange(new[]
				{
					float3(xSize, roundness, i),
					float3(xSize, xSize - roundness, i),
					float3(xSize, roundness, i + 1),
					float3(xSize, xSize - roundness, i + 1)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = ySize - roundness; i < ySize; i++)
			{
				points.AddRange(new[]
				{
					float3(xSize, i, roundness),
					float3(xSize, i + 1, roundness),
					float3(xSize, i, zSize - roundness),
					float3(xSize, i + 1, zSize - roundness)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(xSize, roundness, roundness),
				float3(xSize, xSize - roundness, roundness),
				float3(xSize, roundness, zSize - roundness),
				float3(xSize, xSize - roundness, zSize - roundness)
			});
			indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private Mesh<V> MinimizeTrianglesXY(Mesh<V> mesh)
		{
			var tris = 0;
			var points = new List<float3>();
			var indices = new List<int>();
			for (var i = 0; i < mesh.Indices.Length / 3; i++)
			{
				var p1 = mesh.Vertices[mesh.Indices[i * 3 + 0]].Position;
				var p2 = mesh.Vertices[mesh.Indices[i * 3 + 1]].Position;
				var p3 = mesh.Vertices[mesh.Indices[i * 3 + 2]].Position;

				if (IsTriangleInQuadXY(p1, p2, p3, roundness, xSize - roundness, roundness, ySize - roundness))
					continue;

				if (IsTriangleInQuadXY(p1, p2, p3, roundness, xSize - roundness, 0, roundness))
					continue;

				if (IsTriangleInQuadXY(p1, p2, p3, 0, roundness, roundness, ySize - roundness))
					continue;

				if (IsTriangleInQuadXY(p1, p2, p3, xSize - roundness, xSize, roundness, ySize - roundness))
					continue;

				if (IsTriangleInQuadXY(p1, p2, p3, roundness, xSize - roundness, ySize - roundness, ySize))
					continue;

				points.AddRange(new[] {p1, p2, p3});
				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2});
				tris += 3;
			}

			for (int i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, i, zSize),
					float3(xSize - roundness, i, zSize),
					float3(roundness, i + 1, zSize),
					float3(xSize - roundness, i + 1, zSize)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(i, roundness, zSize),
					float3(i + 1, roundness, zSize),
					float3(i, ySize - roundness, zSize),
					float3(i + 1, ySize - roundness, zSize)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = ySize - roundness; i < ySize; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, i, zSize),
					float3(xSize - roundness, i, zSize),
					float3(roundness, i + 1, zSize),
					float3(xSize - roundness, i + 1, zSize)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (int i = xSize - roundness; i < xSize; i++)
			{
				points.AddRange(new[]
				{
					float3(i, roundness, zSize),
					float3(i + 1, roundness, zSize),
					float3(i, ySize - roundness, zSize),
					float3(i + 1, ySize - roundness, zSize)
				});

				indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(roundness, roundness, zSize),
				float3(xSize - roundness, roundness, zSize),
				float3(roundness, ySize - roundness, zSize),
				float3(xSize - roundness, ySize - roundness, zSize)
			});
			indices.AddRange(new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private bool IsTriangleInQuadXZ(float3 p1, float3 p2, float3 p3, float xMin, float xMax, float zMin, float zMax)
		{
			return IsPointInQuadXZ(p1, xMin, xMax, zMin, zMax) &&
			       IsPointInQuadXZ(p2, xMin, xMax, zMin, zMax) &&
			       IsPointInQuadXZ(p3, xMin, xMax, zMin, zMax);
		}

		private bool IsPointInQuadXZ(float3 p, float xMin, float xMax, float zMin, float zMax)
		{
			return p.x >= xMin && p.x <= xMax && p.z >= zMin && p.z <= zMax;
		}

		private bool IsTriangleInQuadYZ(float3 p1, float3 p2, float3 p3, float yMin, float yMax, float zMin, float zMax)
		{
			return IsPointInQuadYZ(p1, yMin, yMax, zMin, zMax) &&
			       IsPointInQuadYZ(p2, yMin, yMax, zMin, zMax) &&
			       IsPointInQuadYZ(p3, yMin, yMax, zMin, zMax);
		}

		private bool IsPointInQuadYZ(float3 p, float yMin, float yMax, float zMin, float zMax)
		{
			return p.y >= yMin && p.y <= yMax && p.z >= zMin && p.z <= zMax;
		}

		private bool IsTriangleInQuadXY(float3 p1, float3 p2, float3 p3, float xMin, float xMax, float yMin, float yMax)
		{
			return IsPointInQuadXY(p1, xMin, xMax, yMin, yMax) &&
			       IsPointInQuadXY(p2, xMin, xMax, yMin, yMax) &&
			       IsPointInQuadXY(p3, xMin, xMax, yMin, yMax);
		}

		private bool IsPointInQuadXY(float3 p, float xMin, float xMax, float yMin, float yMax)
		{
			return p.x >= xMin && p.x <= xMax && p.y >= yMin && p.y <= yMax;
		}
	}
}