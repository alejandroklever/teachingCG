using GMath;
using Rendering;
using static GMath.Gfx;

namespace Renderer.Scene
{
    public struct PositionNormalCoordinate : INormalVertex<PositionNormalCoordinate>, ICoordinatesVertex<PositionNormalCoordinate>
    {
        public float3 Position { get; set; }
        public float3 Normal { get; set; }

        public float2 Coordinates { get; set; }

        public PositionNormalCoordinate Add(PositionNormalCoordinate other)
        {
            return new()
            {
                Position = this.Position + other.Position,
                Normal = this.Normal + other.Normal,
                Coordinates = this.Coordinates + other.Coordinates
            };
        }

        public PositionNormalCoordinate Mul(float s)
        {
            return new()
            {
                Position = this.Position * s,
                Normal = this.Normal * s,
                Coordinates = this.Coordinates * s
            };
        }

        public PositionNormalCoordinate Transform(float4x4 matrix)
        {
            float4 p = float4(Position, 1);
            p = mul(p, matrix);
                
            float4 n = float4(Normal, 0);
            n = mul(n, matrix);

            return new PositionNormalCoordinate
            {
                Position = p.xyz / p.w,
                Normal = n.xyz,
                Coordinates = Coordinates
            };
        }
    }
}