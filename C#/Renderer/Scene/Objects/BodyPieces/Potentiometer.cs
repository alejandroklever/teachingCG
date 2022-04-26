using Rendering;
using static Renderer.Scene.MeshTools;

namespace Renderer.Scene
{
    public class Potentiometer<V> : SceneObject<V> where V : struct, INormalVertex<V>, ICoordinatesVertex<V>
    {
        public Potentiometer(Transform transform, int roundness) : base(transform)
        {
            var bottomMesh = MyManifold<V>.Cone(roundness, 1, zero, 1, .8f, .25f, createDiscs: false);
            var topMesh = MyManifold<V>.Cylinder(roundness, 1, .25f * up, .8f, .5f, createDiscs: false);
            var disc = MyManifold<V>.Disc(roundness, 1, .75f * up, .8f);

            // Mesh = Join(bottomMesh, topMesh, disc).Weld();

            var mesh = Join(bottomMesh, topMesh, disc).Weld();
            AddMesh(mesh, Materials.Default);
            
            UpdateTranslation(zero);
        }
    }
}   