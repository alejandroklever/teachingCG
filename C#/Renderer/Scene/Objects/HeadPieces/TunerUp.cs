using System.Threading.Tasks.Dataflow;
using Rendering;
using static Renderer.Scene.MeshTools;
using float3 = GMath.float3;

namespace Renderer.Scene
{
    public class TunerUp<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        private const int roundness = 6;

        public TunerUp(Transform transform) : base(transform)
        {
            var washer = MyManifold<V>.Cylinder(roundness, 1, zero, .75f, .1f, createDiscs: false);
            var washerTop = MyManifold<V>.Disc(roundness, 1, .1f * float3.up, .75f).Weld();
            var cone0 = MyManifold<V>.Cone(roundness, 1, .1f * float3.up, .4f, .2f, .75f, createDiscs: false);
            var cone1 = MyManifold<V>.Cone(roundness, 1, .85f * float3.up, .2f, .4f, .4f, createDiscs: false);
            var cone2 = MyManifold<V>.Cone(roundness, 1, 1.25f * float3.up, .4f, 0, .1f, createDiscs: false).Weld();

            Add(Join(washer, washerTop).Weld(), Materials.Default);
            Add(Join(cone0, cone1, cone2).Weld(), Materials.Default);

            // Mesh =  MeshTools.Join(washer, washerTop, cone0, cone1, cone2);
            UpdateTranslation(zero);
        }
    }
}