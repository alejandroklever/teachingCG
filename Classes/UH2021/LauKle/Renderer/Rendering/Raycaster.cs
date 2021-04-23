using GMath;
using System;
using System.Collections.Generic;
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
    public delegate HitResult HitTest<P, A>(IRaycastContext context, A attribute, ref P payload)
        where P : struct where A : struct;

    /// <summary>
    /// Action to perform relative to a hit.
    /// </summary>
    /// <typeparam name="P">The payload type of the ray to be updated.</typeparam>
    /// <typeparam name="A">The attribute type of the intersection position. </typeparam>
    /// <param name="attribute">The attribute at the hit position.</param>
    /// <param name="payload">The ray payload to be updated.</param>
    /// <returns></returns>
    public delegate void HitAction<P, A>(IRaycastContext context, A attribute, ref P payload)
        where P : struct where A : struct;

    /// <summary>
    /// Action to perform if a ray doesnt hit any surface.
    /// </summary>
    public delegate void MissAction<P>(IRaycastContext context, ref P payload) where P : struct;

    /// <summary>
    /// Represents a retained scene for further ray-tracing.
    /// </summary>
    /// <typeparam name="A">The geometry attribute result of an intersection</typeparam>
    public class Scene<A> where A : struct
    {
        /// <summary>
        /// Internal object used to pack different geometry properties during raycast.
        /// </summary>
        internal struct Visual
        {
            public IRaycastGeometry<A> Geometry;

            public float4x4 Transform;
            // To add more information preprocessed per visual
            // For instance, Wrapper geometry for early test, IDs, Masks,
        }

        public void Add(IRaycastGeometry<A> geometry, float4x4 transform)
        {
            instances.Add(new Visual
            {
                Geometry = geometry,
                Transform = transform,
            });
        }

        internal List<Visual> instances = new List<Visual>();
    }

    /// <summary>
    /// Represents a raytracer over several objects in a scene.
    /// The objects are accessed in a scene and the tracer has the logic for the closest hit case and any hit cases.
    /// </summary>
    public class Raytracer<P, A> where P : struct where A : struct
    {
        public event HitAction<P, A> OnClosestHit;
        public event MissAction<P> OnMiss;
        public event HitTest<P, A> OnAnyHit;

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
        public void Trace(Scene<A> scene, RayDescription ray, ref P payload)
        {
            Scene<A>.Visual? closestVisual = null;
            A? closestAttribute = null;
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

                    var result = OnAnyHit == null
                        ? HitResult.CheckClosest
                        : OnAnyHit(context, hitInfo.Attribute, ref payload);

                    if ((result & HitResult.CheckClosest) == HitResult.CheckClosest)
                    {
                        // Check current attribute with closest one.
                        if (closestDistance > hitInfo.T)
                        {
                            closestDistance = hitInfo.T;
                            closestVisual = v;
                            closestAttribute = hitInfo.Attribute;
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
                context.FromGeometryToWorld = closestVisual.Value.Transform;
                context.FromWorldToGeometry = inverse(closestVisual.Value.Transform);
                context.LocalRay = ray.Transform(context.FromWorldToGeometry);
                OnClosestHit(context, closestAttribute.Value, ref payload);
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
            Origin = origin;
            Direction = direction;
            MinT = minT;
            MaxT = maxT;
        }

        public RayDescription Transform(float4x4 matrix)
        {
            float4 o = float4(Origin, 1);
            float4 t = float4(Origin + Direction, 1);

            o = mul(o, matrix);
            t = mul(t, matrix);

            return new RayDescription(o.xyz / o.w, t.xyz / t.w - o.xyz / o.w, MinT, MaxT);
        }

        public static RayDescription FromScreen(float px, float py, int width, int height, float4x4 inverseView,
            float4x4 inverseProjection, float minT, float maxT)
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

        class TransformedAttributes<T, A> : IRaycastGeometry<T> where T : struct where A : struct
        {
            Func<A, T> transform;
            IRaycastGeometry<A> geometry;

            public TransformedAttributes(IRaycastGeometry<A> geometry, Func<A, T> transform)
            {
                this.transform = transform;
                this.geometry = geometry;
            }

            public IEnumerable<HitInfo<T>> Raycast(RayDescription ray)
            {
                return geometry.Raycast(ray)
                    .Select(h => new HitInfo<T> {T = h.T, Attribute = transform(h.Attribute)});
            }
        }

        public static IRaycastGeometry<T> AttributesMap<A, T>(this IRaycastGeometry<A> geometry, Func<A, T> transform)
            where T : struct where A : struct
        {
            return new TransformedAttributes<T, A>(geometry, transform);
        }

        #endregion

        #region Unitary Sphere

        class UnitarySphereGeometry : QuadricGeometry
        {
            public UnitarySphereGeometry() : base(float3x3(1, 0, 0, 0, 1, 0, 0, 0, 1), float3(0, 0, 0), -1)
            {
            }
        }

        static UnitarySphereGeometry __UnitarySphereInstance;

        public static IRaycastGeometry<float3> UnitarySphere
        {
            get
            {
                return __UnitarySphereInstance ?? (__UnitarySphereInstance = new UnitarySphereGeometry());
            }
        }

        #endregion

        #region Quadric

        class QuadricGeometry : IRaycastGeometry<float3>
        {
            Quadric quadric;

            public QuadricGeometry(float3x3 Q, float3 P, float R)
            {
                quadric = new Quadric(Q, P, R);
            }

            public IEnumerable<HitInfo<float3>> Raycast(RayDescription ray)
            {
                float minT, maxT;
                if (!quadric.Intersect(new Ray3D(ray.Origin, ray.Direction), out minT, out maxT))
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

        public static IRaycastGeometry<float3> Quadric(float3x3 Q, float3 P, float R)
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
                plane = new Plane3D(P, N);
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

        public static IRaycastGeometry<float3> PlaneXY
        {
            get { return __PlaneXY; }
        }

        public static IRaycastGeometry<float3> PlaneXZ
        {
            get { return __PlaneXZ; }
        }

        public static IRaycastGeometry<float3> PlaneYZ
        {
            get { return __PlaneYZ; }
        }

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

        public static IRaycastGeometry<V> AsRaycast<V>(this Mesh<V> mesh) where V : struct, IVertex<V>
        {
            // TODO: Implement another strategy using Acceleration Data-Structures.
            return new NaiveIntersectableMesh<V>(mesh);
        }

        #endregion
    }
}
