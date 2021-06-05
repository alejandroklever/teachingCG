using GMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rendering
{
    /// <summary>
    /// Represents an interpolable vertices.
    /// </summary>
    public interface IInterpolable<V> where V : struct
    {
        /// <summary>
        /// Multiplies all components of this vertex by scalar
        /// </summary>
        V Mul(float s);

        /// <summary>
        /// Adds all components of this vertex with another vertex.
        /// </summary>
        V Add(V other);
    }

    /// <summary>
    /// Represents a vertex with 3D position.
    /// This information can be used by Mesh object to apply geometric transforms, rasterizer and raycasters.
    /// </summary>
    public interface IVertex<V> : IInterpolable<V> where V : struct
    {
        /// <summary>
        /// Gets or sets the 3D position of a vertex.
        /// </summary>
        float3 Position { get; set; }
    }

    public interface INormalVertex<V> : IVertex<V> where V: struct
    {
        /// <summary>
        /// Gets or sets the 3D normal of a vertex
        /// </summary>
        float3 Normal { get; set; }
    }

    public interface ICoordinatesVertex<V> : IVertex<V> where V : struct
    {
        /// <summary>
        /// Gets or sets the 2D texture coordinates of a vertex
        /// </summary>
        float2 Coordinates { get; set; }
    }

    /// <summary>
    /// Represents a vertex with 4D homogeneous coordinates.
    /// </summary>
    public interface IProjectedVertex<V> : IInterpolable<V> where V :struct
    {
        /// <summary>
        /// Gets or sets the homogeneous coordinates for this vertex.
        /// </summary>
        float4 Homogeneous { get; set; }
    }
}
