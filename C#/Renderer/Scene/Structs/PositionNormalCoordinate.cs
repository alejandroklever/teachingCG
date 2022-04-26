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
                Position = Position + other.Position,
                Normal = Normal + other.Normal,
                Coordinates = Coordinates + other.Coordinates
            };
        }

        public PositionNormalCoordinate Mul(float s)
        {
            return new()
            {
                Position = Position * s,
                Normal = Normal * s,
                Coordinates = Coordinates * s
            };
        }

        public PositionNormalCoordinate Transform(float4x4 matrix)
        {
            var p = float4(Position, 1);
            p = mul(p, matrix);
                
            var n = float4(Normal, 0);
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