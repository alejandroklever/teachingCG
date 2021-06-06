using Rendering;

namespace Renderer.Scene
{
    public class Mastil<V>: SceneObject<V> where V : struct, INormalVertex<V>
    {
        public Mastil(Transform transform) : base(transform)
        {
        }
    }
}