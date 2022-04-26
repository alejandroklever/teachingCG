using System.Collections.Generic;
using System.Linq;
using Rendering;
using static GMath.Gfx;
using float3 = GMath.float3;

namespace Renderer.Scene
{
	public class Cube<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
	{
		public readonly int roundness;
		public readonly int xSize;
		public readonly int ySize;
		public readonly int zSize;
		
		public Cube(Transform transform, int xSize, int ySize, int zSize, int roundness = 0,
			bool reduceTriangles = true, bool renderBottom = true) : base(transform)
		{
			this.xSize = xSize;
			this.ySize = ySize;
			this.zSize = zSize;
			this.roundness = roundness;
			
			CreateMesh(reduceTriangles, renderBottom);
			RoundVertices();
			UpdateTranslation();
		}

		private void CreateMesh(bool reduceTriangles, bool renderBottom)
		{
			var m = MyManifold<V>.SurfaceDiscrete(zSize, ySize,
				(u, v) => float3(xSize, v, u));
			var right = reduceTriangles ? ReduceTrianglesYZ(m) : m;

			m = MyManifold<V>.SurfaceDiscrete(zSize, ySize,
				(u, v) => float3(0, v, zSize - u));
			var left = reduceTriangles ? ReduceTrianglesYZ(m, true) : m;

			m = MyManifold<V>.SurfaceDiscrete(xSize, ySize,
				(u, v) => float3(xSize - u, v, zSize));
			var back = reduceTriangles ? ReduceTrianglesXY(m) : m;

			m = MyManifold<V>.SurfaceDiscrete(xSize, ySize,
				(u, v) => float3(u, v, 0));
			var front = reduceTriangles ? ReduceTrianglesXY(m, true) : m;

			m = MyManifold<V>.SurfaceDiscrete(xSize, zSize,
				(u, v) => float3(u, ySize, v));
			var top = reduceTriangles ? ReduceTrianglesXZ(m) : m;


			Mesh = MeshTools.Join(right, left, back, front, top).Weld();

			if (!renderBottom) return;

			m = MyManifold<V>.SurfaceDiscrete(xSize, zSize,
				(u, v) => float3(xSize - u, 0, v));
			var bottom = reduceTriangles ? ReduceTrianglesXZ(m, true) : m;


			Mesh = Mesh.Concat(bottom).Weld();
		}

		private void RoundVertices()
		{
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
		}

		private Mesh<V> ReduceTrianglesXZ(Mesh<V> mesh, bool inverse = false)
		{
			var Y = inverse ? 0 : ySize;
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

			for (var i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, Y, i),
					float3(roundness, Y, i + 1),
					float3(xSize - roundness, Y, i),
					float3(xSize - roundness, Y, i + 1)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(i, Y, roundness),
					float3(i, Y, zSize - roundness),
					float3(i + 1, Y, roundness),
					float3(i + 1, Y, zSize - roundness)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (var i = zSize - roundness; i < zSize; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, Y, i),
					float3(roundness, Y, i + 1),
					float3(xSize - roundness, Y, i),
					float3(xSize - roundness, Y, i + 1)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (var i = xSize - roundness; i < xSize; i++)
			{
				points.AddRange(new[]
				{
					float3(i, Y, roundness),
					float3(i, Y, zSize - roundness),
					float3(i + 1, Y, roundness),
					float3(i + 1, Y, zSize - roundness)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(roundness, Y, roundness),
				float3(roundness, Y, zSize - roundness),
				float3(xSize - roundness, Y, roundness),
				float3(xSize - roundness, Y, zSize - roundness)
			});
			indices.AddRange(inverse
				? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
				: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private Mesh<V> ReduceTrianglesYZ(Mesh<V> mesh, bool inverse = false)
		{
			var X = inverse ? 0 : xSize;
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

			for (var i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(X, roundness, i),
					float3(X, ySize - roundness, i),
					float3(X, roundness, i + 1),
					float3(X, ySize - roundness, i + 1)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(X, i, roundness),
					float3(X, i + 1, roundness),
					float3(X, i, zSize - roundness),
					float3(X, i + 1, zSize - roundness)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}


			for (var i = zSize - roundness; i < zSize; i++)
			{
				points.AddRange(new[]
				{
					float3(X, roundness, i),
					float3(X, ySize - roundness, i),
					float3(X, roundness, i + 1),
					float3(X, ySize - roundness, i + 1)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (var i = ySize - roundness; i < ySize; i++)
			{
				points.AddRange(new[]
				{
					float3(X, i, roundness),
					float3(X, i + 1, roundness),
					float3(X, i, zSize - roundness),
					float3(X, i + 1, zSize - roundness)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(X, roundness, roundness),
				float3(X, ySize - roundness, roundness),
				float3(X, roundness, zSize - roundness),
				float3(X, ySize - roundness, zSize - roundness)
			});
			indices.AddRange(inverse
				? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
				: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private Mesh<V> ReduceTrianglesXY(Mesh<V> mesh, bool inverse = false)
		{
			var Z = inverse ? 0 : zSize;
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

			for (var i = 0; i < roundness; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, i, Z),
					float3(xSize - roundness, i, Z),
					float3(roundness, i + 1, Z),
					float3(xSize - roundness, i + 1, Z)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;

				points.AddRange(new[]
				{
					float3(i, roundness, Z),
					float3(i + 1, roundness, Z),
					float3(i, ySize - roundness, Z),
					float3(i + 1, ySize - roundness, Z)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (var i = ySize - roundness; i < ySize; i++)
			{
				points.AddRange(new[]
				{
					float3(roundness, i, Z),
					float3(xSize - roundness, i, Z),
					float3(roundness, i + 1, Z),
					float3(xSize - roundness, i + 1, Z)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			for (var i = xSize - roundness; i < xSize; i++)
			{
				points.AddRange(new[]
				{
					float3(i, roundness, Z),
					float3(i + 1, roundness, Z),
					float3(i, ySize - roundness, Z),
					float3(i + 1, ySize - roundness, Z)
				});
				indices.AddRange(inverse
					? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
					: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});
				tris += 4;
			}

			points.AddRange(new[]
			{
				float3(roundness, roundness, Z),
				float3(xSize - roundness, roundness, Z),
				float3(roundness, ySize - roundness, Z),
				float3(xSize - roundness, ySize - roundness, Z)
			});
			indices.AddRange(inverse
				? new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3}.Reverse()
				: new[] {tris + 0, tris + 1, tris + 2, tris + 2, tris + 1, tris + 3});

			return new Mesh<V>(points.Select(p => new V {Position = p}).ToArray(), indices.ToArray()).Weld();
		}

		private static bool IsTriangleInQuadXZ(float3 p1, float3 p2, float3 p3, float xMin, float xMax, float zMin,
			float zMax) =>
			IsPointInQuadXZ(p1, xMin, xMax, zMin, zMax) &&
			IsPointInQuadXZ(p2, xMin, xMax, zMin, zMax) &&
			IsPointInQuadXZ(p3, xMin, xMax, zMin, zMax);

		private static bool IsPointInQuadXZ(float3 p, float xMin, float xMax, float zMin, float zMax)
			=> p.x >= xMin && p.x <= xMax && p.z >= zMin && p.z <= zMax;

		private static bool IsTriangleInQuadYZ(float3 p1, float3 p2, float3 p3, float yMin, float yMax, float zMin,
			float zMax) =>
			IsPointInQuadYZ(p1, yMin, yMax, zMin, zMax) &&
			IsPointInQuadYZ(p2, yMin, yMax, zMin, zMax) &&
			IsPointInQuadYZ(p3, yMin, yMax, zMin, zMax);

		private static bool IsPointInQuadYZ(float3 p, float yMin, float yMax, float zMin, float zMax)
			=> p.y >= yMin && p.y <= yMax && p.z >= zMin && p.z <= zMax;

		private static bool IsTriangleInQuadXY(float3 p1, float3 p2, float3 p3, float xMin, float xMax, float yMin,
			float yMax) =>
			IsPointInQuadXY(p1, xMin, xMax, yMin, yMax) &&
			IsPointInQuadXY(p2, xMin, xMax, yMin, yMax) &&
			IsPointInQuadXY(p3, xMin, xMax, yMin, yMax);

		private static bool IsPointInQuadXY(float3 p, float xMin, float xMax, float yMin, float yMax)
			=> p.x >= xMin && p.x <= xMax && p.y >= yMin && p.y <= yMax;
	}
}