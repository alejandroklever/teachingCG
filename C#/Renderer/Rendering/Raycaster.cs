using GMath;
using System;
using System.Collections.Generic;
using System.Text;
using static GMath.Gfx;
using System.Linq;

namespace Rendering
{
    public enum HitResult
    {
        /// <summary>
        /// The ray hit the geometry but no action is necessary.
        /// </summary>
        Discard = 0,
        /// <summary>
        /// The ray hit the geometry and closest hit should be checked.
        /// </summary>
        CheckClosest = 1,
        /// <summary>
        /// The ray hit the geometry and the search should stop.
        /// </summary>
        Stop = 2
    }

    public interface IRaycastContext
    {
        RayDescription GlobalRay { get; }
        RayDescription LocalRay { get; }
        float CurrentT { get; }
        float4x4 FromWorldToGeometry { get; }
        float4x4 FromGeometryToWorld { get; }
        int GeometryIndex { get; }
    }

    public struct HitInfo<A> where A : struct
    {
        public float T;

        public A Attribute;
    }

    /// <summary>
    /// Action to perform relative to a hit.
    /// </summary>
    /// <typeparam name="P">The payload type of the ray to be updated.</typeparam>
    /// <typeparam name="A">The attribute type of the intersection position. </typeparam>
    /// <param name="attribute">The attribute at the hit position.</param>
    /// <param name="payload">The ray payload to be updated.</param>
    /// <returns></returns>
    public delegate HitResult HitTest<P, A, M>(IRaycastContext context, A attribute, M material, ref P payload) where P : struct where A : struct where M : struct;

    /// <summary>
    /// Action to perform relative to a hit.
    /// </summary>
    /// <typeparam name="P">The payload type of the ray to be updated.</typeparam>
    /// <typeparam name="A">The attribute type of the intersection position. </typeparam>
    /// <param name="attribute">The attribute at the hit position.</param>
    /// <param name="payload">The ray payload to be updated.</param>
    /// <returns></returns>
    public delegate void HitAction<P, A, M>(IRaycastContext context, A attribute, M material, ref P payload) where P : struct where A : struct where M : struct;

    /// <summary>
    /// Action to perform if a ray doesnt hit any surface.
    /// </summary>
    public delegate void MissAction<P>(IRaycastContext context, ref P payload) where P : struct;

    /// <summary>
    /// Represents a retained scene for further ray-tracing.
    /// </summary>
    /// <typeparam name="A">The geometry attribute result of an intersection</typeparam>
    public class Scene<A, M> where A : struct where M : struct
    {
        /// <summary>
        /// Internal object used to pack different geometry properties during raycast.
        /// </summary>
        internal struct Visual
        {
            public IRaycastGeometry<A> Geometry;
            public float4x4 Transform;
            public M Material;
            /// To add more information preprocessed per visual
            /// For instance, Wrapper geometry for early test, IDs, Masks,
        }

        public void Add(IRaycastGeometry<A> geometry, M material, float4x4 transform)
        {
            instances.Add(new Visual
            {
                Geometry = geometry,
                Material = material,
                Transform = transform,
            });
        }

        internal List<Visual> instances = new List<Visual>();
    }

    /// <summary>
    /// Represents a raytracer over several objects in a scene.
    /// The objects are accessed in a scene and the tracer has the logic for the closest hit case and any hit cases.
    /// </summary>
    public class Raytracer<P, A, M> where P : struct where A : struct where M : struct
    {
        public event HitAction<P, A, M> OnClosestHit;
        public event MissAction<P> OnMiss;
        public event HitTest<P, A, M> OnAnyHit;

        /// <summary>
        /// Represents the state of the algorithm for each geometry.
        /// </summary>
        struct InternalRaycastingContext : IRaycastContext
        {
            public RayDescription GlobalRay { get; set; }

            public RayDescription LocalRay { get; set; }

            public float CurrentT { get; set; }

            public float4x4 FromWorldToGeometry { get; set; }

            public float4x4 FromGeometryToWorld { get; set; }

            public int GeometryIndex { get; set; }
        }

        /// <summary>
        /// Performs a trace of a ray through the scene. The payload object will be updated in events OnAnyHit and OnClosestHit.
        /// </summary>
        public void Trace(Scene<A, M> scene, RayDescription ray, ref P payload)
        {
            Scene<A, M>.Visual? closestVisual = null;
            A? closestAttribute = null;
            int? closestGeometryIndex = null;
            float closestDistance = ray.MaxT;
            bool stopped = false;
            
            InternalRaycastingContext context = new InternalRaycastingContext();

            context.GlobalRay = ray;
            context.GeometryIndex = 0;
            foreach (var v in scene.instances)
            {
                context.FromGeometryToWorld = v.Transform;
                context.FromWorldToGeometry = inverse(v.Transform);
                context.LocalRay = ray.Transform(context.FromWorldToGeometry);
                
                foreach (var hitInfo in v.Geometry.Raycast(context.LocalRay))
                {
                    context.CurrentT = hitInfo.T;

                    var result = OnAnyHit == null ? HitResult.CheckClosest : OnAnyHit(context, hitInfo.Attribute, v.Material, ref payload);

                    if ((result & HitResult.CheckClosest) == HitResult.CheckClosest)
                    { // Check current attribute with closest one.
                        if (closestDistance > hitInfo.T)
                        {
                            closestDistance = hitInfo.T;
                            closestVisual = v;
                            closestAttribute = hitInfo.Attribute;
                            closestGeometryIndex = context.GeometryIndex;
                        }
                    }

                    stopped |= (result & HitResult.Stop) == HitResult.Stop;

                    if (stopped) break;
                }
                if (stopped) break;

                context.GeometryIndex++;
            }

            if (closestVisual.HasValue && OnClosestHit != null)
            {
                context.CurrentT = closestDistance;
                context.GeometryIndex = closestGeometryIndex.Value;
                context.FromGeometryToWorld = closestVisual.Value.Transform;
                context.FromWorldToGeometry = inverse(closestVisual.Value.Transform);
                context.LocalRay = ray.Transform(context.FromWorldToGeometry);
                OnClosestHit(context, closestAttribute.Value, closestVisual.Value.Material, ref payload);
            }

            if (!closestVisual.HasValue && OnMiss != null)
                OnMiss(context, ref payload);
        }
    }

    public struct RayDescription
    {
        public float3 Origin;
        public float3 Direction;
        public float MinT;
        public float MaxT;

        public RayDescription(float3 origin, float3 direction, float minT = 0.0001f, float maxT = 1000000)
        {
            this.Origin = origin;
            this.Direction = direction;
            this.MinT = minT;
            this.MaxT = maxT;
        }

        public RayDescription Transform(float4x4 matrix)
        {
            float4 o = float4(Origin, 1);
            float4 t = float4(Origin + Direction, 1);

            o = mul(o, matrix);
            t = mul(t, matrix);

            return new RayDescription(o.xyz / o.w, t.xyz / t.w - o.xyz / o.w, this.MinT, this.MaxT);
        }

        public static RayDescription FromScreen(float px, float py, int width, int height, float4x4 inverseView, float4x4 inverseProjection, float minT, float maxT)
        {
            float4 origin = float4(2 * px / width - 1, 1 - 2 * py / height, 0, 1);
            float4 target = float4(2 * px / width - 1, 1 - 2 * py / height, 1, 1);

            origin = mul(mul(origin, inverseProjection), inverseView);
            target = mul(mul(target, inverseProjection), inverseView);

            return new RayDescription
            {
                Origin = origin.xyz / origin.w,
                Direction = normalize(target.xyz / target.w - origin.xyz / origin.w),
                MinT = minT,
                MaxT = maxT
            };
        }

        public static RayDescription FromTo(float3 origin, float3 target)
        {
            return new RayDescription
            {
                Origin = origin,
                Direction = target - origin,
                MinT = 0,
                MaxT = 1
            };
        }

        public static RayDescription FromDir(float3 origin, float3 direction, float minT = 0.0001f, float maxT = 100000)
        {
            return new RayDescription
            {
                Origin = origin,
                Direction = direction,
                MinT = minT,
                MaxT = maxT
            };
        }

    }

    /// <summary>
    /// Represents a geometry with an implicit ray-intersection logic.
    /// </summary>
    /// <typeparam name="A">Represents the attribute of the geometry at every intersection position.</typeparam>
    public interface IRaycastGeometry<A> where A : struct
    {
        IEnumerable<HitInfo<A>> Raycast(RayDescription ray);
    }

    public static class Raycasting
    {
        #region Attributes Map

        class TransformedAttributes<T,A> : IRaycastGeometry<T> where T : struct where A: struct
        {
            Func<A, T> transform;
            IRaycastGeometry<A> geometry;

            public TransformedAttributes(IRaycastGeometry<A> geometry, Func<A,T> transform)
            {
                this.transform = transform;
                this.geometry = geometry;
            }

            public IEnumerable<HitInfo<T>> Raycast(RayDescription ray)
            {
                return this.geometry.Raycast(ray).Select(h => new HitInfo<T> { T = h.T, Attribute = this.transform(h.Attribute) });
            }
        }

        public static IRaycastGeometry<T> AttributesMap<A, T>(this IRaycastGeometry<A> geometry, Func<A, T> transform) where T : struct where A : struct
        {
            return new TransformedAttributes<T, A>(geometry, transform);
        }

        #endregion

        #region Unitary Sphere

        class UnitarySphereGeometry : QuadricGeometry
        {
            public UnitarySphereGeometry() : base(float3x3(1, 0, 0, 0, 1, 0, 0, 0, 1), float3(0, 0, 0), -1) { }
        }

        static UnitarySphereGeometry __UnitarySphereInstance;
        public static IRaycastGeometry<float3> UnitarySphere { get { return __UnitarySphereInstance ?? (__UnitarySphereInstance = new Raycasting.UnitarySphereGeometry()); } }

        #endregion

        #region Quadric

        class QuadricGeometry : IRaycastGeometry<float3>
        {
            Quadric quadric;

            public QuadricGeometry(float3x3 Q, float3 P, float R)
            {
                this.quadric = new Quadric(Q, P, R);
            }
            public IEnumerable<HitInfo<float3>> Raycast(RayDescription ray)
            {
                float minT, maxT;
                if (!this.quadric.Intersect(new Ray3D(ray.Origin, ray.Direction), out minT, out maxT))
                    yield break;

                if (minT >= ray.MinT && minT < ray.MaxT)
                    yield return new HitInfo<float3>
                    {
                        T = minT,
                        Attribute = ray.Origin + ray.Direction * minT
                    };

                if (maxT >= ray.MinT && maxT < ray.MaxT)
                    yield return new HitInfo<float3>
                    {
                        T = maxT,
                        Attribute = ray.Origin + ray.Direction * maxT
                    };
            }
        }

        public static IRaycastGeometry<float3> Quadric (float3x3 Q, float3 P, float R)
        {
            return new QuadricGeometry(Q, P, R);
        }

        #endregion

        #region Plane

        class PlaneGeometry : IRaycastGeometry<float3>
        {
            Plane3D plane;
            public PlaneGeometry(float3 P, float3 N)
            {
                this.plane = new Plane3D(P, N);
            }

            public IEnumerable<HitInfo<float3>> Raycast(RayDescription ray)
            {
                float t;
                if (!plane.Intersect(new Ray3D(ray.Origin, ray.Direction), out t))
                    yield break;

                if (t >= ray.MinT && t < ray.MaxT)
                    yield return new HitInfo<float3>
                    {
                        T = t,
                        Attribute = ray.Origin + t * ray.Direction
                    };
            }
        }

        static PlaneGeometry __PlaneXY = new PlaneGeometry(float3(0, 0, 0), float3(0, 0, 1));
        static PlaneGeometry __PlaneXZ = new PlaneGeometry(float3(0, 0, 0), float3(0, 1, 0));
        static PlaneGeometry __PlaneYZ = new PlaneGeometry(float3(0, 0, 0), float3(1, 0, 0));

        public static IRaycastGeometry<float3> PlaneXY { get { return __PlaneXY; } }
        public static IRaycastGeometry<float3> PlaneXZ { get { return __PlaneXZ; } }
        public static IRaycastGeometry<float3> PlaneYZ { get { return __PlaneYZ; } }

        #endregion

        #region Mesh Hittest

        class NaiveIntersectableMesh<V> : IRaycastGeometry<V> where V : struct, IVertex<V>
        {
            Mesh<V> mesh;
            public NaiveIntersectableMesh(Mesh<V> mesh)
            {
                this.mesh = mesh;
            }

            public IEnumerable<HitInfo<V>> Raycast(RayDescription ray)
            {
                List<HitInfo<V>> hits = new List<HitInfo<V>>();

                if (mesh.Topology != Topology.Triangles)
                    return hits;

                Ray3D r = new Ray3D(ray.Origin, ray.Direction);

                for (int i = 0; i < mesh.Indices.Length / 3; i++)
                {
                    V v1 = mesh.Vertices[mesh.Indices[i * 3 + 0]];
                    V v2 = mesh.Vertices[mesh.Indices[i * 3 + 1]];
                    V v3 = mesh.Vertices[mesh.Indices[i * 3 + 2]];
                    Triangle3D tri = new Triangle3D(v1.Position, v2.Position, v3.Position);
                    float t;
                    float3 baricenter;
                    if (tri.Intersect(r, out t, out baricenter))
                        if (t >= ray.MinT && t < ray.MaxT)
                            hits.Add(new HitInfo<V>
                            {
                                T = t,
                                Attribute = v1.Mul(baricenter.x).Add(v2.Mul(baricenter.y)).Add(v3.Mul(baricenter.z))
                            });
                }

                hits.Sort((h1, h2) => h1.T.CompareTo(h2.T));

                return hits;
            }
        }

        class GridIntersectableMesh<V> : IRaycastGeometry<V> where V : struct, IVertex<V>
        {
            /// <summary>
            /// Stored mesh
            /// </summary>
            Mesh<V> mesh;

            /// <summary>
            /// Hash of the triangles in a grid partitioning
            /// </summary>
            List<int>[,,] triangleHash;

            /// <summary>
            /// Resolution of the grid
            /// </summary>
            int resolution;

            /// <summary>
            /// AABB of the mesh
            /// </summary>
            AABB3D box;

            public GridIntersectableMesh(Mesh<V> mesh)
            {
                this.mesh = mesh;

                resolution = 20;
                triangleHash = new List<int>[resolution, resolution, resolution];

                box = mesh.ComputeAABB();

                float3 dim = box.Maximum - box.Minimum;
                float maxDim = max(dim.x, max(dim.y, dim.z));

                box = new AABB3D { Minimum = box.Minimum - 0.1f, Maximum = box.Minimum + maxDim + 0.1f }; // extend to grant all mesh-intersections are inside the box.

                for (int i = 0; i < mesh.Indices.Length / 3; i++)
                {
                    V v1 = mesh.Vertices[mesh.Indices[i * 3 + 0]];
                    V v2 = mesh.Vertices[mesh.Indices[i * 3 + 1]];
                    V v3 = mesh.Vertices[mesh.Indices[i * 3 + 2]];
                    Triangle3D tri = new Triangle3D(v1.Position, v2.Position, v3.Position);

                    float3 minimum = min(v1.Position, min(v2.Position, v3.Position));
                    float3 maximum = max(v1.Position, max(v2.Position, v3.Position));

                    int3 corner1 = (int3)((minimum - box.Minimum) * resolution / (box.Maximum - box.Minimum));
                    int3 corner2 = (int3)((maximum - box.Minimum) * resolution / (box.Maximum - box.Minimum));

                    for (int z = corner1.z; z <= corner2.z; z++)
                        for (int y = corner1.y; y <= corner2.y; y++)
                            for (int x = corner1.x; x <= corner2.x; x++)
                            {
                                if (triangleHash[z, y, x] == null)
                                    triangleHash[z, y, x] = new List<int>();
                                triangleHash[z, y, x].Add(i); // add the triangle to the list.
                            }
                }
            }

            public IEnumerable<HitInfo<V>> Raycast(RayDescription ray)
            {
                if (mesh.Topology != Topology.Triangles)
                    yield break;

                Ray3D r = new Ray3D(ray.Origin, ray.Direction + 0.000000001f); // epsilon deviation of the direction to avoid indefinitions

                float minT, maxT;
                if (!box.Intersect(r, out minT, out maxT))
                    yield break;

                maxT = min(maxT, ray.MaxT);

                float t = max(ray.MinT, minT);

                float3 P = r.X + r.D * t;

                int3 cell = (int3)min(((P - box.Minimum) * resolution / (box.Maximum - box.Minimum)), resolution - 1);


                float3 side = r.D > 0;

                int3 cellInc = (r.D > 0) * 2 - 1;

                float3 corner = box.Minimum + (cell + side) * (box.Maximum - box.Minimum) / resolution;

                float3 alphas = (corner - r.X) / r.D;
                float3 alphaInc = (box.Maximum - box.Minimum) / abs(resolution * r.D);

                while (t < maxT)
                {
                    float nextT = min(alphas.x, min(alphas.y, alphas.z));

                    if (any(cell < 0) || any(cell >= resolution))
                        yield break; // just for numerical problems, traversal could go outside grid.

                    // check current cell
                    if (triangleHash[cell.z, cell.y, cell.x] != null)
                    {
                        List<HitInfo<V>> hits = new List<HitInfo<V>>();

                        foreach (var i in triangleHash[cell.z, cell.y, cell.x])
                        {
                            V v1 = mesh.Vertices[mesh.Indices[i * 3 + 0]];
                            V v2 = mesh.Vertices[mesh.Indices[i * 3 + 1]];
                            V v3 = mesh.Vertices[mesh.Indices[i * 3 + 2]];
                            Triangle3D tri = new Triangle3D(v1.Position, v2.Position, v3.Position);
                            float triT;
                            float3 baricenter;
                            if (tri.Intersect(r, out triT, out baricenter))
                                if (triT >= t && triT <= nextT)
                                    hits.Add(new HitInfo<V>
                                    {
                                        T = triT,
                                        Attribute = v1.Mul(baricenter.x).Add(v2.Mul(baricenter.y)).Add(v3.Mul(baricenter.z))
                                    });
                        }

                        hits.Sort((h1, h2) => h1.T.CompareTo(h2.T)); // only need to sort hits inside a cell, because cells are iterated in ray-order

                        foreach (var hi in hits)
                            yield return hi;
                    }

                    // advance ray to next cell
                    int3 movement = new int3(alphas.x <= alphas.y && alphas.x <= alphas.z, alphas.y < alphas.x && alphas.y <= alphas.z, alphas.z < alphas.x && alphas.z < alphas.y);
                    cell += movement * cellInc;
                    alphas += movement * alphaInc;
                    t = nextT;
                }
            }
        }

        public static IRaycastGeometry<V> AsRaycast<V>(this Mesh<V> mesh, RaycastingMeshMode mode = RaycastingMeshMode.Grid) where V : struct, IVertex<V>
        {
            // TODO: Implement another strategy using Acceleration Data-Structures.
            switch (mode)
            {
                case RaycastingMeshMode.Naive:
                    return new NaiveIntersectableMesh<V>(mesh);
                case RaycastingMeshMode.Grid:
                    return new GridIntersectableMesh<V>(mesh);
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion
    }

    public enum RaycastingMeshMode
    {
        Naive,
        Grid,
        KdTree,
        BSP,
        BVH
    }
}
